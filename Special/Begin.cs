// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
    public class Begin : Special
    {
        public Begin()
        {
        }

        public override void print(Node t, int n, bool p)
        {
            Printer.printBegin(t, n, p);
        }

        public override Node eval(Node exp, Environment env)
        {
            if (Node.length(exp) < 2)
            {
                Console.Error.WriteLine("Error: invalid begin expression");
                return Nil.getInstance();
            }
            else
            {
                return begin(exp.getCdr(), env);
            }
        }

        private Node begin(Node exp, Environment env)
        {
            Node car = exp.getCar();
            Node node = car.eval(env);
            Node cdr = exp.getCdr();
            if (cdr.isNull())
                return node;
            return begin(cdr, env);
        }
    }
}

