// Closure.java -- the data structure for function closures

// Class Closure is used to represent the value of lambda expressions.
// It consists of the lambda expression itself, together with the
// environment in which the lambda expression was evaluated.

// The method apply() takes the environment out of the closure,
// adds a new frame for the function call, defines bindings for the
// parameters with the argument values in the new frame, and evaluates
// the function body.

using System;

namespace Tree
{
    public class Closure : Node
    {
        private Node fun;		// a lambda expression
        private Environment env;	// the environment in which
                                        // the function was defined

        public Closure(Node f, Environment e)	{ fun = f;  env = e; }

        public Node getFun()		{ return fun; }
        public Environment getEnv()	{ return env; }

        public  override  bool isProcedure()	{ return true; }

        public override void print(int n) {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.WriteLine("#{Procedure");
            if (fun != null)
                fun.print(Math.Abs(n) + 4);
            for (int i = 0; i < Math.Abs(n); i++)
                Console.Write(' ');
            Console.WriteLine('}');
        }

        private static void assignParams(Node parameters, Node args, Environment env)
        {
            if (parameters.isNull() && args.isNull())
                return;
            if (parameters.isNull() || args.isNull())
                Console.Error.WriteLine("Error: number of arguments do not match number of parameters");
            else if (parameters.isSymbol())
                env.define(parameters, args);        
            else if (parameters.isPair() && args.isPair())
            {
                env.define(parameters.getCar(), args.getCar());
                assignParams(parameters.getCdr(), args.getCdr(), env);
            }
            else
                Console.Error.WriteLine("Error: invalid closure");
        }

        private Node evalFunc(Node exp, Environment env)
        {
            var car = exp.getCar();
            var node = car.eval(env);
            var cdr = exp.getCdr();
            if (cdr.isNull())
                return node;
            return evalFunc(cdr, env);
        }

        public override Node apply(Node args)
        {
            var car = fun.getCdr().getCar();
            var cdr = fun.getCdr().getCdr();
            env = new Environment(env);
            assignParams(car, args, env);
            return evalFunc(cdr, env);
        }
    }    
}
