using System.Collections.Generic;

namespace SmallCrm
{
    class Order
    {
        private string Orderid;
        /// <summary>
        /// The id of Order
        /// </summary>
        public string OrderId{ get; set; }

        /// <summary>
        /// Delivery address of order
        /// </summary>
        public string DeliveryAddress { get; set; }

        /// <summary>
        /// Totalmount of order
        /// </summary>
        public decimal TotalMount { get; set; }

        /// <summary>
        /// Product list of order
        /// </summary>
        public List<Product> OrderProductList { get; set; }

        /// <summary>
        /// Constructor for Order, generate order Id
        /// </summary>
        public Order()
        {
            OrderId = Program.RandomGeneratorOrderId();
        }
    }
}
