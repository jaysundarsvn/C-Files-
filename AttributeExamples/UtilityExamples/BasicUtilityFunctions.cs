using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace UtilityFunctions
{
    public class BasicUtilityFunctions
    {
        public string WriteWelcomeMessage()
        {
            return "Welcome to 'Basic Utility' Functions";
        }

        public int IntegerPlusInteger(int operand1, int operand2) 
        {
            return operand1 + operand2;
        }

        public string ConcatThreeString(string str1, string str2, string str3) 
        {
            return str1 + "" + str2 + " " + str3;
        }

        public int GetStringLength(string str)
        {
            return str.Length;
        }
    }
}