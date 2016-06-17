using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class DontDestroyOnLoad : Singleton<DontDestroyOnLoad>
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}