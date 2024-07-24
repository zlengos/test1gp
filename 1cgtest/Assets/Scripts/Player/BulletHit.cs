using System.Collections;
using UnityEngine;

namespace Player
{
    public class BulletHit : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void Start()
        {
            StartCoroutine(WaitForAnimationEnd());
        }
        
        private IEnumerator WaitForAnimationEnd()
        {
            AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
            float animationLength = animationState.length;
            
            yield return new WaitForSeconds(animationLength);

            gameObject.SetActive(false);
        }
    }
}