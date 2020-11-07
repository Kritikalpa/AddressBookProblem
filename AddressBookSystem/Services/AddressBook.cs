using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AddressBookSystem.Services
{
    class AddressBook
    {
        public static string connectionString = "Data Source=(LocalDb)\\testServer;Initial Catalog=addressBook_service;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);

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

        Dictionary<ContactPerson, string> cityPersonMap = new Dictionary<ContactPerson, string>();
        Dictionary<ContactPerson, string> statePersonMap = new Dictionary<ContactPerson, string>();

        public void AddContact()
        {
            SqlConnection connection = new SqlConnection(connectionString);
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
                Console.WriteLine("\nENTER A VALID ZIP");
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
                this.cityPersonMap.Add(newPerson, city);
                this.statePersonMap.Add(newPerson, state);
                id++;
            }
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SpAddContact", this.connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@first_name", firstName);
                    command.Parameters.AddWithValue("@last_name", lastName);
                    command.Parameters.AddWithValue("@address", adddress);
                    command.Parameters.AddWithValue("@city", city);
                    command.Parameters.AddWithValue("@state", state);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@zip", zip);
                    command.Parameters.AddWithValue("@phone_number", phoneNumber);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine("{0} rows affected", result);
                    }
                    Console.WriteLine("Error while Adding contact");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        public void viewContacts()
        {
            Console.WriteLine("\nADDRESS BOOK\n------------------");
            foreach (ContactPerson person in personList)
            {
                Console.WriteLine(person.toString());
            }
        }

        public void editContact()
        {
            bool userInput = true;
            int personId = 1;
            KeyValuePair<ContactPerson, string> entry;
            while (userInput)
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
                    entry = cityPersonMap.First(entry => ((entry.Key.firstName).Equals(contactPerson.firstName)));
                    if (!entry.Key.Equals(null))
                    {
                        cityPersonMap.Remove(entry.Key);
                    }
                    cityPersonMap.Add(contactPerson, city);
                    break;
                case 5:
                    Console.WriteLine("\nEnter the state:");
                    string state = Console.ReadLine();
                    contactPerson.state = state;
                    entry = statePersonMap.First(entry => ((entry.Key.firstName).Equals(contactPerson.firstName)));
                    if (!entry.Key.Equals(null))
                    {
                        statePersonMap.Remove(entry.Key);
                    }
                    statePersonMap.Add(contactPerson, state);
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

        public void RetrieveDataFromDB(int choice)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spRetrieveContact", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@choice", choice);

                    connection.Open();

                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ContactPerson person = new ContactPerson(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6), (long)dr.GetInt32(7), (long)dr.GetInt32(8));
                            List<ContactPerson> persons = this.personList.FindAll(person => person.firstName.Equals(dr.GetString(1)));
                            bool flag = false;
                            foreach (ContactPerson personOb in persons)
                            {
                                flag = person.Equals(personOb);
                                if (flag == true)
                                {
                                    Console.WriteLine("ID {0}. Duplicate Entry Found.", dr.GetInt32(0));
                                    break;
                                }
                            }
                            if (flag == false)
                            {
                                this.personList.Add(person);
                                this.cityPersonMap.Add(person, dr.GetString(4));
                                this.statePersonMap.Add(person, dr.GetString(5));
                                id++;
                                Console.WriteLine(person.toString());
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }

                    dr.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                //connection.Close();
            }
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

        public void groupByCityOrState(string choice)
        {
            int count = 0;
            switch (choice)
            {
                case "city":
                    var personGroupedByCity = cityPersonMap.GroupBy(entry => entry.Value);
                    foreach(var group in personGroupedByCity)
                    {
                        Console.WriteLine("\nPersons from city : {0}", group.Key);
                        foreach(var person in group)
                        {
                            Console.WriteLine("> " + person.Key.toString());
                            count++;
                        }
                        Console.WriteLine("Number of person in {0} : {1}", group.Key, count);
                    }
                    break;

                case "state":
                    var personGroupedByState = statePersonMap.GroupBy(entry => entry.Value);
                    foreach (var group in personGroupedByState)
                    {
                        Console.WriteLine("\nPersons from state : {0}", group.Key);
                        foreach (var person in group)
                        {
                            Console.WriteLine("> " + person.Key.toString());
                            count++;
                        }
                        Console.WriteLine("Number of person in {0} : {1}", group.Key, count);
                    }
                    break;

            }
        }
        public void ReadContact()
        {
            string path = @"C:\Users\krtkl\source\repos\AddressBookSystem\AddressBookSystem\Services\ContactText.txt";
            using (StreamReader sr = File.OpenText(path))
            {
                String s = "";
                Console.WriteLine("Contact read from file successfully.\nAddress Book Name : {0} \n", this.bookName);
                while((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }

        public void WriteContact()
        {
            string path = @"C:\Users\krtkl\source\repos\AddressBookSystem\AddressBookSystem\Services\ContactText.txt";
            using (StreamWriter sr = File.CreateText(path))
            {
                foreach(ContactPerson person in personList)
                {
                    sr.WriteLine(person.toString());
                }
                Console.WriteLine("Contacts inserted in file");
            }
        }

        public void ReadCSV()
        {
            string path = @"C:\Users\krtkl\source\repos\AddressBookSystem\AddressBookSystem\Services\ContactCSV.csv";

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<ContactPerson>().ToList();
                Console.WriteLine("Read data successfull from csv file");
                foreach (ContactPerson person in records)
                {
                    Console.WriteLine(person.toString());
                }
            }
        }

        public void WriteCSV()
        {
            string path = @"C:\Users\krtkl\source\repos\AddressBookSystem\AddressBookSystem\Services\ContactCSV.csv";

            using (var writer = new StreamWriter(path))
            using (var csvExport = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvExport.WriteRecords(personList);
                Console.WriteLine("Write data successfull to csv file");
            }
        }

        public void ReadJSON()
        {
            string path = @"\Users\krtkl\source\repos\AddressBookSystem\AddressBookSystem\Services\ContactJSON.json";

            var records = JsonConvert.DeserializeObject<IList<ContactPerson>>(File.ReadAllText(path));
            Console.WriteLine("Read data successfull from JSON file");
            foreach (ContactPerson person in records)
            {
                Console.WriteLine(person.toString());
            }
        }

        public void WriteJSON()
        {
            string path = @"\Users\krtkl\source\repos\AddressBookSystem\AddressBookSystem\Services\ContactJSON.json";

            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter writer = new StreamWriter(path))
            using (var JsonExport = new JsonTextWriter(writer))
            {
                serializer.Serialize(writer, personList);
                Console.WriteLine("Write data successfull to JSON file");
            }
        }
    }
}
