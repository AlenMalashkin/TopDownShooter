using UnityEngine;

namespace Code.GameplayLogic
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        public void PlayAnimationByName(string animationName)
            => _animator.Play(animationName);
        
        public void PlayAnimationByName(string animationName, int layerIndex)
            => _animator.Play(animationName, layerIndex);

        public int GetLayerIndex(string layerName)
            => _animator.GetLayerIndex(layerName);

        public void SetLayerWeight(int layerIndex, float weight)
            => _animator.SetLayerWeight(layerIndex, weight);

        public void SetFloat(string floatName, float value)
            => _animator.SetFloat(floatName, value);

        public void SetBool(string boolName, bool value)
            => _animator.SetBool(boolName, value);
    }
}