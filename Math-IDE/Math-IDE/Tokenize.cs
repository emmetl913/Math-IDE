using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Math_IDE
{
    /// <summary>
    /// Returns a string with each word separated by a newline character.
    internal class Tokenize
    {
        public string[] TokenizeText(string text)
        {
            string pattern = @"[a-zA-Z_][a-zA-Z0-9_]*|[0-9]+|[=;,]|[\[\]\(\)\{\}]";
            
            MatchCollection matches = Regex.Matches(text, pattern);

            string[] tokenizedText = new string[text.Length];
            int index = 0;
            foreach (Match match in matches)
            {
                tokenizedText[index] = match.Value;
                index++;
            }
            Debug.WriteLine("Text successfully tokenized!");
            return tokenizedText;
        }   
    }
}
