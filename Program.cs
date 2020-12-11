using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine;

namespace Vending_Machine
{
    public class Program
    {
        public static int Money { get; set; }
        public static int Buy_amount { get; set; }
        public static int Buy_item_stock { get; set; }
        public static int Buy_item_value { get; set; }
        public static string Buy_item_name { get; set; }
        public static List<Item> Fruits = new List<Item>();
        public static StringBuilder history = new StringBuilder(1024);


        public enum Emenu
        {
            None,
            GetMenu,
            BuyItem,
            SetMoney,
            GetMoney,
            AdminLogin,
            History,
            Exit
        }

        static void Main()
        {

            Fruits.Add(new Item(value: 500, stock: 50, name: "Apple"));
            Fruits.Add(new Item(value: 200, stock: 80, name: "Banana"));
            Fruits.Add(new Item(value: 700, stock: 20, name: "Lemon"));
            Fruits.Add(new Item(value: 800, stock: 100, name: "Mikang"));
            Fruits.Add(new Item(value: 1000, stock: 10, name: "Melon"));

            Console.WriteLine("#################################");
            Console.WriteLine("###### Vending Machine 0.3 ######");
            Console.WriteLine("#################################\n");
            Item.GetItems(Fruits);

            //종료전까지 반복 실행
            while (true)
            {
                //제어 메뉴 출력
                Console.WriteLine("1.Show Items  2.Order  3.Charge Money  4.Check Money  5.Admin Login  6.Hsitory 7.Exit \n");
                Console.Write(" \nExecute Order Number: ");
                InputPlusNumber(out int menu_num);
                Emenu menu_select = (Emenu)menu_num;

                switch (menu_select)
                {
                    case Emenu.GetMenu:
                        Item.GetItems(Fruits);
                        break;

                    case Emenu.BuyItem:
                        BuyItem();
                        break;

                    case Emenu.SetMoney:
                        SetMoney();
                        break;

                    case Emenu.GetMoney:
                        Console.WriteLine($"NOW Money: {Money} \n");
                        break;

                    case Emenu.AdminLogin:
                        Admin.Login();
                        break;

                    case Emenu.History:
                        ShowHistory();
                        break;

                    case Emenu.Exit:
                        Console.WriteLine("###########################");
                        Console.WriteLine("########  BYE BYE  ########");
                        Console.WriteLine("###########################");
                        return;

                    default:
                        Console.WriteLine("It is wrong number\n");
                        break;
                }
            }
        }

        static void ShowHistory()
        {
            Console.WriteLine(history);
        }

        static void SetMoney()
        {
            Console.WriteLine($"\nMax: {int.MaxValue}\n");
            Console.WriteLine($"Current amount: {Money} \n");
            Console.WriteLine($"Rechargeable amount: {int.MaxValue - Money}");
            Console.Write("Charge amount: ");
            InputPlusNumber(out int money_charge);
            if (Money <= int.MaxValue - money_charge)
            {
                Money += money_charge;
            }
            else
            {
                Console.WriteLine("The maximum has been exceeded");
            }
            Console.WriteLine($"Current amount: {Money} \n");

        }

        static void BuyItem()
        {
            Console.Write("The product number to be purchased: ");
            InputPlusNumber(out int buy_num);
            buy_num--;

            if (buy_num > Fruits.Count)
            {
                Console.WriteLine("This product does not exist \n");
                return;
            }

            Console.Write("The number of products to purchase: ");
            InputPlusNumber(out int buy_counts);
            if (buy_counts == 0)
            {
                Console.WriteLine("The minimum number of purchases is 1. \n");
                return;
            }

            Buy_amount = Fruits[buy_num].Item_value * buy_counts;
            Buy_item_name = Fruits[buy_num].Item_name;
            Buy_item_stock = Fruits[buy_num].Item_stock;

            Console.WriteLine($"Total payment amount: {Buy_amount}\n");

            if (Money < Buy_amount && Buy_item_stock >= buy_counts)
            {
                Console.WriteLine("Not enough money.");
                Console.WriteLine($"Current money: {Money} \n");
            }
            else if (Money >= Buy_amount && Buy_item_stock >= buy_counts)
            {
                Money -= Buy_amount;
                string messages = $"Purchased product name: {Buy_item_name}  Number of purchases: {buy_counts}";
                Console.WriteLine("Payment finished\n");
                Console.WriteLine(messages);
                Console.WriteLine($"Current money: {Money} \n");
                Fruits[buy_num].Item_stock -= buy_counts;
                history.AppendLine(messages);
                history.AppendLine($"Amount: {Buy_amount}\n");

            }
            else if (Buy_item_stock < buy_counts)
            {
                Console.WriteLine("Out of stock\n");
            }
            Buy_amount = 0;
            Buy_item_stock = 0;
        }

        public static void InputPlusNumber(out int number)
        {
            bool valueSuccess = int.TryParse(Console.ReadLine(), out int input);

            if (valueSuccess && input > 0)
            {
                number = input;
            }
            else
            {
                Console.WriteLine("\nInvaild value\n");
                number = 0;
            }
        }
    }

    

    
}
