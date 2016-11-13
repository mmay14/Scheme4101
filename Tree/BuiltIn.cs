// BuiltIn -- the data structure for built-in functions

// Class BuiltIn is used for representing the value of built-in functions
// such as +.  Populate the initial environment with
// (name, new BuiltIn(name)) pairs.

// The object-oriented style for implementing built-in functions would be
// to include the C# methods for implementing a Scheme built-in in the
// BuiltIn object.  This could be done by writing one subclass of class
// BuiltIn for each built-in function and implementing the method apply
// appropriately.  This requires a large number of classes, though.
// Another alternative is to program BuiltIn.apply() in a functional
// style by writing a large if-then-else chain that tests the name of
// the function symbol.

using System;
using System.IO;
using Parse;

namespace Tree
{
    public class BuiltIn : Node
    {
        private Node symbol;            // the Ident for the built-in function

        public BuiltIn(Node s)		{ symbol = s; }

        public Node getSymbol()		{ return symbol; }

        public  override  bool isProcedure()	{ return true; }

        public override void print(int n)
        {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.Write("#{Built-in Procedure ");
            if (symbol != null)
                symbol.print(-Math.Abs(n));
            Console.Write('}');
            if (n >= 0)
                Console.WriteLine();
        }

        public override Node apply(Node args)
        {
            int num = Node.length(args);
            if (num > 2)
                Console.Error.WriteLine("Error: too many arguments");
            else if (num == 0)
                return apply0();
            else if (num == 1)
                return apply1(args.getCar());
            return apply2(args.getCar(), args.getCdr().getCar());
        }

        private Node apply0()
        {
            string name = symbol.getName();
            if (name.Equals("read"))
            {
                var parser = new Parser(new Scanner(Console.In), new TreeBuilder());
                var exp = (Node) parser.parseExp();
                if (exp != null)
                    return exp;
                return new Ident("end-of-file");
            }
            if (name.Equals("newline"))
            {
                Console.WriteLine();
                return Unspecific.getInstance();
            }
            if (name.Equals("interaction-environment"))
                return Scheme4101.env;
            Console.Error.WriteLine("Error: wrong number of arguments");
            return Nil.getInstance();
        }

        private Node apply1(Node arg1)
        {
            string name = symbol.getName();
            if (name.Equals("car"))
                return arg1.getCar();
            if (name.Equals("cdr"))
                return arg1.getCdr();
            if (name.Equals("number?"))
                return BoolLit.getInstance(arg1.isNumber());
            if (name.Equals("symbol?"))
                return BoolLit.getInstance(arg1.isSymbol());
            if (name.Equals("null?"))
                return BoolLit.getInstance(arg1.isNull());
            if (name.Equals("pair?"))
                return BoolLit.getInstance(arg1.isPair());
            if (name.Equals("procedure?"))
                return BoolLit.getInstance(arg1.isProcedure());
            if (name.Equals("write"))
            {
                arg1.print(-1);
                return Unspecific.getInstance();
            }
            if (name.Equals("display"))
            {
                Console.Write(arg1);
                return Unspecific.getInstance();
            }
            Console.Error.WriteLine("Error: wrong number of arguments");
            return Nil.getInstance();
        }

        private Node apply2(Node arg1, Node arg2)
        {
            string name = symbol.getName();
            if (name.Equals("eq?"))
            {
                if (arg1.isSymbol() && arg2.isSymbol())
                    return BoolLit.getInstance(arg1.getName().Equals(arg2.getName()));
                return BoolLit.getInstance(arg1 == arg2);
            }
            if (name.Equals("cons"))
                return new Cons(arg1, arg2);
            if (name.Equals("set-car!"))
            {
                arg1.setCar(arg2);
                return Unspecific.getInstance();
            }
            if (name.Equals("set-cdr!"))
            {
                arg1.setCdr(arg2);
                return Unspecific.getInstance();
            }
            if (name.Equals("eval"))
            {
                if (arg2.isEnvironment())
                {
                    Environment envArg1 = (Environment)arg1;
                    return arg1.eval(envArg1);
                }
                Console.Error.WriteLine("Error: argument is not an environment");
                return Nil.getInstance();
            }
            if (name.Equals("apply"))
                return arg1.apply(arg2);
            if (name[0].Equals('b') && name.Length == 2)
            {
                if (arg1.isNumber() && arg2.isNumber())
                    return evalArithmetic(arg1.getIntVal(), arg2.getIntVal());
                Console.Error.WriteLine("Error: invalid arguments");
                return Nil.getInstance();
            }
            Console.Error.WriteLine("Error: wrong number of arguments");
            return Nil.getInstance();
        }

        private Node evalArithmetic(int arg1, int arg2)
        {
            string name = symbol.getName();
            if (name.Equals("b+"))
                return new IntLit(arg1 + arg2);
            if (name.Equals("b-"))
                return new IntLit(arg1 - arg2);
            if (name.Equals("b*"))
                return new IntLit(arg1 * arg2);
            if (name.Equals("b/"))
                return new IntLit(arg1 / arg2);
            if (name.Equals("b="))
                return BoolLit.getInstance(arg1 == arg2);
            if (name.Equals("b<"))
                return BoolLit.getInstance(arg1 < arg2);
            Console.Error.WriteLine("Error: unknown arithmetic symbol");
            return Nil.getInstance();
        }
    }    
}

