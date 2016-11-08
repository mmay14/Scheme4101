// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree
{
    public class Set : Special
    {
        public Set()
        {
        }

        public override void print(Node t, int n, bool p)
        {
            Printer.printSet(t, n, p);
        }

        public override Node eval(Node exp, Environment env)
        {
            if (Node.length(exp) != 3)
            {
                Console.Error.WriteLine("Error: invalid set expression");
                return (Node) Nil.getInstance();
            }        
            Node var = exp.getCdr().getCar();
            Node varExp = exp.getCdr().getCdr().getCar();
            env.assign(var, varExp.eval(env));
            return Unspecific.getInstance();
        }
    }
}

