using System.ComponentModel;
using System.Runtime.InteropServices;

namespace XGuardUser32.Services
{
    public static class TaskBarService
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern nint FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ShowWindow(nint hWnd, int nCmdShow);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        public static void HideTaskBar(bool notshow)
        {
            // Найти панель задач
            nint taskbarHandle = FindWindow("Shell_TrayWnd", null);
            //Logger.Warn($"taskbarHandle = {taskbarHandle}");

            if (taskbarHandle == nint.Zero)
            {
                int errorCode = Marshal.GetLastWin32Error();
                string errorMessage = new Win32Exception(errorCode).Message;
                //Logger.Warn($"Не удалось найти панель задач. Ошибка: {errorCode}, Сообщение: {errorMessage}");
                return;
            }

            // Показать или скрыть панель задач
            bool result = ShowWindow(taskbarHandle, notshow ? SW_HIDE : SW_SHOW);
            if (!result)
            {
                int errorCode = Marshal.GetLastWin32Error();
                string errorMessage = new Win32Exception(errorCode).Message;
                //Logger.Warn($"Не удалось изменить состояние панели задач. Ошибка: {errorCode}, Сообщение: {errorMessage}");
            }
        }
    }
}
