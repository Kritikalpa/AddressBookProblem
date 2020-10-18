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

        public List<ContactPerson> personList = new List<ContactPerson>();

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
            string zip = Console.ReadLine();
            while (!reZip.IsMatch(zip))
            {
                Console.WriteLine("\nENTER A VALID EMAIL");
                zip = Console.ReadLine();
            }
            Console.WriteLine("\nEnter the email:");
            string email = Console.ReadLine();
            while (!reEmail.IsMatch(email))
            {
                Console.WriteLine("\nENTER A VALID EMAIL");
                email = Console.ReadLine();
            }
            Console.WriteLine("\nEnter the phone number:");
            string phoneNumber = Console.ReadLine();
            while (!rePhone.IsMatch(phoneNumber))
            {
                Console.WriteLine("\nENTER A VALID PHONE NUMBER");
                phoneNumber = Console.ReadLine();
            }
            ContactPerson newPerson = new ContactPerson(id, firstName, lastName, adddress, city, state, email, long.Parse(zip), long.Parse(phoneNumber));
            List<ContactPerson> persons = this.personList.FindAll(person => person.firstName.Equals(firstName));
            bool flag = false;
            foreach(ContactPerson person in persons){
                flag = newPerson.Equals(person);
                if (flag == true)
                {
                    Console.WriteLine("\nContact Already Exists");
                    break;
                }
            }
            if(flag == false)
            {
                this.personList.Add(newPerson);
                id++;
            }  
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
            bool userInput = true;
            int personId = 1;
            while(userInput)
            {
                try
                {
                    Console.WriteLine("\nEnter the id");
                    personId = Convert.ToInt32(Console.ReadLine());
                    userInput = false;
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            ContactPerson contactPerson = null;
            foreach (ContactPerson person in personList)
            {
                if (person.id == personId)
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
                    while (!reName.IsMatch(firstName))
                    {
                        Console.WriteLine("\nENTER A VALID fIRST NAME");
                        firstName = Console.ReadLine();
                    }
                    contactPerson.firstName = firstName;
                    break;
                case 2:
                    Console.WriteLine("\nEnter the last name:");
                    string lastName = Console.ReadLine();
                    while (!reName.IsMatch(lastName))
                    {
                        Console.WriteLine("\nENTER A VALID LAST NAME");
                        lastName = Console.ReadLine();
                    }
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
                    string zip = Console.ReadLine();
                    while (!reZip.IsMatch(zip))
                    {
                        Console.WriteLine("\nENTER A VALID EMAIL");
                        zip = Console.ReadLine();
                    }
                    contactPerson.zip = long.Parse(zip);
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
                    string phoneNumber = Console.ReadLine();
                    while (!rePhone.IsMatch(phoneNumber))
                    {
                        Console.WriteLine("\nENTER A VALID PHONE NUMBER");
                        phoneNumber = Console.ReadLine();
                    }
                    contactPerson.phoneNumber = long.Parse(phoneNumber);
                    break;
                default:
                    break;
            }
            Console.WriteLine("Contact details updated");
        }


        public void DeleteContact()
        {
            bool userInput = true;
            int personId = 1;
            while (userInput)
            {
                try
                {
                    Console.WriteLine("Enter the contact id that need to be deleted");
                    personId = Convert.ToInt32(Console.ReadLine());
                    userInput = false;
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            this.personList.RemoveAll(person => person.id == personId);
            Console.WriteLine("The contact is deleted");
        }

        public List<ContactPerson> searchPersonByPlace(string place)
        {
            List<ContactPerson> persons = this.personList.FindAll(person => person.city.Equals(place) || person.state.Equals(place));
            return persons;
        }

    }
}
