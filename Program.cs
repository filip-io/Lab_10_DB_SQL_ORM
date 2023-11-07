using Lab_10_DB_SQL_ORM.Data;
using Lab_10_DB_SQL_ORM.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lab_10_DB_SQL_ORM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Northwind Database";
            Console.WriteLine("\t /-=-=-=-= Welcome to the Northwind Database! =-=-=-=-\\ \n");

            using (var context = new NorthwindContext())
            {
                {
                    MainMenu(context);
                }


                // Main menu

                static void MainMenu(NorthwindContext context)
                {
                    while (true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("\t What would you like to do?");
                        Console.WriteLine("\t Press key corresponding your choice: \n");
                        Console.WriteLine("\t 1. Display all customers");
                        Console.WriteLine("\t 2. Display specific customer");
                        Console.WriteLine("\t 3. Add customer");
                        Console.WriteLine("\t 4. Close program");
                        string choice = Console.ReadLine();


                        if (choice != "1" && choice != "2" && choice != "3" && choice != "4")
                        {
                            Console.WriteLine("Please press valid key to continue.");
                        }


                        switch (choice)
                        {
                            case "1":
                                Console.Clear();
                                DisplayCustomers(context);
                                break;
                            case "2":
                                Console.Clear();
                                DisplaySpecificCustomer();
                                break;
                            case "3":
                                Console.Clear();
                                AddNewCustomer();
                                break;
                            case "4":
                                Environment.Exit(0);
                                break;
                        }

                    }



                }
            }


            /* Method to display all customers. 
             * Ascending or descending sorting depending on users choice. */

            static void DisplayCustomers(NorthwindContext context)
            {

                {
                    Console.WriteLine("Press D for descending or A for ascending sorting: ");
                    string sorting = Console.ReadLine().ToLower();

                    while (sorting != "a" && sorting != "d")
                    {
                        Console.WriteLine("Please press valid key to continue:");
                        sorting = Console.ReadLine().ToLower();
                    }

                    var displayAll = context.Customers
                        .Select(c => new
                        {
                            c.CompanyName,
                            c.Country,
                            c.Region,
                            c.Phone,
                            Orders = c.Orders.Count()
                        });

                    if (sorting == "d")
                    {
                        displayAll = displayAll.OrderByDescending(c => c.CompanyName);
                    }
                    else if (sorting == "a")
                    {
                        displayAll = displayAll.OrderBy(c => c.CompanyName);
                    }

                    var DisplayAllCustomers = displayAll.ToList();

                    Console.Clear();

                    foreach (var c in DisplayAllCustomers)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Company name: {c.CompanyName}\n");
                        Console.WriteLine($"Country: {c.Country}\n");
                        Console.WriteLine($"Region: {c.Region}\n");
                        Console.WriteLine($"Phone number: {c.Phone}\n");
                        Console.WriteLine($"Numbert of orders: {c.Orders}\n");
                        Console.WriteLine("=============================================");
                    }



                }

            }



            /* Method to display a specific customer based on users choice.
             * All fields (except ID) will be shown and all orders made by that customer. */

            static void DisplaySpecificCustomer()
            {
                Console.WriteLine("Please enter customer name: ");
                string name = Console.ReadLine();

                using (NorthwindContext context = new NorthwindContext())
                {
                    string input = Console.ReadLine();

                    Console.Clear();

                    var customer = context.Customers
                        .Where(c => c.CompanyName == name)
                        .Select(c => new
                        {
                            c.CompanyName,
                            c.ContactName,
                            c.ContactTitle,
                            c.Address,
                            c.City,
                            c.Region,
                            c.PostalCode,
                            c.Country,
                            c.Phone,
                            c.Fax
                        })
                        .FirstOrDefault();

                    if (customer != null)
                    {
                        Console.WriteLine($"Customer Details:");
                        Console.WriteLine($"Company Name: {customer.CompanyName}");
                        Console.WriteLine($"Contact Name: {customer.ContactName}");
                        Console.WriteLine($"Contact Title: {customer.ContactTitle}");
                        Console.WriteLine($"Address: {customer.Address}");
                        Console.WriteLine($"City: {customer.City}");
                        Console.WriteLine($"Region: {customer.Region}");
                        Console.WriteLine($"Postal Code: {customer.PostalCode}");
                        Console.WriteLine($"Country: {customer.Country}");
                        Console.WriteLine($"Phone: {customer.Phone}");
                        Console.WriteLine($"Fax: {customer.Fax}");

                        Console.WriteLine("\nOrders for this customer:");
                        var customerWithOrders = context.Customers
                            .Include(c => c.Orders)
                            .ThenInclude(o => o.OrderDetails)
                            .ThenInclude(od => od.Product)
                            .SingleOrDefault(c => c.CompanyName == name);

                        foreach (var order in customerWithOrders.Orders)
                        {
                            Console.WriteLine($"Order ID: {order.OrderId}");
                            Console.WriteLine($"Order Date: {order.OrderDate}");

                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Customer not found.");
                    }


                }


            }


            // Method to add a customer. ID will be auto-generated with a randomized string.

            static void AddNewCustomer()
            {
                Console.Clear();

                Console.WriteLine("Please enter details for new customer:");

                Console.Write("Company Name: ");
                string companyName = Console.ReadLine();

                Console.Write("Contact Name: ");
                string contactName = Console.ReadLine();

                Console.Write("Contact Title: ");
                string contactTitle = Console.ReadLine();

                Console.Write("Address: ");
                string address = Console.ReadLine();

                Console.Write("City: ");
                string city = Console.ReadLine();

                Console.Write("Region: ");
                string region = Console.ReadLine();

                Console.Write("Postal Code: ");
                string postalCode = Console.ReadLine();

                Console.Write("Country: ");
                string country = Console.ReadLine();

                Console.Write("Phone: ");
                string phone = Console.ReadLine();

                Console.Write("Fax: ");
                string fax = Console.ReadLine();

                string randomString = GenerateRandomString(5);

                Customer newCustomer = new Customer
                {
                    CustomerId = randomString,
                    CompanyName = string.IsNullOrWhiteSpace(companyName) ? null : companyName,
                    ContactName = string.IsNullOrWhiteSpace(contactName) ? null : contactName,
                    ContactTitle = string.IsNullOrWhiteSpace(contactTitle) ? null : contactTitle,
                    Address = string.IsNullOrWhiteSpace(address) ? null : address,
                    City = string.IsNullOrWhiteSpace(city) ? null : city,
                    Region = string.IsNullOrWhiteSpace(region) ? null : region,
                    PostalCode = string.IsNullOrWhiteSpace(postalCode) ? null : postalCode,
                    Country = string.IsNullOrWhiteSpace(country) ? null : country,
                    Phone = string.IsNullOrWhiteSpace(phone) ? null : phone,
                    Fax = string.IsNullOrWhiteSpace(fax) ? null : fax
                };

                using (NorthwindContext context = new NorthwindContext())
                {
                    context.Customers.Add(newCustomer);
                    context.SaveChanges();
                    Console.WriteLine("You have successfully added a new customer!");
                }

            }

            static string GenerateRandomString(int length)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                Random random = new Random();
                return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }

    }
}