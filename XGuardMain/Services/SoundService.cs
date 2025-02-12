using NAudio.CoreAudioApi;

namespace XGuard.Services
{
    public static class SoundService
    {
        private static MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
        private static MMDevice device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

        public static void MuteVolume(bool sound)
        {
            device.AudioEndpointVolume.Mute = sound;
        }
    }
}
