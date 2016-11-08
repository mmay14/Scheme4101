// Cond -- Parse tree node strategy for printing the special form cond

using System;

namespace Tree
{
    public class Cond : Special
    {
        public Cond()
        {
        }

        public override void print(Node t, int n, bool p)
        {
            Printer.printCond(t, n, p);
        }

        private Node evalExp(Node exp, Environment env)
        {
            var car = exp.getCar();
            var node = car.eval(env);
            var cdr = exp.getCdr();
            if (cdr.isNull())
                return node;
            return evalExp(cdr, env);
        }

        private Node evalClauses(Node exp, Environment env)
        {
            if (exp.isNull())
                return Unspecific.getInstance();
            var clause = exp.getCar();
            if (Node.length(clause) <= 0)
            {
                Console.Error.WriteLine("Error: invalid cond expression");
                return Nil.getInstance();
            }

            var testExp = clause.getCar();
            var expression = clause.getCdr();
            if (testExp.isSymbol() && testExp.getName().Equals("else"))
            {
                
            }
            var testValid = testExp.eval(env);
            if (testValid == BoolLit.getInstance(false))
            {
                var nextExps = exp.getCdr();
                return evalClauses(nextExps,env);
            }
            if (expression.isNull())
                return BoolLit.getInstance(true);
            var exp1 = expression.getCar();
            if(!exp1.isSymbol() || !exp1.getName().Equals("=>"))
                return evalExp(expression, env);
            if (Node.length(expression) != 2)
            {
                Console.Error.WriteLine("Error: invalid cond expression");
                return Nil.getInstance();
            }
            var cdr = expression.getCdr().getCar();
            return cdr.eval(env).apply(BoolLit.getInstance(true));
        }

        public override Node eval(Node exp, Environment env)
        {
            if (Node.length(exp) < 2)
            {
                Console.Error.WriteLine("Error: invalid cond expression");
                return Nil.getInstance();
            }
            var exps = exp.getCdr();
            return evalClauses(exps, env);
        }
    }
}


