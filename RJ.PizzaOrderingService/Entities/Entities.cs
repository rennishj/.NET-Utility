using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RJ.PizzaOrderingService.Entities
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Description { get; set; }
    }

    [DataContract]
    public class Customer
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Street { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Zip { get; set; }

    }

    [DataContract]
    public class Order
    {
        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public List<OrderItem> OrderItems { get; set; }
        [DataMember]
        public DateTime OrderDate { get; set; }
        [DataMember]
        public decimal ItemsTotal { get; set; }
        [DataMember]
        public int OrderStatusId { get; set; }
    }

    [DataContract]
    public class OrderItem
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public int ProductId { get; set; }
        [DataMember]
        public int Qty { get; set; }
        [DataMember]
        public int UnitPrice { get; set; }
        [DataMember]
        public int TotalPrice { get; set; }
    }
}
