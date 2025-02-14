using System;
using System.Collections.Generic;

namespace Shopping_System_Project
{
    // Product Class (Represents an Item in the Store)
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }

    // CartItem Class (Represents an Item in the Cart)
    class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }

    // Shopping Cart Class 
    class ShoppingCart
    {
        private Dictionary<int, CartItem> Products { get; set; } = new Dictionary<int, CartItem>();
        private Stack<Action> undoStack = new Stack<Action>(); // Undo feature

        // Add Product to Cart
        public void Add(Product product)
        {
            if (Products.ContainsKey(product.Id))
                Products[product.Id].Quantity++;
            else
                Products[product.Id] = new CartItem(product, 1);

            // Add undo operation
            undoStack.Push(() => RemoveItem(product, true));

            Console.WriteLine($"{product.Name} added to cart");
        }

        // View Cart
        public void ViewCart()
        {
            if (Products.Count == 0)
            {
                Console.WriteLine("Cart is Empty :-)");
                return;
            }

            Console.WriteLine("\nCart Contents:");
            foreach (var item in Products.Values)
                Console.WriteLine($"ID: {item.Product.Id}, Name: {item.Product.Name}, Quantity: {item.Quantity}, Price per unit: ${item.Product.Price}");

            Console.WriteLine($"Total Price: ${CalculateTotal()}");
        }

        // Remove Item from Cart
        public void RemoveItem(Product product, bool isUndo = false)
        {
            if (Products.ContainsKey(product.Id))
            {
                if (Products[product.Id].Quantity > 1)
                    Products[product.Id].Quantity--;
                else
                    Products.Remove(product.Id);

                if (!isUndo)
                    undoStack.Push(() => Add(product));

                Console.WriteLine($"Removed {product.Name} from cart");
            }
            else
                Console.WriteLine("This item isn't in the cart!");
        }

        // Calculate Total Price
        public double CalculateTotal()
        {
            double total = 0;
            foreach (var item in Products.Values)
                total += item.Product.Price * item.Quantity;

            return total;
        }

        // Checkout (Clears Cart)
        public void Checkout()
        {
            if (Products.Count == 0)
            {
                Console.WriteLine("Cart is empty Nothing to checkout");
                return;
            }

            Console.WriteLine("Checkout Summary:");
            foreach (var item in Products.Values)
                Console.WriteLine($"{item.Product.Name} x {item.Quantity} = ${item.Product.Price * item.Quantity}");

            Console.WriteLine($"Total Price: ${CalculateTotal()}");
            Console.WriteLine("Thank you for your purchase!");

            Products.Clear();
            undoStack.Clear(); // Clear undo history after checkout
        }

        // Undo Last Action
        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                undoStack.Pop()?.Invoke();
                Console.WriteLine("Last action undone");
            }
            else
                Console.WriteLine("No actions to undo :-)");
        }

        // Clear Cart
        public void ClearCart()
        {
            Products.Clear();
            undoStack.Clear();
            Console.WriteLine("Cart has been cleared :-)");
        }
    }

   
}
