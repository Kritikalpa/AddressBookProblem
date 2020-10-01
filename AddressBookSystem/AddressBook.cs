using AddressBookSystem.Services;
using System;
using System.Text.RegularExpressions;

namespace AddressBookSystem
{
    class AddressBookMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book Program");

            Regex reEmail = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            Regex rePhone = new Regex(@"^[0-9]{10}$");

            Console.WriteLine("Create a contact");
            Console.WriteLine("----------------");
            Console.WriteLine("Enter the first name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("\nEnter the last name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("\nEnter the address:");
            string adddress = Console.ReadLine();
            Console.WriteLine("\nEnter the city:");
            string city = Console.ReadLine();
            Console.WriteLine("\nEnter the state:");
            string state = Console.ReadLine();
            Console.WriteLine("\nEnter the zip:");
            long zip = long.Parse(Console.ReadLine());
            Console.WriteLine("\nEnter the email:");
            string email = Console.ReadLine();
            while (!reEmail.IsMatch(email))
            {
                Console.WriteLine("\nENTER A VALID EMAIL");
                email = Console.ReadLine();
            }
            Console.WriteLine("\nEnter the phone number:");
            long phoneNumber = long.Parse(Console.ReadLine());
            while (!rePhone.IsMatch(phoneNumber.ToString()))
            {
                Console.WriteLine("\nENTER A VALID PHONE NUMBER");
                phoneNumber = long.Parse(Console.ReadLine());
            }

            ContactPerson addressBook = new ContactPerson(firstName, lastName, adddress, city, state, email, zip, phoneNumber);

            Console.WriteLine("\nADDRESS BOOK\n------------------");

            addressBook.toString();
        }
    }
}
