using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookSystem.Services
{
    class ContactPerson
    {
        public string firstName, lastName, address, city, state, email;
        public long zip, phoneNumber;
        public int id;

        public ContactPerson(int id, string firstName,string lastName, string address, string city, string state, string email, long zip, long phoneNumber)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.city = city;
            this.state = state;
            this.email = email;
            this.zip = zip;
            this.phoneNumber = phoneNumber;
        }

        public void toString()
        {
            
            Console.WriteLine(this.firstName + " " + this.lastName + ", " + this.address + ", " + this.city + ", " + this.state + ", " + this.zip + ", " + this.phoneNumber + ", " + this.email);
        }
    }
}
