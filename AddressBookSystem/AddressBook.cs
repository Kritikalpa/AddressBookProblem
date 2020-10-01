using AddressBookSystem.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AddressBookSystem
{
    class AddressBookMain
    {
        static Regex reEmail = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
        static Regex rePhone = new Regex(@"^[0-9]{10}$");
        public static void editContact(List<ContactPerson> personList)
        {
            

            Console.WriteLine("\nEnter the id");
            int id = Convert.ToInt32(Console.ReadLine());
            ContactPerson contactPerson = null;
            foreach (ContactPerson person in personList)
            {
                if(person.id == id)
                {
                    contactPerson = person;
                    break;
                }
            }
            Console.WriteLine("Select update parameter");
            Console.WriteLine("1. First Name  2. Last Name  3. Address  4. City  5. State  6. ZIP  7. Email  8. Phone Number");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter the first name:");
                    string firstName = Console.ReadLine();
                    contactPerson.firstName = firstName;
                    break;
                case 2:
                    Console.WriteLine("\nEnter the last name:");
                    string lastName = Console.ReadLine();
                    contactPerson.lastName = lastName;
                    break;
                case 3:
                    Console.WriteLine("\nEnter the address:");
                    string adddress = Console.ReadLine();
                    contactPerson.address = adddress;
                    break;
                case 4:
                    Console.WriteLine("\nEnter the city:");
                    string city = Console.ReadLine();
                    contactPerson.city = city;
                    break;
                case 5:
                    Console.WriteLine("\nEnter the state:");
                    string state = Console.ReadLine();
                    contactPerson.state = state;
                    break;
                case 6:
                    Console.WriteLine("\nEnter the zip:");
                    long zip = long.Parse(Console.ReadLine());
                    contactPerson.zip = zip;
                    break;
                case 7:
                    Console.WriteLine("\nEnter the email:");
                    string email = Console.ReadLine();
                    while (!reEmail.IsMatch(email))
                    {
                        Console.WriteLine("\nENTER A VALID EMAIL");
                        email = Console.ReadLine();
                    }
                    contactPerson.email = email;
                    break;
                case 8:
                    Console.WriteLine("\nEnter the phone number:");
                    long phoneNumber = long.Parse(Console.ReadLine());
                    while (!rePhone.IsMatch(phoneNumber.ToString()))
                    {
                        Console.WriteLine("\nENTER A VALID PHONE NUMBER");
                        phoneNumber = long.Parse(Console.ReadLine());
                    }
                    contactPerson.phoneNumber = phoneNumber;
                    break;
                default:
                    break;
            }
            Console.WriteLine("Contact details updated");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book Program\n");

            List<ContactPerson> personList = new List<ContactPerson>();
            int choice = 1, id = 1;
            

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

                        personList.Add(new ContactPerson(id, firstName, lastName, adddress, city, state, email, zip, phoneNumber));
                        id++;
                        break;

                    case 2:
                        Console.WriteLine("\nADDRESS BOOK\n------------------");
                        foreach(ContactPerson person in personList)
                        {
                            person.toString();
                        }
                        break;

                    case 3:
                        editContact(personList);
                        break;

                    case 4:
                        Console.WriteLine("Enter the contact id that need to be deleted");
                        int personId = Convert.ToInt32(Console.ReadLine());
                        personList.RemoveAll(person => person.id == personId);
                        Console.WriteLine("The contact is deleted");
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
