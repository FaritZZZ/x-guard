using System.Runtime.InteropServices;

namespace XGuardUser32.Services
{
    public class KeyboardAndMouseService
    {
        // Импорт функции BlockInput из user32.dll
        [DllImport("user32.dll")]
        public static extern bool BlockInput(bool blockIt);
    }
}
