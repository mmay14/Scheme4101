// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree
{
    public class Regular : Special
    {
        public Regular()
        {
        }

        public override void print(Node t, int n, bool p)
        {
            Printer.printRegular(t, n, p);
        }

        public override Node eval(Node exp, Environment env)
        {
            
            if (Node.length(exp) < 1)
            {
                Console.Error.WriteLine("Error: invalid regular expression");
                return Nil.getInstance();
            }
            var car = exp.getCar();
            var cdr = exp.getCdr();
            return car.eval(env).apply(buildArgsList(cdr, env));
        }

        private Node buildArgsList(Node exp, Environment env)
        {
            if (!exp.isNull())
            {
                var car = exp.getCar();
                var cdr = exp.getCdr();
                return new Cons(car.eval(env), buildArgsList(cdr, env));
            }
            return Nil.getInstance();
        }
    }
}


