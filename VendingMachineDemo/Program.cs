using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            BiscuitMachine biscuitMachine = new BiscuitMachine();

            bool isTransactionAgain = true;

            while(isTransactionAgain)
            {
                biscuitMachine.AllowTransaction();
                biscuitMachine.DisplayBiscuitLists();

                Console.WriteLine("Any other trasaction? [Y/N]");

                if (Convert.ToChar(Console.ReadLine().ToUpper()) != 'Y')
                {
                    isTransactionAgain = false;
                }

            }

            Console.WriteLine("Transaction Ends");
            Console.ReadKey();


        }
    }
}
