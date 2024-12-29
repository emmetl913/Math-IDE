using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math_IDE;

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
                if (current == "mat")
                {
                    string name = next;
                    int x = 0, y = 0;
                   
                    while (next != "]")
                    {
                        next = code[Array.IndexOf(code, next) + 1];
                        if (next == ";")
                        {
                            y++;
                        }
                        if (int.TryParse(next, out int result))
                        {
                            x++;
                        }
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
                }
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
