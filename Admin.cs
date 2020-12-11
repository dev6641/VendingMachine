using System;
using Vending_Machine;

namespace VendingMachine
{
    public static class Admin
    {
        private static int Pwd = 1234;
        private static bool Permission = false;

        private enum Menu
        {
            Err,
            Restock,
            AddItem,
            DeleteItem,
            EditItem,
            ChangePwd,
            Logout
        }

        public static void Login()
        {
            CheckPermission();
            if (Permission == true)
            {
                while (Permission == true)
                {
                    Console.WriteLine("1.Restock  2.Add new item  3.Delete item  4.Edit item info  5.Change password  6.Logout \n");
                    Console.Write("\nExecute Order Number: ");
                    Program.InputPlusNumber(out int AdminMenuInput);
                    Menu Emenu = (Menu)AdminMenuInput;

                    switch (Emenu)
                    {
                        case Menu.Restock:
                            Restock();
                            break;

                        case Menu.AddItem:
                            AddItem();
                            break;

                        case Menu.DeleteItem:
                            DeleteItem();
                            break;

                        case Menu.EditItem:
                            EditItem();
                            break;

                        case Menu.ChangePwd:
                            ChangePwd();
                            break;

                        case Menu.Logout:
                            Permission = false;
                            Console.WriteLine("\n\n###########################");
                            Console.WriteLine("##### Logout Success ######");
                            Console.WriteLine("###########################\n\n");
                            break;

                        case Menu.Err:
                            Console.WriteLine("Invaild order number!\n");
                            break;
                    }

                }
            }

        }


        public static void CheckPermission()
        {
            Console.Write("\nAdmin password: ");
            Program.InputPlusNumber(out int inputPwd);
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
            Item.GetItems(Program.Fruits);

            Console.Write("Product number to fill stock: ");
            Program.InputPlusNumber(out int inputNum);

            if (inputNum == 0 || inputNum > Program.Fruits.Count)
            {
                Console.WriteLine("This product does not exist.\n");
                return;
            }

            inputNum--;

            Console.Write("Number of restock: ");
            Program.InputPlusNumber(out int inputCount);
            if (inputCount == 0)
            {
                Console.WriteLine("minimum is 1 \n");
                return;
            }

            Program.Fruits[inputNum].Item_stock += inputCount;
            Console.WriteLine("Complete stock filling\n");
            Console.WriteLine($"{Program.Fruits[inputNum].Item_name}'s stock: {Program.Fruits[inputNum].Item_stock}\n");

        }

        public static void AddItem()
        {
            string name;

            Item.GetItems(Program.Fruits);
            Console.Write("Product name to be added: ");
            name = Console.ReadLine();

            Console.Write("Product value to be added: ");
            Program.InputPlusNumber(out int value);
            if (value == 0)
            {
                Console.WriteLine("minimum is 1.\n");
                return;
            }

            Console.Write("Product stock to be added: ");
            Program.InputPlusNumber(out int stock);
            if (stock == 0)
            {
                Console.WriteLine("minimum is 1.\n");
                return;
            }

            Program.Fruits.Add(new Item(value: value, stock: stock, name: name));
            Item.ItemUpdate = true;

        }

        public static void EditItem()
        {
            string name;

            Item.GetItems(Program.Fruits);

            Console.Write("Product number to be edited: ");
            Program.InputPlusNumber(out int InputNumber);
            InputNumber--;

            if (InputNumber == 0)
            {
                Console.WriteLine("This product does not exist.\n");
                return;
            }

            Console.Write("New item name: ");
            name = Console.ReadLine();

            Console.Write("New item value: ");
            Program.InputPlusNumber(out int value);
            if (value == 0)
            {
                Console.WriteLine("minimum is 1.\n");
                return;
            }

            Console.Write("New item stock: ");
            Program.InputPlusNumber(out int stock);
            if (stock == 0)
            {
                Console.WriteLine("minimum is 1.\n");
                return;
            }
            Program.Fruits[InputNumber].Item_name = name;
            Program.Fruits[InputNumber].Item_value = value;
            Program.Fruits[InputNumber].Item_stock = stock;
            Item.ItemUpdate = true;

        }

        public static void DeleteItem()
        {
            Item.GetItems(Program.Fruits);
            Console.Write("Product number to be deleted: ");
            Program.InputPlusNumber(out int InputNumber);
            InputNumber--;

            if (InputNumber > Program.Fruits.Count || Program.Fruits.Count == 0)
            {
                Console.WriteLine("Does not exist.\n");
                return;
            }

            Program.Fruits.RemoveAt(InputNumber);
            Item.ItemUpdate = true;
        }

        public static void ChangePwd()
        {

            Console.WriteLine("You can use only number value for password\n");
            Console.Write("New Password: ");
            Program.InputPlusNumber(out int newPwd);
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
