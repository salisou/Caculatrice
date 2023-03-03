using System;
using System.Collections.Generic;
using System.Text;

namespace Caculatrice.Models
{
    public class Calculator : ICalculator
    {
        public string AddSymbol { get; }
        public string SubstractSymbol { get; }
        public string MultiplySymbol { get; }
        public string DivideSymbol { get; }
        public string SqrtSymbol { get; }
        public decimal Number1 { get; set; }
        public decimal Number2 { get; set; }

        public Calculator()
        {
            AddSymbol = "+";
            SubstractSymbol = "-";
            MultiplySymbol = "*";
            DivideSymbol = "÷";
            SqrtSymbol = "√";
        }

        public decimal Calculate(string operation)
        {
            decimal result = 0;

            if (operation == AddSymbol)
            {
                result = Number1 + Number2;
            }
            else if (operation == SubstractSymbol)
            {
                result = Number1 - Number2;
            }
            else if (operation == MultiplySymbol)
            {
                result = Number1 * Number2;
            }
            else if (operation == DivideSymbol)
            {
                if (Number2 != 0)
                    result = Number1 / Number2;
            }
            else if (operation == SqrtSymbol)
            {
                if (Number1 > 0)
                    result = (decimal)Math.Sqrt((double)Number1);
            }

            return result;
        }
    }
}
