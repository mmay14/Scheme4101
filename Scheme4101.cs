// SPP -- The main program of the Scheme pretty printer.

using System;
using Parse;
using Tokens;
using Tree;

public class Scheme4101
{

    public static Tree.Environment env = null;

    public static int Main(string[] args)
    {
        
        // Create scanner that reads from standard input
        Scanner scanner = new Scanner(Console.In);
        
        if (args.Length > 1 ||
            (args.Length == 1 && ! args[0].Equals("-d")))
        {
            Console.Error.WriteLine("Usage: mono SPP [-d]");
            return 1;
        }
        
        // If command line option -d is provided, debug the scanner.
        if (args.Length == 1 && args[0].Equals("-d"))
        {
            // Console.Write("Scheme 4101> ");
            Token tok = scanner.getNextToken();
            while (tok != null)
            {
                TokenType tt = tok.getType();

                Console.Write(tt);
                if (tt == TokenType.INT)
                    Console.WriteLine(", intVal = " + tok.getIntVal());
                else if (tt == TokenType.STRING)
                    Console.WriteLine(", stringVal = " + tok.getStringVal());
                else if (tt == TokenType.IDENT)
                    Console.WriteLine(", name = " + tok.getName());
                else
                    Console.WriteLine();

                // Console.Write("Scheme 4101> ");
                tok = scanner.getNextToken();
            }
            return 0;
        }

        // Create parser
        TreeBuilder builder = new TreeBuilder();
        Parser parser = new Parser(scanner, builder);
        Node root;

        string[] builtInFunctions = new string[24]
   {
      "symbol?",
      "number?",
      "b+",
      "b-",
      "b*",
      "b/",
      "b=",
      "b<",
      "car",
      "cdr",
      "cons",
      "set-car!",
      "set-cdr!",
      "null?",
      "pair?",
      "eq?",
      "procedure?",
      "read",
      "write",
      "display",
      "newline",
      "eval",
      "apply",
      "interaction-environment"
   };
        env = new Tree.Environment();
        foreach (var function in builtInFunctions)
        {
            var ident = new Ident(function);
            env.define(ident, new BuiltIn(ident));
        }

        env = new Tree.Environment(env);  

        root = (Node) parser.parseExp();
        while (root != null) 
        {
            Console.Write(">");
            root.eval(env).print(0);
            root = (Node) parser.parseExp();
        }

        return 0;
    }
}
