using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineDemo
{
    public class Helper
    {
        public static bool TryParseToInteger(string input)
        {
            int output = default(int);

            try
            {
                output = int.Parse(input);
                return true;
            }
            catch
            {
                output = default(int);
                return false;
            }
        }
    }
}
