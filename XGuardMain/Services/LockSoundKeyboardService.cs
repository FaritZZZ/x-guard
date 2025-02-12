namespace XGuard.Services
{
    public static class LockSoundKeyboardService
    {
        private static bool _switsher;

        public static bool BlockingLogic

        {
            get => _switsher;
            set
            {
                if (_switsher == value) return;

                _switsher = value;

                SoundService.MuteVolume(_switsher);
                KeyboardMouseService.BlockInput(_switsher);
            }
        }
    }
}
