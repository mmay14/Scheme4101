using System;

namespace Tree
{
    class Unspecific : Node
    {
        private static Unspecific instance = new Unspecific();

        public static Unspecific getInstance()
        {
            return instance;
        }

        public override void print(int n)
        {
            for (var index = 0; index < n; ++index)
                Console.Write(' ');
            Console.Write("#{Unspecified}");
            if (n < 0)
                return;
            Console.WriteLine();
        }

    }
}
