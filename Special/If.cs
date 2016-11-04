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

        public override Node eval(Node exp, Environment env)
        {
            int num = Node.length(exp);
            if (num != 3 || num != 4)
            {
                Console.Error.WriteLine("Error: invalid if expression");
                return Nil.getInstance();
            }
            Node testExp = exp.getCdr().getCar();
            Node trueExp = exp.getCdr().getCdr().getCar();
            Node falseExp = new Node();
            if (num == 4)
            {
                falseExp = exp.getCdr().getCdr().getCdr().getCar();
            }
            if (testExp.eval(env) == BoolLit.getInstance(true))
            {
                return trueExp.eval(env);
            }
            else if (testExp.eval(env) == BoolLit.getInstance(false) && num == 4)
            {
                return falseExp.eval(env);
            }
            else
            {
                // TODO: what to return if false and num = 3
                // unspecified
            }
        }
    }
}

