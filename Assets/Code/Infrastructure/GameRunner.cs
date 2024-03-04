using UnityEngine;

namespace Code.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private Bootstrap _bootstrap;
        
        private void Awake()
        {
            Bootstrap bootstrap = FindObjectOfType<Bootstrap>();

            if (bootstrap != null)
                return;

            Instantiate(_bootstrap);
        }
    }
}