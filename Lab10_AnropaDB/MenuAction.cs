using Lab10_AnropaDB.Data;
using Lab10_AnropaDB.Models.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10_AnropaDB
{
    internal class MenuAction
    {
        public static void GetAllCustomers()
        {
            using (var context = new NorthWindContext())
            {
                //-  Hämta alla kunder. Visa företagsnamn, land, region, telefonnummer och antal ordrar
                //de har Sortera på företagsnamn.Användaren ska kunna välja stigande eller fallande ordning.
                //Extra challenge: In console when listing customers, print number of orders already shipped and number of orders not shipped yet (ShippedDate is null on the latter)

                var customerList = context.Customers
                                   .Select(c => new
                                   {
                                       CompanyName = c.CompanyName,
                                       Country = c.Country,
                                       Region = c.Region,
                                       Phone = c.Phone,
                                       OrderCount = c.Orders.Count(),   //Vi kan göra implicita joins genom att använda navigation properties
                                       ShippedCount = c.Orders.Where(o => o.ShippedDate != null).Count(),
                                       NotShippedCount = c.Orders.Where(o => o.ShippedDate == null).Count()                           
                                   })
                                   .ToList();
                //Choose the order A or D
                Console.WriteLine("Please choose the order(A = Ascending, D = Descending): A or D ?");
                string order = Console.ReadLine();
                if (order == "D")
                {
                    customerList = customerList.OrderByDescending(x => x.CompanyName).ToList();
                }
                else
                {
                    customerList = customerList.OrderBy(x => x.CompanyName).ToList();
                }
                foreach (var customer in customerList)
                {
                    Console.WriteLine($"{customer.CompanyName}, from {customer.Country} {customer.Region}, " +
                        $"with phone number {customer.Phone} has {customer.OrderCount} orders in total, " +
                        $"of which {customer.ShippedCount} are shipped and {customer.NotShippedCount} are not shipped yet..");
                }
                Console.WriteLine();
            }
        }

        public static void SelectOneCustomer()
        {
            //The user must be able to select a customer from the list.
            //All fields (except the ID) for the customer must then be displayed
            //as well as a list of all orders the customer has made.

            Console.Write("\nPlease enter a CompanyName: ");
            string typedCompanyName = Console.ReadLine();

            using (var context = new NorthWindContext())
            {
                var selectedCustomer = context.Customers   //Select one customer from the list
                    .Where(c => c.CompanyName == typedCompanyName)    // how to vertify the name first????
                    .Select(c => new
                    {
                        c.CompanyName,
                        c.ContactName,
                        c.ContactTitle,
                        c.Address,
                        c.City,
                        c.PostalCode,
                        c.Country,
                        c.Region,
                        c.Phone,
                        c.Fax,
                        orderCount = c.Orders.Count()
                    })
                    .FirstOrDefault();

                Console.WriteLine($"\nCompany: {selectedCustomer.CompanyName}, " +
                    $"\n ContactName: {selectedCustomer.ContactName}, " +
                    $"\n ContactTitle: {selectedCustomer.ContactTitle}, " +
                    $"\n Address: {selectedCustomer.Address}, " +
                    $"\n City: {selectedCustomer.City}, " +
                    $"\n PostalCode: {selectedCustomer.PostalCode}, " +
                    $"\n Country: {selectedCustomer.Country}, " +
                    $"\n Region: {selectedCustomer.Region}, " +
                    $"\n Phone: {selectedCustomer.Phone}, " +
                    $"\n Fax: {selectedCustomer.Fax}");

                Console.WriteLine($"\nThe company has made the {selectedCustomer.orderCount} orders: ");
                var order = context.Customers   // one customer can make a list of orders
                    .Where(c => c.CompanyName == typedCompanyName)
                    .Include(c => c.Orders)
                    .Single()
                    .Orders
                    .ToList();
                for (int i = 0; i < order.Count(); i++)
                {
                    Console.WriteLine($"{i + 1}. OrderId: {order[i].OrderId}, " +
                        $"\n shippedDate: {order[i].ShippedDate}, " +
                        $"\n ShipCity: {order[i].ShipCity}, " +
                        $"\n ShipVia: {order[i].ShipVia}, " +
                        $"\n Fright: {order[i].Freight}");
                }
                Console.WriteLine("\nWe have listed all the infomation for the selected customer!");
            }
        }

        public static void AddNewCustomer()
        {
            //Add customer. The user should be able to add a customer and fill in values for all columns except the ID.
            //iD you need to generate a random string for (5 characters long).
            //If the user does not fill in a value, null should be sent to the database, not an empty string. 

            using (var context = new NorthWindContext())
            {
                while (true)
                {
                    Console.WriteLine("Here we go to add an new customer:");
                    //generate a random string CompanyId, perhaps there has other good way.....
                    string alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    int size = 5;
                    Random rnd = new Random();
                    string newCustomerId = "";
                    for (int i = 0; i < size; i++)
                    {
                        int randomIndex = rnd.Next(0, 26);
                        newCustomerId = newCustomerId + alfabet.Substring(randomIndex, 1);  // We need to vertify if the random generated customerId already exists
                    }
                    var existingCustomerId = context.Customers.FirstOrDefault(c => c.CustomerId == newCustomerId); // Find the first customerId matched the randomly generated newCustomerId
                    if(existingCustomerId != null) 
                    {
                        Console.WriteLine("The customerID has existed, please run it again........");
                    }
                    else
                    {                        
                        Console.WriteLine("\nType the CompanyName: ");
                        string newCompanyName = Console.ReadLine();
                        while (string.IsNullOrWhiteSpace(newCompanyName))     //CompanyName should be not null, we need to vertify it first.
                                                                         //This method 'string.IsNullOrWhiteSpace(newCompanyName)' returns true if the string is null or has no characters, and false otherwise.
                                                                         //The while loop will repeat as long as the condition is true, meaning that the user input is invalid.
                        {
                            Console.WriteLine("\nCompany Name can not be null or empty, please enter the CompanyName: ");
                            newCompanyName = Console.ReadLine();
                        }
                        // Assign null if the input is null or empty, it equals to: 
                                        //if (string.IsNullOrEmpty(newContactName))
                                        //{
                                        //    newContactName = null;
                                        //}
                        Console.WriteLine("\nType the ContactName: ");
                        string input = Console.ReadLine();
                        string newContactName =string.IsNullOrWhiteSpace(input) ? null : input;
                        Console.WriteLine("\nType the contactTitle: ");
                        input = Console.ReadLine();
                        string newContactTitle = string.IsNullOrWhiteSpace(input) ? null : input;
                        Console.WriteLine("\nType the Address: ");
                        input = Console.ReadLine();
                        string newAddress = string.IsNullOrWhiteSpace(input) ? null : input;
                        Console.WriteLine("\nType the City: ");
                        input = Console.ReadLine();
                        string newCity = string.IsNullOrWhiteSpace(input) ? null : input;
                        Console.WriteLine("\nType the Region: ");
                        input = Console.ReadLine();
                        string newRegion = string.IsNullOrWhiteSpace(input) ? null : input;
                        Console.WriteLine("\nType the PostalCode: ");
                        input = Console.ReadLine();
                        string newPostalCodee = string.IsNullOrWhiteSpace(input) ? null : input;
                        Console.WriteLine("\nType the Country: ");
                        input = Console.ReadLine();
                        string newCountry = string.IsNullOrWhiteSpace(input) ? null : input;
                        Console.WriteLine("\nType the Phone: ");
                        input = Console.ReadLine();
                        string newPhone = string.IsNullOrWhiteSpace(input) ? null : input;
                        Console.WriteLine("\nType the Fax: ");
                        input = Console.ReadLine();
                        string newFax = string.IsNullOrWhiteSpace(input) ? null : input;

                        //add the entered info into the database
                        Customer customer = new Customer()
                        {
                            CustomerId = newCustomerId,
                            CompanyName = newCompanyName,
                            ContactName = newContactName,
                            ContactTitle = newContactTitle,
                            Address = newAddress,
                            City = newCity,
                            Region = newRegion,
                            PostalCode = newPostalCodee,
                            Country = newCountry,
                            Phone = newPhone,
                            Fax = newFax
                        };
                        context.Customers.Add(customer);
                        context.SaveChanges();
                        Console.WriteLine("\nThe new customer has added successfully!");
                        Environment.Exit(1);
                    }
                }

            }
        }

    }
}
