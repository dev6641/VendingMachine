using System;
using System.Collections.Generic;
using Vending_Machine;

namespace VendingMachine
{
    public static class Admin
    {
        private static int Pwd = 1234;
        private static bool Permission = false;
        private static List<Item> AccessItems = new List<Item>();

        private enum EadminMenu
        {
            Err,
            Restock,
            AddItem,
            DeleteItem,
            EditItem,
            ChangePwd,
            Logout
        }

        public static void Login(List<Item> items)
        {   
            CheckPermission();
            AccessItems = items;
            while (Permission == true)
            {
                Console.WriteLine("1.Restock  2.Add new item  3.Delete item  4.Edit item info  5.Change password  6.Logout \n");
                Console.Write("\nExecute Order Number: ");
                Program.InputPlusIntFailTo0(out int AdminMenuInput);
                EadminMenu Emenu = (EadminMenu)AdminMenuInput;

                switch (Emenu)
                {
                    case EadminMenu.Restock:
                        Restock();
                        break;

                    case EadminMenu.AddItem:
                        AddItem();
                        break;

                    case EadminMenu.DeleteItem:
                        DeleteItem();
                        break;

                    case EadminMenu.EditItem:
                        EditItem();
                        break;

                    case EadminMenu.ChangePwd:
                        ChangePwd();
                        break;

                    case EadminMenu.Logout:
                        Permission = false;
                        Console.WriteLine("\n\n###########################");
                        Console.WriteLine("##### Logout Success ######");
                        Console.WriteLine("###########################\n\n");
                        break;

                    case EadminMenu.Err:
                        Console.WriteLine("Invaild order number!\n");
                        break;
                }

            }

        }


        public static void CheckPermission()
        {
            Console.Write("\nAdmin password: ");
            Program.InputPlusIntFailTo0(out int inputPwd);
            if (inputPwd == Pwd)
            {
                Console.WriteLine("\n\n###########################");
                Console.WriteLine("### Admin Login Success ###");
                Console.WriteLine("###########################\n\n");
                Permission = true;
            }
            else if (inputPwd != 0 && inputPwd != Pwd)
            {
                Console.WriteLine("\nIt's worng password!\n");
            }
        }

        public static void Restock()
        {
            Item.GetItems(AccessItems);

            Console.Write("Product number to fill stock: ");
            Program.InputPlusIntFailTo0(out int inputNum);

            if (inputNum == 0 || inputNum > AccessItems.Count)
            {
                Console.WriteLine("This product does not exist.\n");
                return;
            }

            inputNum--;

            Console.Write("Number of restock: ");
            Program.InputPlusIntFailTo0(out int inputCount);
            if (inputCount == 0)
            {
                Console.WriteLine("minimum is 1 \n");
                return;
            }

            AccessItems[inputNum].Item_stock += inputCount;
            Console.WriteLine("Complete stock filling\n");
            Console.WriteLine($"{AccessItems[inputNum].Item_name}'s stock: {AccessItems[inputNum].Item_stock}\n");

        }

        public static void AddItem()
        {
            string name;

            Item.GetItems(AccessItems);
            Console.Write("Product name to be added: ");
            name = Console.ReadLine();

            Console.Write("Product value to be added: ");
            Program.InputPlusIntFailTo0(out int value);
            if (value == 0)
            {
                Console.WriteLine("minimum is 1.\n");
                return;
            }

            Console.Write("Product stock to be added: ");
            Program.InputPlusIntFailTo0(out int stock);
            if (stock == 0)
            {
                Console.WriteLine("minimum is 1.\n");
                return;
            }

            AccessItems.Add(new Item(value: value, stock: stock, name: name));
            Item.ItemUpdate = true;

        }

        public static void EditItem()
        {
            Item.GetItems(AccessItems);
            Console.Write("Product number to be edited: ");
            Program.InputPlusIntFailTo0(out int InputNumber);

            if (InputNumber == 0)
            {
                Console.WriteLine("This product does not exist.\n");
                return;
            }
            InputNumber--;
            Console.Write("New item name: ");
            string name;
            name = Console.ReadLine();

            Console.Write("New item value: ");
            Program.InputPlusIntFailTo0(out int value);
            if (value == 0)
            {
                Console.WriteLine("minimum is 1.\n");
                return;
            }

            Console.Write("New item stock: ");
            Program.InputPlusIntFailTo0(out int stock);
            if (stock == 0)
            {
                Console.WriteLine("minimum is 1.\n");
                return;
            }
            AccessItems[InputNumber].Item_name = name;
            AccessItems[InputNumber].Item_value = value;
            AccessItems[InputNumber].Item_stock = stock;
            Item.ItemUpdate = true;

        }

        public static void DeleteItem()
        {
            Item.GetItems(AccessItems);
            Console.Write("Product number to be deleted: ");
            Program.InputPlusIntFailTo0(out int InputNumber);
            InputNumber--;

            if (InputNumber > AccessItems.Count || AccessItems.Count == 0)
            {
                Console.WriteLine("Does not exist.\n");
                return;
            }

            AccessItems.RemoveAt(InputNumber);
            Item.ItemUpdate = true;
        }

        public static void ChangePwd()
        {

            Console.WriteLine("You can use only number value for password\n");
            Console.Write("New Password: ");
            Program.InputPlusIntFailTo0(out int newPwd);
            if (newPwd == 0)
            {
                Console.WriteLine("Can't use value for password");
                return;
            }
            Pwd = newPwd;
            Console.WriteLine("Change Success!\n");
        }
    }
}
