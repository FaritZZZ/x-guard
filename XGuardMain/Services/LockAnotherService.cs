using System.Runtime.InteropServices;
using XGuardLibrary;
using XGuardLibrary.Utilities;

namespace XGuard.Services
{
    public static class LockAnotherService
    {
        private static bool _switsher;
        public static bool BlockingLogic

        {
            get => _switsher;
            set
            {
                if (_switsher == value) return;

                _switsher = value;
                FileTrueOrFalse.CreateFile(_switsher);
                SoundService.MuteVolume(_switsher);
            }
        }
    }
}
