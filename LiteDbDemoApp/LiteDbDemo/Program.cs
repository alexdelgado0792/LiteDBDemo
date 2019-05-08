using LiteDB;
using LiteDbDemo.Collections;
using System;

namespace LiteDbDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {

            TestDb();
            //TestFiles();
            Console.WriteLine("Done.........");

        }

        private static void TestDb()
        {
            using (var helper = new LiteDbHelper<Customer>())
            {
                // Get a collection (or create, if doesn't exist)
                LiteCollection<Customer> col = helper.GetCollection();

                // Create your new customer instance
                var customer = new Customer
                {
                    Name = "IL AAAAAAA",
                    Phones = new string[] { "8000-0000", "9000-0000" },
                    IsActive = true
                };

               // Insert new customer document (Id will be auto-incremented)
                var id = helper.Insert(customer);


                // Update a document inside a collection
                customer.Name = "AL 111111";

                var ala = helper.InsertUpdate(customer);
                var borra = helper.DeleteById(id);


                //TODO: more examples
                // Index document using document Name property
                //col.EnsureIndex(x => x.Name);

                // Use LINQ to query documents
                //System.Collections.Generic.IEnumerable<Customer> results = col.FindAll();
                //Customer result = col.FindOne(x => x.Name.StartsWith("Jo"));

                // Let's create an index in phone numbers (using expression). It's a multikey index
                //col.EnsureIndex(x => x.Phones, "$.Phones[*]");

                // and now we can query phones
                //var r = col.FindOne(x => x.Phones.Contains("8888-5555"));


                helper.DropCollection();


            }
        }

        private static void TestFiles()
        {
            using (var helper = new LiteDbFileHelper())
            {
                helper.DownloadFile("my-photo-id", "TEST");
                helper.DownloadFile("pollito", "TESTPollo");
            }
        }
    }
}
