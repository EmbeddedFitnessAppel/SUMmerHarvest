using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utility.Pooling
{
    public class ObjectPool
    {
        private static ObjectPool instance;

        private readonly Dictionary<string, ObjectPoolContainer<MonoBehaviour>> containers;

        private ObjectPool()
        {
            containers = new Dictionary<string, ObjectPoolContainer<MonoBehaviour>>();
        }

        public static ObjectPool Instance
        {
            get { return instance ?? (instance = new ObjectPool()); }
        }

        /// <summary>
        ///     Adds a new pool for the object if it doesn't exist already.
        /// </summary>
        public bool Add<T>(Func<MonoBehaviour> creator) where T : MonoBehaviour
        {
            if (creator == null) return false;
            containers.Add(typeof(T).FullName, new ObjectPoolContainer<MonoBehaviour>(creator));
            return true;
        }

        private ObjectPoolContainer<MonoBehaviour> GetContainer<T>() where T : MonoBehaviour
        {
            if (containers.ContainsKey(typeof(T).FullName)) return containers[typeof(T).FullName];
            return default(ObjectPoolContainer<MonoBehaviour>);
        }

        /// <summary>
        ///     Gets a new object from the pool or instantiates a new object if none are available.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : MonoBehaviour
        {
            var container = GetContainer<T>();
            if (container == null) return default(T);

            return (T)container.Get();
        }

        public void Release<T>(T obj) where T : MonoBehaviour
        {
            var container = GetContainer<T>();
            if (container == null) return;

            container.Release(obj);
        }
    }
}