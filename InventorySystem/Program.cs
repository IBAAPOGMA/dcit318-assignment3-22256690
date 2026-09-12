using System;

namespace InventorySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "inventory.json";

            Console.WriteLine("--- Starting Inventory App (Session 1) ---");
            InventoryApp app = new InventoryApp(filePath);

            app.SeedSampleData();
            app.SaveData();

            Console.WriteLine();
            Console.WriteLine("--- Simulating a new session ---");
            Console.WriteLine();

            // Simulate a new session by creating a brand new InventoryApp instance.
            // This one starts with an empty log, so anything printed below
            // must have come from the file, not memory.
            InventoryApp newSessionApp = new InventoryApp(filePath);
            newSessionApp.LoadData();
            newSessionApp.PrintAllItems();

            Console.WriteLine();
            Console.WriteLine("--- Done ---");
        }
    }
}
