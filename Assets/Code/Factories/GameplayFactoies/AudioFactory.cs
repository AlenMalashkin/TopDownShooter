using Code.Audio;
using Code.Services.AssetProvider;
using Code.Services.ProgressService;
using Code.Services.SaveService;
using Object = UnityEngine.Object;

namespace Code.Factories.GameplayFactoies
{
    public class AudioFactory : IAudioFactory
    {
        private IAssetProvider _assetProvider;
        private ISaveLoadService _saveLoadService;
        private IProgressService _progressService;

        public AudioFactory(IAssetProvider assetProvider, ISaveLoadService saveLoadService,
            IProgressService progressService)
        {
            _assetProvider = assetProvider;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
        }

        public SoundPlayer CreateSoundPlayer()
        {
            SoundPlayer soundPlayer = Object.Instantiate(_assetProvider.LoadAsset<SoundPlayer>("Prefabs/SoundPlayer"));
            soundPlayer.Init(_progressService, _saveLoadService);
            return soundPlayer;
        }
    }
}