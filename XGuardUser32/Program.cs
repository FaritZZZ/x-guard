using XGuardLibrary;
using XGuardUser32;
using System.Runtime.InteropServices;
using XGuardUser32.Services;
public class Program
{
    [DllImport("user32.dll")]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    // Константы для управления видимостью окна
    private const int SW_HIDE = 0;
    private const int SW_SHOW = 5;

    public static async Task Main(string[] args)
    {

        string windowName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "XGuardUser32.exe");
        IntPtr hWnd = FindWindow(null, windowName);

        if (hWnd != IntPtr.Zero)
        {
            // Скрыть окно
            ShowWindow(hWnd, SW_HIDE);
            Console.WriteLine("Окно скрыто.");
        }
        else
        {
            Console.WriteLine("Окно не найдено.");
        }

        ControlBlocking.LoopBloking();
        CloseWindows.Run();
        bool value =false;
        while (true)
        {
            try
            {
                value = FileTrueOrFalse.WhichFile();
                LockAnotherThings.BlockingLogic = value;
                //KeyboardAndMouseService.BlockInput(value);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error on User32: {ex}");
                await Task.Delay(500);
            }
        }
    }
}
