using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module13.DTO
{
    public class Order
    {
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int ProductId { get; set; }

        public Order()
        {
        }

        public Order(int status, DateTime createdDate, DateTime updatedDate, int productId) : this(0, status, createdDate, updatedDate, productId)
        {
        }

        public Order(OrderStatus status, DateTime createdDate, DateTime updatedDate, int productId) : this(0, status, createdDate, updatedDate, productId)
        {
        }

        public Order(int id, int status, DateTime createdDate, DateTime updatedDate, int productId) : this(id, (OrderStatus)status, createdDate, updatedDate, productId)
        {
        }

        public Order(int id, OrderStatus status, DateTime createdDate, DateTime updatedDate, int productId)
        {
            this.Id = id;
            this.Status = status;
            this.CreatedDate = createdDate;
            this.UpdatedDate = updatedDate;
            this.ProductId = productId;
        }
    }
}
