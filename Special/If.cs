// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree
{
    public class If : Special
    {
        public If()
        {
        }

        public override void print(Node t, int n, bool p)
        {
            Printer.printIf(t, n, p);
        }

        //syntax: if <test> <consequent> <alternate>
        //syntax: if <test> <consequent>
  
        public override Node eval(Node exp, Environment env)
        {
            var length = Node.length(exp);
            if (length < 3 || length > 4)
            {
                Console.Error.WriteLine("Error: Invalid length for if expression");
                return Nil.getInstance();
            }
            var test = exp.getCdr().getCar();
            var consequent = exp.getCdr().getCdr().getCar();

            var alternate = new Node();
            if (length == 4) //has alternates
                alternate = exp.getCdr().getCdr().getCdr().getCar();
            else //If <test> yields a false value and no <alternate> is specified, then the result of the expression is unspecified.
                alternate = Unspecific.getInstance();

            if (test.eval(env) != BoolLit.getInstance(false))
                return consequent.eval(env);
            return alternate.eval(env);
        }
    }
}

