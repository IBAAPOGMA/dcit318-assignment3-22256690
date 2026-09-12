using System;

namespace InventorySystem
{
    public class InventoryApp
    {
        private InventoryLogger<InventoryItem> _logger;

        public InventoryApp(string filePath)
        {
            _logger = new InventoryLogger<InventoryItem>(filePath);
        }

        public void SeedSampleData()
        {
            _logger.Add(new InventoryItem(1, "Rice Bag", 20, DateTime.Now));
            _logger.Add(new InventoryItem(2, "Cooking Oil", 15, DateTime.Now));
            _logger.Add(new InventoryItem(3, "Sugar Bag", 30, DateTime.Now));
            _logger.Add(new InventoryItem(4, "Tomato Paste", 50, DateTime.Now));
            _logger.Add(new InventoryItem(5, "Milk Powder", 25, DateTime.Now));
        }

        public void SaveData()
        {
            _logger.SaveToFile();
        }

        public void LoadData()
        {
            _logger.LoadFromFile();
        }

        public void PrintAllItems()
        {
            foreach (var item in _logger.GetAll())
            {
                Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Date Added: {item.DateAdded}");
            }
        }
    }
}