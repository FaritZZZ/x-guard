using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGuardLibrary;

namespace XGuard.Services
{
    public static class RunUser32
    {
        private static readonly ProcessObserver _processObserver;
        static RunUser32()
        {
            _processObserver = new ProcessObserver("XGuardUser32", 1, true);
            //_processObserver.Enabled = false;
            
        }

        public static void Run()
        {
            _processObserver.Run();
        }

        //public static async void Run2()
        //{
        //    Logger.Info("Run");
        //    // Отправляем значение через Named Pipe
        //    string pathToExe = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "XGuardUser32.exe");
        //    // Создание процесса
        //    Process process = new Process();
        //    process.StartInfo.FileName = pathToExe;
            
        //    process.Start();
        //    // Проверка завершения процесса
        //    while (true)
        //    {
        //        if (process.HasExited)
        //        {
        //            process.Start();
        //            Logger.Info($"Перезапуск процесса {DateTime.Now}");
        //        }
        //        await Task.Delay(50); // Пауза 1 секунда
        //    }

        //    // Ожидание завершения (опционально)
        //    process.WaitForExit();
        //}
    }
}
