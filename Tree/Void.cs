using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tree
{
    public class Void : Node
    {
        private static Void instance = new Void();

        private Void()
        {
        }

        public static Void getInstance()
        {
            return instance;
        }

        public override void print(int n)
        {
        }
    }
}
