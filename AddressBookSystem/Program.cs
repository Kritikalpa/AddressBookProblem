using AddressBookSystem.Services;
using System;


namespace AddressBookSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book Program\n");

            AddressBook addressBook = new AddressBook();
            int choice = 1;
            
            while(choice != 5)
            {
                Console.WriteLine("\n1. Add a Contact");
                Console.WriteLine("2. View Address Book");
                Console.WriteLine("3. Edit Contact");
                Console.WriteLine("4. Delete Contact");
                Console.WriteLine("5. Exit\n");
                Console.WriteLine("Enter your choice");
                choice = Convert.ToInt32(Console.ReadLine());

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
                        Console.WriteLine("Thank you for using application.");
                        break;

                    default:
                        break;


                }
            }    
        }
    }
}
