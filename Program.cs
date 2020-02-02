using System;
using System.Collections.Generic;
using System.Linq;
namespace SmallCrm
{
    class Program
    {
        /*In this project I created the backbone of a small CRM(Customer Managment System)
        * First of all, I created the productList of the crm, that contains all existed 
        * products. For creating this list, I parse a product.csv file that contains,
        * id and the name-description of each product. Then, I call a function
        * generateProductId and generatePrice, to assign price and ID to each product.
        * For testing the functionality of this project I create 2 objects of 
        * customer class that make 2 customers. Then I create 2 orders with class Order. 
        * When created ever instance of class order, automatically generate a unique 
        * indentification OrderId for each order. Then, I add each order, into the orderlist of 
        * each Customer, and print into console which products bought every customers,
        * the details of product, order and customers and who spend the most.
        * Also, the program prints the 10 top seller product. 
        */
        public static List<Order> OrderList = new List<Order>();
        static void Main(string[] args)
        {
            //Create a new List with all products
            var ProductList = Productlist();
            /*
             * Create 2 customers
             * Create 2 orders
             * Generate a unique orderId for each order
             * Insert each order in each customer orderlist
             */
            var Customer1 = new Customer("Dimitris","Pnevmatikos");
            var Customer2 = new Customer("Giorgos","Stathis");
            var Order1 = new Order();
            var Order2 = new Order();

            //Add order to each customer and then add them to global OrderList of crm.
            Order1.OrderProductList = (CustomerProductListGenerate(ProductList));
            Customer1.Orders.Add(Order1);
            Order2.OrderProductList = (CustomerProductListGenerate(ProductList));
            Customer2.Orders.Add(Order2);
            OrderList.Add(Order1);
            OrderList.Add(Order2);

            /*For each order, print to console order details and data of each customer.*/
            Order2.OrderId = RandomGeneratorOrderId();
            Customer1.TotalMoney = +GetCostOfOrder(Order1.OrderProductList);
            Customer2.TotalMoney = +GetCostOfOrder(Order2.OrderProductList);

            Console.WriteLine($"\nThe products from order of {Customer1.FirstName} " +
                $"{Customer1.LastName},with Orderid {Order1.OrderId} and the cost of them is " +
                $"{Customer1.TotalMoney}");

            foreach (Order i in Customer1.Orders)
            {
                PrintTheProductsOfEachOrder(i.OrderProductList);
            }

            Console.WriteLine($"\nThe products from order of {Customer2.FirstName} " +
                $"{Customer2.LastName}, with Orderid {Order2.OrderId} and the cost of them is " +
                $"{Customer2.TotalMoney}");

            foreach (Order i in Customer2.Orders)
            {
                PrintTheProductsOfEachOrder(i.OrderProductList);
            }
           
            /*
             * Create and print a list with top seller products
             * Create a list with all Customers and add each customer
             */
            TopSellerProduct(ProductList);

            var customerlist = new List<Customer>();
            customerlist.Add(Customer1);
            customerlist.Add(Customer2);

            Console.WriteLine($"The most Valuable Customer is: " +
                $"{GetValuableCustomer(customerlist).LastName} " +
                $"{GetValuableCustomer(customerlist).FirstName}");
        }

        //Function for generate Unique order id
        public static string RandomGeneratorOrderId()
        {
            Random r = new Random();
            var randomNum = r.Next(1, 1000);
            var rrandomId = randomNum.ToString("#A#" + r.Next(1, 50) + "#Z#");
            if (OrderList.Exists(x=>x.OrderId == rrandomId)) { 
                RandomGeneratorOrderId();
            } 
            return rrandomId;
        }

        //Generate and return a random productList of 10 products
        public static List<Product> CustomerProductListGenerate(List<Product> productList)
        {
            var CustomerProductList = new List<Product>();
            Random r = new Random();
            for (var i = 0; i < 10; i++){
                var randomIndex = r.Next(0, productList.Count);
                CustomerProductList.Add(productList[randomIndex]);
                productList[randomIndex].SoldNum++;
            }
            return CustomerProductList;
        }

        //Create the productList of crm, from parcing the .csv file and print them.
        public static List<Product> Productlist()
        {
            var lines = System.IO.File.ReadLines("products.csv");
            var ProductList = new List<Product>();
            foreach (string line in lines){
                var pros = line.Split(";");
                if (pros[0]==null || pros[1]==null){
                    throw new Exception("A product cant has null values");
                }
                if (!ProductList.Any(z => z.ProductId.Equals(pros[0]))){
                    var pro = new Product();
                    pro.ProductId = (pros[0]);
                    pro.Description = pros[1];
                    pro.Price = GetRandomPrice();
                    ProductList.Add(pro);
                }else {
                    Console.WriteLine("Can't add this product in productlist " +
                        "because it already exist a product with same ID");
                }
            }
            PrintAllProductList(ProductList);
            return ProductList;
        }

        //Generate a random decimal number and assign to each product as price
        public static decimal GetRandomPrice()
        {
            Random r = new Random();
            var randomNum = r.NextDouble() + r.Next(0, 1500);
            var rrandomPrice = randomNum.ToString("0.00");
            var price = System.Convert.ToDecimal(rrandomPrice);
            if ( price <= 0.00M){
                GetRandomPrice();
            }
            return price;
        }

        //Print all the products of productList
        public static void PrintAllProductList(List<Product> productList)
        {
            Console.WriteLine("The list of products is:");
            foreach (Product p in productList){
                Console.WriteLine($" {p.Description} {p.ProductId} {p.Price} {p.SoldNum} ");
            }
            Console.WriteLine($"The size of product list is {productList.Count} products");
        }

        //Print the product details of each order
        public static void PrintTheProductsOfEachOrder(List<Product> productList)
        {
            foreach (Product p in productList){
                Console.WriteLine(p.ProductId + " " + p.Description + " " + p.Price);
            }
        }

        //Find the top 10 best seller products and print them to console.
        public static void TopSellerProduct(List<Product> ProductList)
        {
            Console.WriteLine("\nThe best seller products are");
            var list1 = ProductList.OrderByDescending(x => x.SoldNum).Take(10);
            foreach (Product i in list1) {
                Console.WriteLine($"{i.ProductId} {i.Description} {i.Price} {i.SoldNum}");
            }
        }

        //Calculate and return the sum of value from an orderlist
        public static decimal GetTheSumOfCost(List<Order> CustomerOrders)
        {
            if (CustomerOrders == null){   
                Console.WriteLine($"The CustomerOrder list cant be null \n");
                throw new ArgumentNullException();
            }
            decimal totalMoney=0;
            foreach (Order i in CustomerOrders){
                totalMoney += i.TotalMount;
            }
            return totalMoney;
        }

        //Calculate and return the customer from a customer list, who spends the most 
        public static Customer GetValuableCustomer(List<Customer> customerlist)
        {
            var custom = customerlist.OrderByDescending(p => p.TotalMoney).First();
            return custom;
        }

        //Calculate and return the total cost of a product list
        public static Decimal GetCostOfOrder(List<Product> productList)
        {
            var totalCost = 0.0M;
            foreach (Product i in productList){
                totalCost += i.Price;
            }
            return totalCost;
        }
    }
}