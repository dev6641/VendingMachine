using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Vending_Machine;

namespace VendingMachine
{
    public class Item
    {
        public int Item_value { get; set; }
        public int Item_stock { get; set; }
        public string Item_name { get; set; }
        public static StringBuilder MenuMsg = new StringBuilder(1024);
        public static StringBuilder History = new StringBuilder(1024);
        public static bool ItemUpdate = true;


        public Item(int value, int stock, string name)
        {
            this.Item_value = value;
            this.Item_stock = stock;
            this.Item_name = name;
        }

        static public void GetItems(List<Item> ListItem)
        {
            Console.WriteLine();

            int index = 1;
            if (ItemUpdate)
            {
                MenuMsg.Clear();
                foreach (var i in ListItem)
                {
                    MenuMsg.AppendLine($" {index}.{i.Item_name}  Value: {i.Item_value}  Stock: {i.Item_stock}");
                    //msg.AppendLine($" {index}.{i.Item_name}  Value: {i.Item_value}  Stock: {i.Item_stock}");
                    index++;
                }
                ItemUpdate = false;
            }
            Console.WriteLine(MenuMsg);
            Console.WriteLine($"\n Total number of products: {Program.Fruits.Count}\n");
        }

    }
}
