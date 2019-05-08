using System;

namespace LiteDbDemo.Collections
{
    public class Customer
    {
        public Guid CustomerId { get; private set; }
        public string Name { get; set; }

        public string[] Phones { get; set; }

        public bool IsActive { get; set; }
    }
}
