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
            foreach (string item in code)
            { 
                string current = item;
                string next = code[Array.IndexOf(code, item) + 1];
                Debug.WriteLine("Current Item: " + current + "     Next Item: " + next);
                if (current == "mat")
                {
                    Debug.WriteLine("Matrix Detected!");
                    handle_mat(code, item, current, next);
                }
            }
        }

        public void handle_mat(string[] code, string item, string current, string next)
        {
            string name = next;
            int x = 0, y = 0, i = 0;
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
            output_log.AddFirst("New Matrix A Created! In=" + (offset - 1) + "  Contents: \n" + mats[offset-1].DebugPrintGetter());

            Debug.WriteLine("New Matrix A Created! In=" + (offset - 1));
            mats[offset-1].DebugPrint();
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

                for (int i = 0; i < Rows; i++)
                {
                    string rowValues = "";
                    for (int j = 0; j < Cols; j++)
                    {
                        rowValues += Data[i, j] + (j < Cols - 1 ? ", " : "");
                    }
                    Debug.WriteLine(rowValues);
                }
            }
            public string DebugPrintGetter()
            {
                string rowValues = "";
                for (int i = 0; i < Rows; i++)
                {
                    
                    for (int j = 0; j < Cols; j++)
                    {
                        rowValues += Data[i, j] + (j < Cols - 1 ? ", " : "");
                    }
                    rowValues += "\n";
                }
                return rowValues;
            }
        }
        
    }
}
