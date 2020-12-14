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
            //기본 메뉴 선언
            Fruits.Add(new Item(value: 500, stock: 50, name: "Apple"));
            Fruits.Add(new Item(value: 200, stock: 80, name: "Banana"));
            Fruits.Add(new Item(value: 700, stock: 20, name: "Lemon"));
            Fruits.Add(new Item(value: 800, stock: 100, name: "Mikang"));
            Fruits.Add(new Item(value: 1000, stock: 10, name: "Melon"));

            Console.WriteLine("#################################");
            Console.WriteLine("###### Vending Machine 0.4 ######");
            Console.WriteLine("#################################\n");
            Item.GetItems(Fruits);

            //종료전까지 반복 실행
            while (true)
            {
                //제어 메뉴 출력
                Console.WriteLine("1.Show Items  2.Order  3.Charge Money  4.Check Money  5.Admin Login  6.Hsitory 7.Exit \n");
                Console.Write(" \nExecute Order Number: ");
                InputPlusIntFailTo0(out int menu_num);
                Emenu menu_select = (Emenu)menu_num;

                switch (menu_select)
                {
                    case Emenu.GetMenu:
                        Item.GetItems(Fruits);
                        break;

                    case Emenu.BuyItem:
                        BuyItem(Fruits);
                        break;

                    case Emenu.SetMoney:
                        SetMoney();
                        break;

                    case Emenu.GetMoney:
                        Console.WriteLine($"NOW Money: {Money} \n");
                        break;

                    case Emenu.AdminLogin:
                        Admin.Login(Fruits);
                        break;

                    case Emenu.History:
                        ShowHistory();
                        break;

                    case Emenu.Exit:
                        Console.WriteLine("###########################");
                        Console.WriteLine("########  BYE BYE  ########");
                        Console.WriteLine("###########################");
                        return;

                    case Emenu.None:
                        Console.WriteLine("It is wrong number\n");
                        break;
                }
            }
        }

        static void ShowHistory()
        {
            Console.WriteLine(Item.History);
        }

        static void SetMoney()
        {
            Console.WriteLine($"\nMax: {int.MaxValue}\n");
            Console.WriteLine($"Current amount: {Money} \n");
            Console.WriteLine($"Rechargeable amount: {int.MaxValue - Money}");
            Console.Write("Charge amount: ");
            InputPlusIntFailTo0(out int money_charge);
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

        static void BuyItem(List<Item> items)
        {
            Console.Write("The product number to be purchased: ");
            InputPlusIntFailTo0(out int buy_num);
            buy_num--;

            if (buy_num > items.Count)
            {
                Console.WriteLine("This product does not exist \n");
                return;
            }

            Console.Write("The number of products to purchase: ");
            InputPlusIntFailTo0(out int buy_counts);
            if (buy_counts == 0)
            {
                Console.WriteLine("The minimum number of purchases is 1. \n");
                return;
            }

            Buy_amount = items[buy_num].Item_value * buy_counts;
            Buy_item_name = items[buy_num].Item_name;
            Buy_item_stock = items[buy_num].Item_stock;

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
                items[buy_num].Item_stock -= buy_counts;
                Item.History.AppendLine(messages);
                Item.History.AppendLine($"Amount: {Buy_amount}\n");
                Item.ItemUpdate = true;

            }
            else if (Buy_item_stock < buy_counts)
            {
                Console.WriteLine("Out of stock\n");
            }
            Buy_amount = 0;
            Buy_item_stock = 0;
        }

        public static void InputPlusIntFailTo0(out int number)
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
