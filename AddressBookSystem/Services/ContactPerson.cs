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


        public ContactPerson(int id, string firstName, string lastName, string address, string city, string state, string email, long zip, long phoneNumber)
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
            
            Console.WriteLine(this.id + ". " +this.firstName + " " + this.lastName + ", " + this.address + ", " + this.city + ", " + this.state + ", " + this.zip + ", " + this.phoneNumber + ", " + this.email);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            else
            {
                ContactPerson person = (ContactPerson)obj;
                return (this.firstName == person.firstName) && (this.lastName == person.lastName)
                    && (this.address == person.address) && (this.city == person.city) && (this.state == person.state)
                    && (this.email == person.email) && (this.zip == person.zip) && (this.phoneNumber == person.phoneNumber);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
