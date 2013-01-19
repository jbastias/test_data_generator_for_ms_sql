#region License
//
// Your Project Name Here: Program.cs
//
// Author:
//   Your Name Here (insert-your@email.here)
//
// Copyright (C) 2012 Your Name Here
//
// [License Body Here]
#endregion
#region Using Directives
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using CommandLine;
using CommandLine.Text;
using datatool;

#endregion

namespace CSharpTemplate
{
    sealed class Program
    {
        sealed class Options : CommandLineOptionsBase
        {
            
            [Option("t", "text", HelpText = "text value here")]
            public string TextValue { get; set; }

            [Option("n", "numeric", HelpText = "numeric value here")]
            public double NumericValue { get; set; }

            [Option("b", "bool", HelpText = "on|off switch here")]
            public bool BooleanValue { get; set; }

            [HelpOption]
            public string GetUsage()
            {

                Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Name);

                var assembly = Assembly.GetExecutingAssembly();

                var help = new HelpText
                {

                    Heading = new HeadingInfo(assembly.GetName().Name, assembly.ImageRuntimeVersion), // new HeadingInfo(ThisAssembly.Title, ThisAssembly.InformationalVersion),
                    Copyright = new CopyrightInfo("me", 2012), //new CopyrightInfo(ThisAssembly.Author, 2012),
                    AdditionalNewLineAfterOption = true,
                    AddDashesToOption = true
                };
                HandleParsingErrorsInHelp(help);
                help.AddPreOptionsLine("<<license details here.>>");
                help.AddPreOptionsLine("Usage: CSharpTemplate -tSomeText --numeric 2012 -b");
                help.AddOptions(this);

                return help;
            }

            void HandleParsingErrorsInHelp(HelpText help)
            {
                if (this.LastPostParsingState.Errors.Count > 0)
                {
                    var errors = help.RenderParsingErrorsText(this, 2); // indent with two spaces
                    if (!string.IsNullOrEmpty(errors))
                    {
                        help.AddPreOptionsLine(string.Concat(Environment.NewLine, "ERROR(S):"));
                        help.AddPreOptionsLine(errors);
                    }
                }
            }
        }

        static void RunCommand(string command)
        {
            var commands = new List<string> {"create-test-data", "create-data-models"};
            if(commands.Any(command.Contains))
            {
                Console.WriteLine("yes");



            }
            else
            {
                Console.WriteLine("no");
            }


            //commands.ForEach();
            //if (commands)

            //if (command)

        }




        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLineParser.Default.ParseArguments(args, options))
            {


                   new DataStorer(new DataSettings()).Run();

                //RunCommand(args[0]);

                //Console.WriteLine("t|ext: " + options.TextValue);
                //Console.WriteLine("n|umeric: " + options.NumericValue.ToString());
                //Console.WriteLine("b|ool: " + options.BooleanValue.ToString().ToLower());
            }
        }

    }
}