using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace XGuardLibrary
{
    public static class FileHandler
    {
        private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sharedData.txt");

        // Статический конструктор для проверки наличия файла
        static FileHandler()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose(); // Создаем файл и сразу закрываем поток
                
            }
        }

        // Метод для записи данных в файл
        public static void WriteToFile(string message)
        {
            File.WriteAllText(filePath, string.Empty);
            File.WriteAllText(filePath, message);
            Console.WriteLine("Данные записаны в файл: " + filePath);
        }

        // Метод для чтения данных из файла
        public static string ReadFromFile()
        {
            return File.ReadAllText(filePath);
        }
    }

}
