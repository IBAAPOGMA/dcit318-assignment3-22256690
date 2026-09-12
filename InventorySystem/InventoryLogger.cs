using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace InventorySystem
{
    public class InventoryLogger<T> where T : IInventoryEntity
    {
        private List<T> _log = new List<T>();
        private string _filePath;

        public InventoryLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void Add(T item)
        {
            _log.Add(item);
        }

        public List<T> GetAll()
        {
            return _log;
        }

        public void SaveToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(_log);
                using (StreamWriter writer = new StreamWriter(_filePath))
                {
                    writer.Write(json);
                }
                Console.WriteLine($"Data saved successfully to {_filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        public void LoadFromFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(_filePath))
                {
                    string json = reader.ReadToEnd();
                    List<T>? loadedItems = JsonSerializer.Deserialize<List<T>>(json);
                    if (loadedItems != null)
                    {
                        _log = loadedItems;
                    }
                }
                Console.WriteLine($"Data loaded successfully from {_filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }
    }
}