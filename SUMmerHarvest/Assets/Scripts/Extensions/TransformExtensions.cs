using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        ///     Destroys all child gameobjects of the given transform.
        /// </summary>
        /// <param name="transform">Transform to remove the childs from.</param>
        /// <returns>Same transform as given for chaining.</returns>
        public static Transform Clear(this Transform transform)
        {
            if (!transform) return transform;

            while (transform.childCount != 0)
            {
                Object.Destroy(transform.GetChild(0).gameObject);
            }

            return transform;
        }

        /// <summary>
        ///     Destroys all child <see cref="GameObject" />'s immediately. Should be used when in editor only.
        /// </summary>
        /// <param name="transform">Transform to remove the childs from.</param>
        /// <returns>Same transform as given for chaining.</returns>
        public static Transform ClearImmediate(this Transform transform)
        {
            if (!transform) return transform;

            while (transform.childCount != 0)
            {
                Object.DestroyImmediate(transform.GetChild(0).gameObject);
            }

            return transform;
        }
    }
}