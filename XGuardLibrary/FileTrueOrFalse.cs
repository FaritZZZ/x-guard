using System.Diagnostics;
using System.IO;

namespace XGuardLibrary
{
    public static class FileTrueOrFalse
    {        
        // Создает файл True, если получает true. Если получает false, удаляет файл если есть
        public static void CreateFile(bool fileName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"True.txt");
            if (!fileName && File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            if (fileName && !File.Exists(filePath))
            {
                File.Create(filePath).Dispose(); // Создаем файл и сразу закрываем поток                
            }
        }

        // Если находит файл с именем True возвращает true, а иначе false
        public static bool WhichFile()
        {
            string fileTrue = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "True.txt");
            if (File.Exists(fileTrue))
            {
                return true;
            }
            return false;
        }
    }
}
