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
            Debug.WriteLine("Creating matrix under name: " +  name);
            while (next != "]")
            {
                if (next == ";")
                {
                    y++;
                }
                if (int.TryParse(next, out int result))
                {
                    x++;
                }
                if (i < code.Length)
                { 
                    next = code[i++];
                }

                Debug.WriteLine($"{next} {x}");
            }
            mats[offset] = new Matrix(x, y, name);
            offset++;

            int[,] tempMat = new int[y, x]; 
            int row = 0, col = 0, j = 0;

            next = code[i++];

            while (next != "]" && j <= code.Length)
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

            Debug.WriteLine("Matrix parsed successfully!");


            Debug.WriteLine("New Matrix Created! In=" + (offset - 1));
            mats[offset-1].DebugPrint();
        }



        public class Matrix
        {
            public int Rows { get; }
            public int Cols { get; }
            public string Name { get; }
            public int[,] Data { get; }

            public Matrix(int rows, int cols, string name)
            {
                Rows = rows;
                Cols = cols;
                Name = name;
                Data = new int[rows, cols];
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
        }
        
    }
}
