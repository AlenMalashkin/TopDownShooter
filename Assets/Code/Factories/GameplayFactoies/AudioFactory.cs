using Code.Audio;
using Code.Services.AssetProvider;
using Object = UnityEngine.Object;

namespace Code.Factories.GameplayFactoies
{
    public class AudioFactory : IAudioFactory
    {
        private IAssetProvider _assetProvider;

        public AudioFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public SoundPlayer CreateSoundPlayer()
        {
            SoundPlayer soundPlayer = Object.Instantiate(_assetProvider.LoadAsset<SoundPlayer>("Prefabs/SoundPlayer"));
            return soundPlayer;
        }
    }
}