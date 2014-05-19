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
        List<Variables> ListVariables;

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

        string RunCode(string Source)
        {
            string Result = string.Empty;

            AdvancedParser2 Parser = new AdvancedParser2(Source);
            List<string> ListString = Parser.GetListSource();
            
            ParserVariables parserVariables = new ParserVariables(ListVariables);

            ParserTest ParserT = new ParserTest();
            //List<string> ListString = new List<string>();
            //ListString.Add(Source);

            foreach (var list in ListString)
            {
                if (ParserT.CheckFunctionFor(list))
                {
                    int From = Convert.ToInt32(ParserT.GetFirstAttributeFor(list));
                    int To = Convert.ToInt32(ParserT.GetSecondAttributeFor(list));
                    int Inc = Convert.ToInt32(ParserT.GetThirdAttribute(list)+"1");
                    bool less = ParserT.GetLessAttributeFor(list);
                    string forSource = ParserT.GetSourceFor(list);
                    
                    if(less)
                    {
                        for (int i = From; i < To; i = i + Inc)
                        {
                            Result += RunCode(forSource);
                        }
                    }
                    else
                    {
                        for (int i = From; i > To; i = i + Inc)
                        {
                            Result += RunCode(forSource);
                        }
                    }
                }
                else if (ParserT.CheckPrintText(list))
                {
                    Result += ParserT.GetPrintText(list);
                }
                else if (ParserT.CheckPrintVariables(list))
                {
                    string varT = ParserT.GetPrintVariables(list);
                    Result += ListVariables.Single(i => i.Name == varT).Value;
                }
                else if (ParserT.CheckPrintlText(list))
                {
                    Result += ParserT.GetPrintText(list) + "\n";
                }
                else if (ParserT.CheckPrintVariables(list))
                {
                    string varT = ParserT.GetPrintVariables(list);
                    Result += ListVariables.Single(i => i.Name == varT).Value;
                }
                else if (ParserT.CheckPrintlVariables(list))
                {
                    string varT = ParserT.GetPrintVariables(list);
                    Result += ListVariables.Single(i => i.Name == varT).Value + "\n";
                }
                else if (ParserT.CheckCreateVariable(list))
                {
                    parserVariables.ParseNewVariables(ParserT.GetCreateVariable(list));
                }
                else if (ParserT.CheckAttributeVariable(list))
                {
                    parserVariables.ParseVariables(ParserT.GetAttributeVariable(list));

                    string getName = ParserT.GetNameVariable(list);

                    Parser parser = new Parser(ListVariables.Single(i => i.Name == getName).Value);
                    Expression exp = parser.parseExpression();
                    ListVariables.Single(i => i.Name == getName).Value = exp.getValue().ToString();
                }
            }

            return Result;
        }

        public void RunScript(string Source)
        {
            this.Source = Source;
            this.Result = string.Empty;

            this.ListVariables = new List<Variables>();

            this.Result += RunCode(Source);

            //this.Result += RunCode("for(i=0; i<5; i++) {printl(\"tekst\"); }");
            //this.Result += RunCode("  int   a   =   5;");
            //this.Result += RunCode(" int   b   =   2;");
            //this.Result += RunCode("   printl  (  \"   tekst   \"  )  ;  ");
            //this.Result += RunCode("   printl  (  \"   a+b  \"  )  ;  ");
            //this.Result += RunCode("  printl   (  a   )  ;  ");
            //this.Result += RunCode("    a    =   6   ;  ");
            //this.Result += RunCode("printl  (  a  )    ;  ");
            //this.Result += RunCode("   a   =   a   +   b   ;  ");
            //this.Result += RunCode("  printl   (  a   )   ;");
            //this.Result += RunCode("for(i=3; i>0; i--){printl(\"tekst\");}");
        }
    }
}
