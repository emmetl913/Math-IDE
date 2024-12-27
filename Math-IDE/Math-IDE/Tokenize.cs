using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Math_IDE
{
    /// <summary>
    /// Returns a string with each word separated by a newline character.
    internal class Tokenize
    {
        public string TokenizeText(string text)
        {
            string pattern = @"[a-zA-Z_][a-zA-Z0-9_]*|[0-9]+|[=;,]|[\[\]\(\)\{\}]";
            
            MatchCollection matches = Regex.Matches(text, pattern);

            string tokenizedText = "";
            foreach (Match match in matches)
            {
                tokenizedText += match.Value + "\n";
            }

            return tokenizedText;
        }   
    }
}
