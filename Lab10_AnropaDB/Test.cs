using Lab10_AnropaDB.Data;
using Lab10_AnropaDB.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10_AnropaDB
{
    internal class Test
    {
        //public static void SelectCustomer()
        //{
        //    //The user must be able to select a customer from the list.
        //    //All fields (except the ID) for the customer must then be displayed
        //    //as well as a list of all orders the customer has made.
        //    using (NorthWindContext context = new NorthWindContext())
        //    {
        //        Customer customer = CustomerSelectMenu(context);

        //    }


        //}

        //static Customer CustomerSelectMenu(NorthWindContext context) //start with a menu for user to select
        //{
        //    Console.Clear();

        //    List<Customer> customers = context.Customers.ToList();
        //    for (int i = 0; i < customers.Count(); i++)
        //    {
        //        Console.WriteLine($"{i}. CompanyName: {customers[i].CompanyName}, " +
        //            $"Contactname: {customers[i].ContactName}, " +
        //            $"Address: {customers[i].Address}, " +
        //            $"Phone: {customers[i].Phone}, " +
        //            $"Fax: {customers[i].Fax}");
        //    }

        //    Console.WriteLine("\n Please choose a field to select the customer:" +
        //        "\n 1 CompanyName" +
        //        "\n 2 ContactName" +
        //        "\n 3 Address" +
        //        "\n 4 Phone" +
        //        "\n 5 Fax");
        //    string input = Console.ReadLine();
        //    string menuChoice;
        //    switch (input)
        //    {
        //        case "1":
        //            Console.Write("Please enter a CompanyName from the List: ");
        //            string enteredCompanyName = Console.ReadLine();
        //            return menuChoice = enteredCompanyName;
        //            break;
        //        case "2":
        //            Console.Write("Please enter a ContactName from the List: ");
        //            string enteredContactName = Console.ReadLine();

        //            break;
        //        case "3":
        //            Console.Write("Please enter an Address from the List: ");
        //            string enteredAddress = Console.ReadLine();

        //            break;
        //        case "4":
        //            Console.Write("Please enter a Phone from the List: ");
        //            string enteredPhone = Console.ReadLine();

        //            break;
        //        case "5":
        //            Console.Write("Please enter a Fax from the List: ");
        //            string enteredFax = Console.ReadLine();

        //            break;
        //    }
        //    int menuChoice2 = Convert.ToInt32(input);
        //    return customers[menuChoice2];
        //}

        //static void RunSelection(menuChoice)
        //{
        //    using (NorthWindContext context = new NorthWindContext())
        //    {
        //        var selectedCustomerList = context.Customers
        //                            .Where(c => c.CustomerId == menuChoice)
        //                            .Select(c => new
        //                            {
        //                                c.CompanyName,
        //                                c.ContactName,
        //                                c.ContactTitle,
        //                                c.Address,
        //                                c.City,
        //                                c.PostalCode,
        //                                c.Country,
        //                                c.Region,
        //                                c.Phone,
        //                                c.Fax
        //                            })
        //                            .ToList();
        //        Console.WriteLine();
        //        foreach (var customer in selectedCustomerList)
        //        {
        //            Console.WriteLine($"Company: {customer.CompanyName} ContactTitle: {customer.ContactTitle}, ContactName: {customer.ContactName} " +
        //                $"from Country: {customer.Country} Region: {customer.Region} City: {customer.City} Phone: {customer.Phone}" +
        //                $" Fax: {customer.Fax} Address: {customer.Address} PostalCode: {customer.PostalCode}, has made a list of orders: ");
        //        }
        //    }

        //}

    }
}








//using (var context = new NorthWindContext())
//{
//    var customerList = context.Customers
//                           .Select(c => new
//                           {
//                               CustomerId = c.CustomerId,
//                               CompanyName = c.CompanyName
//                           })
//                           .ToList();
//    foreach (var customer in customerList)
//    {
//        Console.WriteLine($"CustomerId: {customer.CustomerId} of company: {customer.CompanyName}");
//    }

//    Console.WriteLine("Please type in a CustomerId: ");
//    string typedCustomerId = Console.ReadLine().ToUpper();
//    var selectedCustomerList = context.Customers
//        .Where(c => c.CustomerId == typedCustomerId)
//        .Select(c => new
//        {
//            CompanyName = c.CompanyName,
//            Country = c.Country,
//            Region = c.Region,
//            Phone = c.Phone,
//            //orderCount = c.Orders.Count()
//        })
//        .ToList();
//    Console.WriteLine();
//    foreach (var customer in selectedCustomerList)
//    {
//        Console.WriteLine($"Company: {customer.CompanyName} is from {customer.Country} {customer.Region}," +
//            $" with phone number {customer.Phone}, has made a list of orders: ");
//    }
//    Console.WriteLine();

//    //as well as a list of all orders the customer has made.
//    var orderList = context.Orders
//        .Where(o => o.CustomerId == typedCustomerId)
//        .Select(o => new
//        {
//            CustomerId = o.CustomerId,
//            OrderId = o.OrderId,
//            OrderDate = o.OrderDate,
//            ProductId = o.OrderDetails.Single().ProductId
//        })
//        .ToList();
//    foreach (var order in orderList)
//    {
//        Console.WriteLine($"The company has orderID: {order.OrderId}, ordered at {order.OrderDate}.");
//    }
