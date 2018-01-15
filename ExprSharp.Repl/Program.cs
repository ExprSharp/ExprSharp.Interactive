using CommandLine;
using ExprSharp.Runtime;
using System;
using System.IO;

namespace ExprSharp.Repl
{

    [Verb("exit", HelpText = "exit")]
    class ExitOption
    {
    }

    [Verb("cls", HelpText = "clear console")]
    class ClsOption
    {
    }

    [Verb("now")]
    class NowOption
    {
        [Value(0, Default = null)]
        public string CurrentFile { get; set; }
    }

    /*[Verb("build", HelpText = "build code")]
    class BuildOption
    {
    }*/

    [Verb("run")]
    class RunOption
    {
    }

    [Verb("clear")]
    class ClearOption
    {
        
    }

    [Verb("view")]
    class ViewOption
    {

    }


    class Program
    {
        static string CurrentPath = "";
        static string currentCode = "";
        static ESRuntime runtime;
        static Parser parser = new Parser(config => { config.HelpWriter = null; });

        static void printHead()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("ExprSharp " + CurrentPath + " ");
            Console.WriteLine();
        }


        static void printFront()
        {
            if (String.IsNullOrEmpty(currentCode) == false)
            {
                Console.Write(currentCode + " ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("> ");
        }

        static void _main(string path)
        {
            Console.ForegroundColor = ConsoleColor.White;

            System.IO.Directory.SetCurrentDirectory(path);
            CurrentPath = System.IO.Directory.GetCurrentDirectory();
            
            printHead();
            runtime = new ESRuntime();

            while (true)
            {
                printFront();
                string cmd = Console.ReadLine();
                try
                {
                    Work_Loaded(cmd);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error {ex.Message}.");
                }
            }
        }

        static void Main(string[] args)
        {
            if (args.Length != 0) System.IO.Directory.SetCurrentDirectory(args[0]);

            _main(System.IO.Directory.GetCurrentDirectory());
        }
        

        static bool Confirm(string tip)
        {
            Console.Write($"{tip} (y/n):");
            if (Console.ReadLine() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void YesLine(string tip)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(tip);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void WaringLine(string tip)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(tip);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static object run(string code)
        {
            try
            {
                return runtime.Execute(code);
            }
            catch (Exception ex)
            {
                WaringLine(ex.Message);
                return null;
            }
        }

        static void Work_Loaded(string cmd)
        {

            parser.ParseArguments<RunOption,ViewOption ,ClearOption,NowOption, ExitOption, ClsOption>(cmd.Split(' ')).
                    WithParsed<RunOption>(r =>
                    {
                        var path = Directory.GetCurrentDirectory() + "/" + currentCode;
                        if (File.Exists(path))
                        {
                            var v = run(File.ReadAllText(path));
                            if (v != null) Console.WriteLine(v.ToString());
                        }
                        else
                        {
                            WaringLine("No files.");
                        }
                    }).
                    WithParsed<ClearOption>(ne =>
                    {
                        runtime = new ESRuntime();
                    }).
                    WithParsed<NowOption>(se => {
                        if (se.CurrentFile != null) { currentCode = se.CurrentFile; }
                    }).
                    WithParsed<ExitOption>(exit =>
                    {
                        Environment.Exit(0);
                    }).
                    WithParsed<ClsOption>(cls =>
                    {
                        Console.Clear();
                        printHead();
                    }).
                    WithParsed<ViewOption>(cls =>
                    {
                        foreach(var v in runtime.EvalEnvironment.Variables)
                        {
                            Console.Write(v.Key + ", ");
                        }
                        Console.WriteLine();
                    }).
                    WithNotParsed(errs =>
                    {
                        var v = run(cmd);
                        if (v != null) Console.WriteLine(v.ToString());
                    });
        }
    }
}
