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
    }
}
