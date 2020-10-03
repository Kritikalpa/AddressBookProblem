using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AddressBookSystem.Services
{
    class AddressBook
    {
        private static Regex reName = new Regex(@"^[a-zA-Z\s]+$");
        private static Regex reZip = new Regex(@"^[0-9]{6}$");
        private static Regex reEmail = new Regex(@"\A(?:[a-z0-9!#$%&'*.+/=?^_`{|}~-]+@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
        private static Regex rePhone = new Regex(@"^[0-9]{10}$");
        private static int id = 1;
        public string bookName;

        public AddressBook(string bookName)
        {
            this.bookName = bookName;
        }

        private List<ContactPerson> personList = new List<ContactPerson>();

        public void AddContact()
        {
            Console.WriteLine("Create a contact");
            Console.WriteLine("----------------");
            Console.WriteLine("Enter the first name:");
            string firstName = Console.ReadLine();
            while (!reName.IsMatch(firstName))
            {
                Console.WriteLine("\nENTER A VALID fIRST NAME");
                firstName = Console.ReadLine();
            }
            Console.WriteLine("\nEnter the last name:");
            string lastName = Console.ReadLine();
            while (!reName.IsMatch(lastName))
            {
                Console.WriteLine("\nENTER A VALID LAST NAME");
                lastName = Console.ReadLine();
            }
            Console.WriteLine("\nEnter the address:");
            string adddress = Console.ReadLine();
            Console.WriteLine("\nEnter the city:");
            string city = Console.ReadLine();
            Console.WriteLine("\nEnter the state:");
            string state = Console.ReadLine();
            Console.WriteLine("\nEnter the zip:");
            long zip = long.Parse(Console.ReadLine());
            while (!reZip.IsMatch(zip.ToString()))
            {
                Console.WriteLine("\nENTER A VALID EMAIL");
                zip = long.Parse(Console.ReadLine());
            }
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

            this.personList.Add(new ContactPerson(id, firstName, lastName, adddress, city, state, email, zip, phoneNumber));
            id++;
        }

        public void viewContacts()
        {
            Console.WriteLine("\nADDRESS BOOK\n------------------");
            foreach (ContactPerson person in personList)
            {
                person.toString();
            }
        }

        public void editContact()
        {
            Console.WriteLine("\nEnter the id");
            int id = Convert.ToInt32(Console.ReadLine());
            ContactPerson contactPerson = null;
            foreach (ContactPerson person in personList)
            {
                if (person.id == id)
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


        public void DeleteContact()
        {
            Console.WriteLine("Enter the contact id that need to be deleted");
            int personId = Convert.ToInt32(Console.ReadLine());
            this.personList.RemoveAll(person => person.id == personId);
            Console.WriteLine("The contact is deleted");
        }

    }
}
