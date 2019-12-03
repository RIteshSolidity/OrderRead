using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderRead
{
    public interface IEvents
    {
        string EventType { get; set; }
    }
    public class OrderCreated : IEvents
    {
        public int OrderId { get; set; }
        public CustomerName customer { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderLineItems> OrderItems { get; set; }
        public string EventType { get; set; }
    }



    //  value type
    public class CustomerName 
    {



        public string FirstName { get;  set; }
        public string LastName { get;  set; }

        public string Location { get;  set; }

        

    }

    //Entity
    public class OrderLineItems
    {


        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }


    }
}
