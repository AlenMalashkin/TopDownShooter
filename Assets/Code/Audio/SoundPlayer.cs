using UnityEngine;

namespace Code.Audio
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public void Play(AudioClip audioClip)
            => _audioSource.PlayOneShot(audioClip);

        public void PlayLoop(AudioClip audioClip)
        {
            _audioSource.loop = true;
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        public void Pause()
            => _audioSource.Pause();

        public void Unpause()
            => _audioSource.UnPause();
    }
}