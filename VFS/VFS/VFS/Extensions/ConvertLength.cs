// ------------------------------------------------------------------------
// Extensions.cs written by Code A Software (http://www.seite.bplaced.net)
// All rights reserved
// Created on:      11.04.2016
// Last update on:  01.08.2016
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFS.Extensions
{
    public class ConvertLength
    {
        public enum Type_
        {
            B = 0,
            KB = 1,
            MB = 2,
            GB = 3,
            TB = 4
        }

        public struct Item
        {
            public double Length;
            public Type_ Type;

            public Item(double Length, Type_ Type)
            {
                this.Length = Length;
                this.Type = Type;
            }

            public override string ToString()
            {
                return this.Length + " " + this.Type.ToString();
            }
        }

        public static Item Calculate(double value)
        {
            // Get right unit prefix
            int index = 0;
            double nValue = value;

            while (nValue > 1024.0)
            {
                nValue /= 1024.0;
                index++;
            }

            return new Item(Math.Round(value / Math.Pow(1024, index), 2), (Type_)index);
        }

        public static Item Calculate(Item source, Type_ type)
        {
            // Calculate difference:
            int difference = (int)source.Type - (int)type;
            return new Item(Math.Round(difference < 0 ? source.Length / Math.Pow(1024, (int)Math.Abs(difference)) : source.Length * Math.Pow(1024, (int)Math.Abs(difference)), 2), type);
        }
    }
}
