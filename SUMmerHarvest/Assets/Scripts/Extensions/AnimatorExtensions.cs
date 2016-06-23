using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class AnimatorExtensions
    {
        public static IEnumerator WaitForCurrentAnimation(this Animator animator)
        {
            
            yield return null;
        }
    }
}