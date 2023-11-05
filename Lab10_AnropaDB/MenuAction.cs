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

                var customerList = context.Customers
                                   .Select(c => new
                                   {
                                       CompanyName = c.CompanyName,
                                       Country = c.Country,
                                       Region = c.Region,
                                       Phone = c.Phone,
                                       OrderCount = c.Orders.Count()   //Vi kan göra implicita joins genom att använda navigation properties
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
                    Console.WriteLine($"{customer.CompanyName}, from {customer.Country} {customer.Region}, with phone number {customer.Phone} has {customer.OrderCount} orders.");
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
            //If the user does not fill in a value, null should be sent to the database, not an empty string. How????

            using (var context = new NorthWindContext())
            {
                Console.WriteLine("Here we go to add an new customer:");
                //generate a random string CompanyId

                string alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                int size = 5;
                Random rnd = new Random();

                string newCustomerId = "";
                for (int i = 0; i < size; i++)
                {
                    int randomIndex = rnd.Next(0, 26);
                    newCustomerId = newCustomerId + alfabet.Substring(randomIndex, 1);  //maybe we need to vertify if the random generated customerId already exists
                }

                Console.WriteLine("\nType the CompanyName: ");
                string newCompanyName = Console.ReadLine();
                Console.WriteLine("\nType the ContactName: ");
                string newContactName = Console.ReadLine();
                Console.WriteLine("\nType the contactTitle: ");
                string newContactTitle = Console.ReadLine();
                Console.WriteLine("\nType the Address: ");
                string newAddress = Console.ReadLine();
                Console.WriteLine("\nType the City: ");
                string newCity = Console.ReadLine();
                Console.WriteLine("\nType the Region: ");
                string newRegion = Console.ReadLine();
                Console.WriteLine("\nType the PostalCode: ");
                string newPostalCodee = Console.ReadLine();
                Console.WriteLine("\nType the Country: ");
                string newCountry = Console.ReadLine();
                Console.WriteLine("\nType the Phone: ");
                string newPhone = Console.ReadLine();
                Console.WriteLine("\nType the Fax: ");
                string newFax = Console.ReadLine();


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
            }
        }
    }
}
