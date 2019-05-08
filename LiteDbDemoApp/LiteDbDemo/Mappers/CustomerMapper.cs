using LiteDbDemo.Collections;

namespace LiteDbDemo.Mappers
{
    public class CustomerMapper : BaseMapper
    {
        public CustomerMapper()
        {
            mapper.Entity<Customer>()
                .Id(x => x.CustomerId) // set your document ID
                                       //.Ignore(x => x.Phones) // ignore this property (do not store)
                .Field(x => x.Name, "cust_name") // rename document field
                .Field(x => x.Phones, "My Phones");
        }
    }
}
