using XGuardUser32.Services;

namespace XGuardUser32
{
    public class ControlBlocking
    {
        public static async void LoopBloking()
        {
            ulong countFalse = 0;
            while (true)
            {
                if (LockAnotherThings.BlockingLogic)
                {
                    KeyboardAndMouseService.BlockInput(true);
                    countFalse = 0;
                }
                else
                {
                    countFalse += 1;
                    if (countFalse < 100)
                    {
                        KeyboardAndMouseService.BlockInput(false);
                    }
                    if (countFalse == 9999999999999999)
                    {
                        countFalse = 0;
                    }
                }
                await Task.Delay(100);
            }

        }
    }
}
