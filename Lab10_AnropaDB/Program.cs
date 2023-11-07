
using Lab10_AnropaDB.Data;
using System.Text.RegularExpressions;

namespace Lab10_AnropaDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nWelcome to NorthWind! Please choose one function to continue:"
                    + "\n 1 Get all customers"
                    + "\n 2 Select a customer from the list"
                    + "\n 3 Add a new customer");

                String choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        MenuAction.GetAllCustomers();
                        Console.WriteLine("\nPlease choose to continue (C) or exit (E): C or E?");
                        string choice2 = Console.ReadLine().ToUpper();
                        if (choice2 == "E")
                        {
                            Environment.Exit(1);  //instead of using return false to the while loop boolean variable, we can use while (true)
                        }
                        break;
                    case "2":
                        MenuAction.SelectOneCustomer();
                        Console.WriteLine();
                        Console.WriteLine("\nPlease choose to continue (C) or exit (E): C or E?");
                        string choice3 = Console.ReadLine().ToUpper();
                        if (choice3 == "E")
                        {
                            Environment.Exit(1);
                        }
                        break;
                    case "3":
                        MenuAction.AddNewCustomer();
                        Console.WriteLine();
                        Console.WriteLine("\nPlease choose to continue (C) or exit (E): C or E?");
                        string choice4 = Console.ReadLine().ToUpper();
                        if (choice4 == "E")
                        {
                            Environment.Exit(1);
                        }
                        break;
                    default:
                        Console.WriteLine("\nInvalid number, please type again!");
                        break;
                }
            }

        }
    }
}