using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookSystem.Services
{
    public class ContactPerson
    {
        public string firstName { get; set; }
        public string lastName{ get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string email { get; set; }
        public long zip { get; set; }
        public long phoneNumber { get; set; }
        public int id { get; set; }


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

        public string toString()
        {
            
            return this.id + ". " +this.firstName + " " + this.lastName + ", " + this.address + ", " + this.city + ", " + this.state + ", " + this.zip + ", " + this.phoneNumber + ", " + this.email;
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
