// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
    public class Define : Special
    {
        public Define()
        {
        }

        public override void print(Node t, int n, bool p)
        {
            Printer.printDefine(t, n, p);
        }

        public override Node eval(Node exp, Environment env)
        {
            int num = Node.length(exp);
            if (num <= 2)
            {
                Console.Error.WriteLine("Error: invalid define expression");
                return Nil.getInstance();
            }
            Node car1 = exp.getCdr().getCar();
            if (car1.isSymbol() && num == 3)
            {
                Node car2 = exp.getCdr().getCdr().getCar();
                env.define(car1, car2.eval(env));
                return Void.getInstance();
            }
            if (!car1.isPair())
            {
                Console.Error.WriteLine("Error: invalid expression");
                return (Node)Nil.getInstance();
            }
            Node car3 = car1.getCar();
            Node cdr1 = car1.getCdr();
            Node cdr2 = exp.getCdr().getCdr();
            if (!car3.isSymbol() || !isValid(cdr1))
            {
                Console.Error.WriteLine("Error: ill-formed definition");
                return (Node)Nil.getInstance();
            }
            Node node = (Node)new Cons((Node)new Ident("lambda"), (Node)new Cons(cdr1, cdr2));
            env.define(car3, node.eval(env));
            return (Node)Void.getInstance();
        }

        private bool isValid(Node paramList)
        {
            return paramList.isNull() || paramList.isSymbol() || paramList.isPair() && paramList.getCar().isSymbol() && isValid(paramList.getCdr());
        }
    }
}


