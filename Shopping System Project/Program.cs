using System;

namespace Shopping_System_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            ShoppingCart cart = new ShoppingCart();

            Product apple = new Product(1, "Apple", 0.5);
            Product banana = new Product(2, "Banana", 0.3);
            Product orange = new Product(3, "Orange", 0.7);

            Console.WriteLine("\nAdding items to cart...");
            cart.Add(apple);
            cart.Add(apple);
            cart.Add(banana);
            cart.ViewCart();

            Console.WriteLine("\nRemoving 1 Apple from cart...");
            cart.RemoveItem(apple);
            cart.ViewCart();

            Console.WriteLine("\nTrying to remove Orange (not in cart)...");
            cart.RemoveItem(orange);

            Console.WriteLine("\nUndoing last action...");
            cart.Undo();
            cart.ViewCart();

            Console.WriteLine("\nChecking total price before checkout...");
            cart.Checkout();

            Console.WriteLine("\nShopping session complete");
            
            Console.ReadKey();
        }
    }
}
