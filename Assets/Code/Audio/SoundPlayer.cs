using Code.Data.Progress;
using Code.Services.ProgressService;
using Code.Services.SaveService;
using UnityEngine;

namespace Code.Audio
{
    public class SoundPlayer : MonoBehaviour, IProgressReader
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _effectSource;

        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        
        public void Init(IProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _saveLoadService.ProgressReaders.Add(this);
            _musicSource.volume = _progressService.Progress.Settings.MusicVolume;
            _effectSource.volume = _progressService.Progress.Settings.EffectVolume;            
        }

        private void OnDisable()
        {
            _saveLoadService.ProgressReaders.Remove(this);
        }

        public void PlaySoundEffect(AudioClip audioClip)
        {
            _musicSource.PlayOneShot(audioClip);
        }

        public void PlayMusic(AudioClip audioClip)
        {
            _musicSource.loop = true;
            _musicSource.clip = audioClip;
            _musicSource.Play();
        }

        public void Pause()
            => _musicSource.Pause();

        public void Unpause()
            => _musicSource.UnPause();

        public void ReadProgress(Progress progress)
        {
            _musicSource.volume = _progressService.Progress.Settings.MusicVolume;
            _effectSource.volume = _progressService.Progress.Settings.EffectVolume; 
        }
    }
}