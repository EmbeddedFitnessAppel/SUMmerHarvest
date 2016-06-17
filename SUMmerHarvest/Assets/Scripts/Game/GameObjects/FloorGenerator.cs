using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.Game.GameObjects
{
    public class FloorGenerator : MonoBehaviour
    {
        public GameObject FloorPrefab;
        public int SquaredSize;
        public int ScaleSize;

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void Reset()
        {
            SquaredSize = 10;
        }

        public void Generate()
        {
            if (!FloorPrefab) return;

            Clear();

            int actualScale = ScaleSize * 10;

            // Create new based on size.
            for (var i = 0; i < SquaredSize; i++)
            {
                for (var j = 0; j < SquaredSize; j++)
                {
                    var obj =
                        (GameObject)
                            Instantiate(FloorPrefab,
                                new Vector3(i * actualScale - SquaredSize * actualScale / 2f, transform.position.y,
                                    j * actualScale - SquaredSize * actualScale / 2f), Quaternion.identity);
                    obj.transform.localScale = new Vector3(ScaleSize, 1, ScaleSize);
                    obj.transform.SetParent(transform);
                }
            }
        }

        public void Clear()
        {
            // Clear old.
            transform.ClearImmediate();
        }
    }
}