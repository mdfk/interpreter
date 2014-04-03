﻿using InterpretScript.interpreter;
using InterpretScript.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpretScript
{
    class Script
    {
        string Result;
        string Source;

        public Script()
        {

        }

        public string GetResult
        {
            get
            {
                return this.Result;
            }
        }

        public void RunScript(string Source)
        {
            this.Source = Source;

            //int value = listInt["x"];

            /*
             * Odpalcie sobie tą zakomentowaną część jeżeli checie przetestować wyrażenia typu 1+1, 3*2 itd.
            Parser parser = new Parser(Source);
            Expression exp = parser.parseExpression();
            this.Result = exp.getValue().ToString();
            */
            AdvancedParser advParser = new AdvancedParser(Source);
            this.Result = "good";
        }
    }
}
