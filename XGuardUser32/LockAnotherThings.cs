using XGuardUser32.Services;

namespace XGuardUser32
{
    public static class LockAnotherThings
    {
        private static bool _switsher;

        public static bool BlockingLogic

        {
            get => _switsher;
            set
            {
                if (_switsher == value) return;

                _switsher = value;

                TaskBarService.HideTaskBar(_switsher);
                //KeyboardAndMouseService.BlockInput(_switsher);
                //Logger.Info($"_switsher = {_switsher.ToString()}");
                //AllWindows.Hide(_switsher);
            }
        }
    }
}
