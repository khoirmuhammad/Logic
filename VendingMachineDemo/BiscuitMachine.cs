using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineDemo.Contracts;

namespace VendingMachineDemo
{
    public class BiscuitMachine
    {
        private int _depositMoney { get; set; } // store money deposit

        private bool _isTransactionEnd { get; set; } // flag for each transaction

        private readonly IBiscuitItem _biscuitItem;
        public BiscuitMachine()
        {
            _depositMoney = 0;
            _biscuitItem = new BiscuitItem();
        }

        /*
         * in order to allow transaction (true : not allowed, false : allowed)
         */
        public void AllowTransaction()
        {
            _isTransactionEnd = false;
        }

        /*
         * Display list of biscuits to client
         */
        public void DisplayBiscuitLists()
        {
            var biscuitItems = _biscuitItem.GetItems();

            // need condition to make sure on the subsequent transaction, the items aren't added again
            if (biscuitItems.Count == 0)
            {
                _biscuitItem.SetItems();
            }

            

            Console.WriteLine("-------------------------- ITEM LIST --------------------------------");
            foreach (var item in biscuitItems)
            {
                Console.WriteLine($"Item Code : {item.ItemCode} - {item.ItemName} - Price : IDR {item.ItemPrice} - Stock {item.Stock}");
            }
            Console.WriteLine("------------------------------------------------------------------------");

            Console.WriteLine();

            if (biscuitItems.Any(x => x.Stock > 0))
            {
                // Inserting money as deposit
                ValidateDeposit("Please insert money as deposit. Allowed money (2000, 5000, 10000, 20000, 50000)", false);
            }
            else
            {
                return;
            }
            
        }

        private void PerformMoneyDeposit(int money)
        {
            int[] acceptableMoney = new int[5] { 2000, 5000, 10000, 20000, 50000 };

            // check the money inserted matches to the machine or not
            if (acceptableMoney.Contains(money))
            {
                _depositMoney += money;

                Console.WriteLine("Select Item Code");
                PerformItemSelection(Convert.ToChar(Console.ReadLine().ToUpper()));

            }
            else
            {
                ValidateDeposit("Please insert allowed money (2000, 5000, 10000, 20000, 50000)", false);               
            }
        }

        private void CheckReturnExceededMoney(BiscuitItem item)
        {
            int returnMoney = _depositMoney - item.ItemPrice;

            if (returnMoney > 0)
            {
                Console.WriteLine($"Your change {_depositMoney - item.ItemPrice}");
            }

            _biscuitItem.UpdateItem(item.ItemCode);
            _depositMoney = _depositMoney - (item.ItemPrice + returnMoney);
            _isTransactionEnd = true;

            Console.WriteLine($"Thank for buying {item.ItemName}");
        }

        private void PerformItemSelection(char itemCode)
        {
            bool isSuccessSelection = false;

            BiscuitItem item = new BiscuitItem();

            while (!isSuccessSelection & !_isTransactionEnd)
            {
               bool isHasQty = false;

                do
                {
                    item = _biscuitItem.GetItems().FirstOrDefault(x => x.ItemCode.Equals(itemCode));

                    // item code input is exist on list or not
                    if (item == null)
                    {
                        Console.WriteLine("Please insert appropriate item code");
                        itemCode = Convert.ToChar(Console.ReadLine().ToUpper());
                    }
                    else
                    {
                        // item selected has enough stock or not
                        if (_biscuitItem.GetItems().Where(x => x.ItemCode.Equals(itemCode)).FirstOrDefault().Stock > 0)
                        {
                            // item selected has higher price than deposit or not
                            if (item.ItemPrice > _depositMoney)
                            {
                                ValidateDeposit("Your money doesn't enough, Insert again", true);

                                Console.WriteLine("Select Item Code");
                                itemCode = Convert.ToChar(Console.ReadLine().ToUpper());
                            }
                            else
                            {
                                CheckReturnExceededMoney(item);
                                isHasQty = true;
                                isSuccessSelection = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Empty stock. Please change the selection");
                            itemCode = Convert.ToChar(Console.ReadLine().ToUpper());
                        }
                    }

                    

                } while (!isHasQty);

                

            }
        }

        private void ValidateDeposit(string message, bool hasSelectItem)
        {
            bool isInteger = false;
            int money = 0;
            while (!isInteger)
            {
                Console.WriteLine(message);
                var input = Console.ReadLine();
                isInteger = Helper.TryParseToInteger(input);

                money = isInteger ? Convert.ToInt32(input) : 0;

                if (isInteger && hasSelectItem)
                    _depositMoney += money;
                else if (isInteger && !hasSelectItem)
                    PerformMoneyDeposit(money);
            }
        }
    }
}
