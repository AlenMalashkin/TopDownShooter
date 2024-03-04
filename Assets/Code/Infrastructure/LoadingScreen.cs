using UnityEngine;

namespace Code.Infrastructure
{
    public class LoadingScreen : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
            => gameObject.SetActive(true);

        public void Hide()
            => gameObject.SetActive(false);
    }
}