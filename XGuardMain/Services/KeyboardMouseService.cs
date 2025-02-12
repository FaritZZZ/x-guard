using System.Runtime.InteropServices;

namespace XGuard.Services
{
    public static class KeyboardMouseService
    {
        // Импортируем необходимые функции из user32.dll
        [DllImport("user32.dll")]
        public static extern int BlockInput(bool blockIt);
    }
}
