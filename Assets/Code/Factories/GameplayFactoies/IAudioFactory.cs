using Code.Audio;

namespace Code.Factories
{
    public interface IAudioFactory : IFactory
    {
        SoundPlayer CreateSoundPlayer();
    }
}