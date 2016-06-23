using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class AnimationExtensions
    {
        public static IEnumerator WaitForAnimation(this Animation animation)
        {
            while (animation.isPlaying)
            {
                yield return null;
            }
        }
    }
}