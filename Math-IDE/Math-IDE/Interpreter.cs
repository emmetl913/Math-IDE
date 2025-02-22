using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Math_IDE;
using static System.Net.Mime.MediaTypeNames;

namespace Math_IDE
{
    internal class Interpreter
    {
        // <summary>
        /// Assuming code is already tokenzied, determine appropriate operations  
        /// and variable definitions
        /// </summary>
        Matrix[] mats = new Matrix[1000];
        public LinkedList<string> output_log = new LinkedList<string>();
        int offset = 0;
        public void parseCode(string[] code)
        {
            int iterator = 0;
            string next = "";
            foreach (string item in code)
            { 
                string current = item;
                if (iterator < code.Length-1)
                {
                    next = code[iterator + 1];
                }
                Debug.WriteLine("Current Item: " + current + "     Next Item: " + next);
                if (current == "mat")
                {
                    Debug.WriteLine("Matrix Detected! " + item + "  " + current + "  " + next);
                    handle_mat(code, item, current, next, iterator);
                }
                if (next == "=")
                {
                    Debug.WriteLine("Operation Detected! Now attempting to parse... " + current + " " + next +" ");
                }
                iterator++;
            }
        }

        public void handle_mat(string[] code, string item, string current, string next, int iterator)
        {
            string name = next;
            int x = 0, y = 0, i = iterator;
            Debug.WriteLine($"Current x:{x}, y:{y}, i:{i}");
            bool first_row = true;
            Debug.WriteLine("Creating matrix under name: " +  name);
            while (next != "]")
            {
                if (next == ";")
                {
                    y++;
                    first_row = false;
                }
                if (int.TryParse(next, out int result) && first_row)
                {
                    x++;
                }
                if (i < code.Length)
                { 
                    next = code[i++];
                }

                Debug.WriteLine($"{next} {x}");
            }
            y++;
            Debug.WriteLine($"Matrix size defined as: {y} , {x}");
            Debug.WriteLine("Matrix should now be empty: ");
            

            int[,] tempMat = new int[y, x]; 
            int row = 0, col = 0, j = Array.IndexOf(code, name);
            Debug.WriteLine("Index of mat: " + j);
            next = name;
            Debug.WriteLine($"Next item: {next}");
            while (next != "]")
            {
                if (int.TryParse(next, out int result))
                {
                    if (col >= x)
                    {
                        Debug.WriteLine($"Error: Too many columns in row {row}.");
                        break;
                    }

                    tempMat[row, col] = result;
                    col++;
                }
                else if (next == ";")
                {
                    if (row + 1 >= y)
                    {
                        Debug.WriteLine("Error: Too many rows.");
                        break;
                    }

                    row++; 
                    col = 0; 
                }

                if (j < code.Length)
                {
                    next = code[j++];
                }
            }
            mats[offset] = new Matrix(x, y, name, tempMat);
            offset++;
            Debug.WriteLine("Matrix parsed successfully!");
            output_log.AddFirst($"New Matrix {mats[offset - 1].Name} Created! In=" + (offset - 1) + "  Contents: \n" + mats[offset-1].DebugPrintGetter());

            Debug.WriteLine("New Matrix A Created! In=" + (offset - 1));
            mats[offset-1].DebugPrint();
        }

        public void mat_arithemetic(string name1, string name2, string opp, string resultant_name)
        {
            // Search mats array for two matricies to add
            int[,] temp = new int[0, 0];
            Matrix mat1 = new Matrix(0, 0, "nullptr", temp); 
            Matrix mat2 = new Matrix(0, 0, "nullptr", temp );
            for (int i = 0; i < mats.Length; i++)
            {
                if (mats[i].Name == name1)
                { 
                    mat1 = mats[i];
                }
                if (mats[i].Name == name2)
                { 
                    mat2 = mats[i];
                }
            }

            if (opp == "+")
            {
                Matrix result = mat1.add(mat2, resultant_name);
            }
            else if (opp == "-")
            {
                Matrix result = mat1.subtract(mat2, resultant_name);
            }
            else if (opp == "*")
            {

            }
            else if (opp == "/")
            {

            }
            else
            {
                throw new Exception("Operator not recognized");
            }
        }



        public class Matrix
        {
            public int Rows { get; }
            public int Cols { get; }
            public string Name { get; }
            public int[,] Data { get; }

            public Matrix(int rows, int cols, string name, int[,] data)
            {
                Rows = rows;
                Cols = cols;
                Name = name;
                Data = data;
            }
            public void DebugPrint()
            {
                Debug.WriteLine($"Matrix Name: {Name}");
                Debug.WriteLine($"Dimensions: {Rows} x {Cols}");
                Debug.WriteLine("Contents:");

                for (int i = 0; i < Cols; i++)
                {
                    string rowValues = "";
                    for (int j = 0; j < Rows; j++)
                    {
                        rowValues += Data[i, j] + (j < Cols - 1 ? ", " : "");
                    }
                    Debug.WriteLine(rowValues);
                }
            }
            public string DebugPrintGetter()
            {
                string rowValues = "";
                for (int i = 0; i < Cols; i++)
                {
                    
                    for (int j = 0; j < Rows; j++)
                    {
                        rowValues += Data[i, j] + (j < Cols - 1 ? ", " : "");
                    }
                    rowValues += "\n";
                }
                return rowValues;
            }

            public Matrix add(Matrix opperator, string new_name)
            {
                if (opperator.Rows != this.Rows || opperator.Cols != this.Cols)
                {
                    throw new Exception("Cannot add two matricies with different rows/cols");
                }


                int[,] new_data = new int[this.Rows, this.Cols];
                for (int i = 0; i < Cols; i++)
                {
                    string rowValues = "";
                    for (int j = 0; j < Rows; j++)
                    {
                        new_data[i, j] = this.Data[i,j] + opperator.Data[i,j];
                    }
                }

                Matrix resultant = new Matrix(Rows, Cols, new_name, new_data);
                return resultant;
            }

            public Matrix subtract(Matrix opperator, string new_name)
            {
                if (opperator.Rows != this.Rows || opperator.Cols != this.Cols)
                {
                    throw new Exception("Cannot add two matricies with different rows/cols");
                }


                int[,] new_data = new int[this.Rows, this.Cols];
                for (int i = 0; i < Cols; i++)
                {
                    string rowValues = "";
                    for (int j = 0; j < Rows; j++)
                    {
                        new_data[i, j] = this.Data[i, j] - opperator.Data[i, j];
                    }
                }

                Matrix resultant = new Matrix(Rows, Cols, new_name, new_data);
                return resultant;
            }
        }
        
    }
}
