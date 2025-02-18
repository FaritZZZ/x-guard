using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using XGuardLibrary;

namespace XGuardUser32.Services
{
    public static class CloseWindows
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const uint WM_CLOSE = 0x0010; // Сообщение для закрытия окна

        public static async void Run()
        {

            
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "keywords.txt"); // Укажите путь к вашему файлу
            HashSet<string> words = ReadWordsFromFile(filePath);

            Thread.Sleep(1500);
            while (true)
            {
                IntPtr handleXGuardFolder = GetForegroundWindow();
                System.Text.StringBuilder windowTitleXGuardFolder = new System.Text.StringBuilder(256);
                if (GetWindowText(handleXGuardFolder, windowTitleXGuardFolder, windowTitleXGuardFolder.Capacity) > 0)
                {
                    string activeWindowTitle = windowTitleXGuardFolder.ToString();

                    if (activeWindowTitle.IndexOf("XGuard", StringComparison.OrdinalIgnoreCase) >= 0 && 
                        activeWindowTitle.IndexOf("XGuardLauncher.exe", StringComparison.OrdinalIgnoreCase) == -1 &&
                        activeWindowTitle.IndexOf("Visual Studio", StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        if (handleXGuardFolder != IntPtr.Zero)
                        {
                            PostMessage(handleXGuardFolder, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                        }
                    }
                }

                try
                {
                    IntPtr handle = GetForegroundWindow();
                    System.Text.StringBuilder windowTitle = new System.Text.StringBuilder(256);
                    if (File.Exists(filePath))
                    {
                        if (GetWindowText(handle, windowTitle, windowTitle.Capacity) > 0)
                        {
                            string activeWindowTitle = windowTitle.ToString();

                            foreach (string word in words)
                            {
                                if (activeWindowTitle.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    Thread.Sleep(1000);
                                    IntPtr handle2 = GetForegroundWindow();
                                    Console.WriteLine($"Активное окно имеет название {activeWindowTitle}");
                                    if (handle != IntPtr.Zero && handle2 == handle)
                                    {
                                        PostMessage(handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"Не удалось закрыть окно: {ex.ToString}");
                }
                
                await Task.Delay(300);
            }
        }

        static HashSet<string> ReadWordsFromFile(string filePath)
        {
            HashSet<string> words = new HashSet<string>(StringComparer.OrdinalIgnoreCase); // Игнорировать регистр

            foreach (var line in File.ReadLines(filePath))
            {
                // Разделяем строку на слова по пробелам и знакам препинания
                var lineWords = line.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in lineWords)
                {
                    words.Add(word);
                }
            }

            return words;
        }
    }
}
