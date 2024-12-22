using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_IDE
{
    /// <summary>
    /// Returns a string with each word separated by a newline character.
    internal class Tokenize
    {
        public string TokenizeText(string text)
        {
            string[] tokens = text.Split(' ');
            string tokenizedText = "";
            foreach (string token in tokens)
            {
                tokenizedText += token + "\n";
            }
            Console.WriteLine(tokenizedText);
            return tokenizedText;
        }   
    }
}
