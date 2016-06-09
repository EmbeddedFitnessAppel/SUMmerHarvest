using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utility.Pooling
{
    public class ObjectPoolContainer<T> where T : MonoBehaviour
    {
        private readonly HashSet<T> pool;
        private readonly Func<T> creator;
        private readonly Action<T> resetter;
        private readonly Action<T> releaser;

        public ObjectPoolContainer(Func<T> creator, Action<T> reset, Action<T> releaser)
        {
            if (creator == null) throw new ArgumentNullException("creator");
            pool = new HashSet<T>();
            this.creator = creator;
            this.resetter = reset;
            this.releaser = releaser;
        }

        public T Get()
        {
            var obj = pool.FirstOrDefault();

            // Remove from available objects.
            if (obj != null) pool.Remove(obj);

            // Create new object if none are available.
            if (obj == null)
            {
                Debug.Log("Pool is empty!");
                obj = creator();
                pool.Add(obj);
            }

            if (resetter != null) resetter(obj);

            obj.gameObject.SetActive(true);
            return obj;
        }

        public void Release(T obj)
        {
            if (obj == null) return;
            obj.gameObject.SetActive(false);

            if (releaser != null) releaser(obj);

            pool.Add(obj);
        }
    }
}