// Let -- Parse tree node strategy for printing the special form let

using System;

namespace Tree
{
    public class Let : Special
    {
        public Let()
        {
        }

        public override void print(Node t, int n, bool p)
        {
            Printer.printLet(t, n, p);
        }

        private int initialize(Node bindings, Environment env, Environment env2)
        {
            if (bindings.isNull())
                return 0;
            var binding = bindings.getCar();
            if (Node.length(binding) == 2)
            {
                var var1 = binding.getCar();
                var init1 = binding.getCdr().getCar();
                var node = init1.eval(env);
                env2.define(var1, node);
                return initialize(bindings.getCdr(), env, env2);
            }
            return -1;
        }

        private Node evalBody(Node exp, Environment env)
        {
            var car = exp.getCar();
            var node = car.eval(env);
            var cdr = exp.getCdr();
            if (cdr.isNull())
                return node;
            return evalBody(cdr, env);
        }

        public override Node eval(Node exp, Environment env)
        {
            if (Node.length(exp) <= 2)
            {
                Console.Error.WriteLine("Error: invalid let expression");
                return Nil.getInstance();
            }
            var car = exp.getCdr().getCar();
            var cdr = exp.getCdr().getCdr();

            if (Node.length(car) <= 0)
            {
                Console.Error.WriteLine("Error: invalid let expression");
                return Nil.getInstance();
            }

            var env2 = new Environment(env);
            if (initialize(car, env, env2) < 0)
            {
                Console.Error.WriteLine("Error: invalid let expression");
                return Nil.getInstance();
            }
            return evalBody(cdr, env2);
        }
    }
}


