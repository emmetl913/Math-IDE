using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                handle_mat(code, item, current, next);
            }
        }

        public void handle_mat(string[] code, string item, string current, string next)
        {
            Debug.WriteLine("Matrix Detected!");
            if (current == "mat")
            {
                string name = next;
                int x = 0, y = 0;

                while (next != "]")
                {
                    //next = code[Array.IndexOf(code, next) + 1];
                    if (x < code.Length) { 
                        next = code[x++];
                    }
                    if (next == ";")
                    {
                        y++;
                    }
                    if (int.TryParse(next, out int result))
                    {
                        x++;
                    }
                    Debug.WriteLine($"{name} {result}");
                }
                mats[offset] = new Matrix(x, y, name);
                offset++;
                next = name;
                int[,] tempMat = new int[x, y];
                x = 0; y = 0;
                while (next != "]")
                {
                    if (int.TryParse(next, out int result))
                    {
                        tempMat[x, y] = result;
                        x++;
                    }
                    if (next == ";")
                    {
                        y++;
                    }
                }
                Debug.WriteLine("New Matrix Created! In=" + (offset - 1));
            }
            else
            {
                Debug.WriteLine("Item was not a matrix, returning...");
                return;
            }
        }

        public class Matrix (int x_size, int y_size, string name)
        {
            public int[,] matrix = new int[x_size, y_size];
            public string name = name;
            public int x_size = x_size;
            public int y_size = y_size;
        }
        
    }
}
