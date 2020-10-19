using AddressBookSystem.Services;
using System;
using System.Collections.Generic;

namespace AddressBookSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book Program");

            Dictionary<string, AddressBook> addressBookMap = new Dictionary<string, AddressBook>();

            int addressBookChoice = 1;
            AddressBook addressBook = null;

            while (addressBookChoice != 4)
            {
                Console.WriteLine("\n1. Create new Address Book");
                Console.WriteLine("2. Update existing Address Book");
                Console.WriteLine("3. Search Person in a City or State");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice");
                try
                {
                    addressBookChoice = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
                switch (addressBookChoice)
                {
                    case 1:
                        Console.WriteLine("\nEnter the name of address book");
                        string bookName = Console.ReadLine();
                        addressBookMap.Add(bookName, new AddressBook(bookName));
                        break;

                    case 2:
                        if (addressBookMap.Count != 0)
                        {
                            bool addressBookExist = false;
                            while (!addressBookExist)
                            {
                                try
                                {
                                    Console.WriteLine("\nEnter the Address Book Name");
                                    string name = Console.ReadLine();
                                    addressBook = addressBookMap[name];
                                    addressBookExist = true;
                                }
                                catch (KeyNotFoundException e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }
                            
                            int choice = 1;

                            while (choice != 6)
                            {
                                Console.WriteLine("\n1. Add a Contact");
                                Console.WriteLine("2. View Address Book");
                                Console.WriteLine("3. Edit Contact");
                                Console.WriteLine("4. Delete Contact");
                                Console.WriteLine("5. View person by city/state");
                                Console.WriteLine("6. Back to main menu\n");
                                Console.WriteLine("Enter your choice");
                                try
                                {
                                    choice = Convert.ToInt32(Console.ReadLine());
                                }
                                catch (FormatException e)
                                {
                                    Console.WriteLine(e.Message);
                                    break;
                                }

                                switch (choice)
                                {
                                    case 1:
                                        addressBook.AddContact();
                                        break;

                                    case 2:
                                        addressBook.viewContacts();
                                        break;

                                    case 3:
                                        addressBook.editContact();
                                        break;

                                    case 4:
                                        addressBook.DeleteContact();
                                        break;

                                    case 5:
                                        Console.WriteLine("1. City\n2. State ");
                                        Console.Write("Select : ");
                                        int option = Convert.ToInt32(Console.ReadLine());
                                        if(option == 1)
                                        {
                                            addressBook.groupByCityOrState("city");
                                        }
                                        else if(option == 2)
                                        {
                                            addressBook.groupByCityOrState("state");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid input.");
                                            break;
                                        }
                                        
                                        break;

                                    case 6:
                                        Console.WriteLine("Back to main menu\n");
                                        break;

                                    default:
                                        break;


                                }
                            }
                        }
                        else
                            Console.WriteLine("\nNo Address Book Available");
                        break;

                    case 3:
                        Console.WriteLine("\nEnter the place needed to be searched : ");
                        string place = Console.ReadLine();
                        Console.WriteLine("\nPerson found at {0} are : ", place);
                        foreach (KeyValuePair<string, AddressBook> entry in addressBookMap)
                        {
                            addressBook = entry.Value;
                            List<ContactPerson> persons = addressBook.searchPersonByPlace(place);
                            foreach(ContactPerson person in persons)
                            {
                                Console.WriteLine(person.toString());
                            }
                        }
                        break;
                    
                    case 4:
                        Console.WriteLine("\nThank you for using the application");
                        break;

                    default:
                        break;
                }
            }  
        }
    }
}
