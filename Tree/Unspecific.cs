using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tree
{
    class Unspecific : Node
    {
        private static Unspecific instance = new Unspecific();

        public static Unspecific getInstance()
        {
            return Unspecific.instance;
        }

        public override void print(int n)
        {
            //TODO: implement
        }

    }
}
