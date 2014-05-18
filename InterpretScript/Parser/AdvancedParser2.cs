﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterpretScript.advanced_interpreter2;

namespace InterpretScript.parser
{
    class AdvancedParser2
    {
        string[] codeListArray = { };
        string ifPattern = @"^\Aif\(\w*\)\{\w*\}";
        string forPattern = @"^\Afor\(\w*\)\{w*\}";
        string whilePattern = @"^\Awhile\(\w*\)\{\w*\}";
        string expressionPattern = @"^[A-Za-z0-9=]*;{1,1}";

        string input;
        string resultString = "";
        Dictionary<string, int> listInt = new Dictionary<string, int>();

        public AdvancedParser2(string input)
        {
            this.input = input;
            splitIntoPhrases();
        }
        ///<summary>
        ///Dzieli wpisany kod na wyrażenia if, while, rozdzielone średnikiem lub {} i zapisuje do listy
        /// </summary>
        public void splitIntoPhrases()
        {
            int start = 0;
            this.input = input.Replace("\r\n", "");
            this.input = input.Replace("\r", "");
            this.input = input.Replace("\n", "");
            this.input = input.Replace("\0", "");
            this.input = input.Replace("\t", "");
            while (start < input.Length - 2)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(input.Substring(start, input.Length - start), ifPattern))
                {
                    System.Text.RegularExpressions.Match result = System.Text.RegularExpressions.Regex.Match(input.Substring(start, input.Length - start), ifPattern);
                    Array.Resize(ref this.codeListArray, this.codeListArray.Length + 1);
                    this.codeListArray[this.codeListArray.Length - 1] = result.Groups[0].Value;
                    start += System.Text.RegularExpressions.Regex.Matches(input.Substring(start, input.Length - start), ifPattern)[0].Length;
                    this.resultString += "if";
                    
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(input.Substring(start, input.Length - start), forPattern))
                {
                    System.Text.RegularExpressions.Match result = System.Text.RegularExpressions.Regex.Match(input.Substring(start, input.Length - start), forPattern);
                    Array.Resize(ref this.codeListArray, this.codeListArray.Length + 1);
                    this.codeListArray[this.codeListArray.Length - 1] = result.Groups[0].Value;
                    start += System.Text.RegularExpressions.Regex.Matches(input.Substring(start, input.Length - start), forPattern)[0].Length;
                    this.resultString += "for";
                    
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(input.Substring(start, input.Length - start), whilePattern))
                {
                    System.Text.RegularExpressions.Match result = System.Text.RegularExpressions.Regex.Match(input.Substring(start, input.Length - start), whilePattern);
                    Array.Resize(ref this.codeListArray, this.codeListArray.Length + 1);
                    this.codeListArray[this.codeListArray.Length - 1] = result.Groups[0].Value;
                    start += System.Text.RegularExpressions.Regex.Matches(input.Substring(start, input.Length - start), whilePattern)[0].Length;
                    this.resultString += "while";
                    
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(input.Substring(start, input.Length - start), expressionPattern))
                {
                    System.Text.RegularExpressions.Match result = System.Text.RegularExpressions.Regex.Match(input.Substring(start, input.Length - start), expressionPattern);
                    Array.Resize(ref this.codeListArray, this.codeListArray.Length + 1);
                    int start2 = start + System.Text.RegularExpressions.Regex.Matches(input.Substring(start, input.Length - start), expressionPattern)[0].Length;
                    this.codeListArray[this.codeListArray.Length - 1] = input.Substring(start, start2-start);
                    start = start2;
                    this.resultString += ";";
                    
                }
                else
                {
                    this.resultString += "Błąd";
                    start = input.Length;
                }
            }
        }
        /// <summary>
        /// Zwraca komendy dla interpretera
        /// </summary>
        /// <returns></returns>
        public string getResults()
        {

            for (int i = 0; i < this.codeListArray.Length; i++)
            {
                Console.WriteLine(this.codeListArray[i]);
            }
            return this.resultString;
        }
    }
}