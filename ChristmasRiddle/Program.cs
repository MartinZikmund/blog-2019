using System;
using System.Collections.Generic;

namespace Riddle
{
    class Program
    {
        static void Main(string[] args)
        {
            var valuesArray = new ValueHolder[] { new ValueHolder() };
            valuesArray[0].SetValue(5);

            var valuesList = new List<ValueHolder>() { new ValueHolder() };
            valuesList[0].SetValue(5);

            Console.WriteLine(valuesList[0].Value == valuesArray[0].Value);
        }
    }

    public struct ValueHolder
    {
        public int Value { get; private set; }

        public void SetValue(int value) => Value = value;
    }
}
