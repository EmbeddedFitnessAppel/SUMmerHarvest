using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utility.Pooling
{
    public class ObjectPoolContainer<T> where T : MonoBehaviour
    {
        private List<T> data;
        private Func<T> creator;

        public ObjectPoolContainer(Func<T> creator)
        {
            data = new List<T>();
            this.creator = creator;
        }

        public T Get()
        {
            return data.FirstOrDefault();
        }

        public void Release(T obj)
        {
            data.Add(obj);
        }
    }
}