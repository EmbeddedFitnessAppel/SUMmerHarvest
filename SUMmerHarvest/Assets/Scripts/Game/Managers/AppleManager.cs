using System;
using System.Collections.Generic;
using Assets.Scripts.Game.GameObjects;
using Assets.Scripts.Game.UI.InWorld;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

namespace Assets.Scripts.Game.Managers
{
    public class AppleManager : Singleton<MonoBehaviour>
    {
        [Tooltip("Amount of seconds to wait between spawning apples.")]
        public float SpawnDelay;

        private float waitedForSpawnTime;
        public BoxCollider spawnArea;
        public GameObject applePrefab;
        public GameObject appleUIPrefab;
        public Canvas InworldCanvas;
        public int maxAppleLoops;
        private int appleLoops;
        public GameObject gameWorld; //za warudo
        private List<Apple> apples = new List<Apple>();
        private Random random;

        public override void Awake()
        {
            base.Awake();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (SceneManager.GetActiveScene().name.IndexOf("Game", StringComparison.OrdinalIgnoreCase) < 0) return;

            waitedForSpawnTime += Time.fixedDeltaTime;
            if (waitedForSpawnTime >= SpawnDelay)
            {
                SpawnApple();
                waitedForSpawnTime = 0;
            }
        }

        private void Start()
        {
            foreach (var a in apples)
            {
                Destroy(a.gameObject);
            }
            apples = new List<Apple>();
            random = new Random();
        }

        public void SpawnApple()
        {
            var a = Instantiate(applePrefab);
            a.transform.SetParent(gameWorld.transform, false);

            StartCoroutine(a.GetComponent<Apple>().Drop());

            var aU = Instantiate(appleUIPrefab);
            aU.transform.SetParent(InworldCanvas.transform);
            aU.GetComponent<ScoreApple>().SetApple(a.GetComponent<Apple>());
            SetApplepos(a);
            appleLoops = 0;
        }

        private void SetApplepos(GameObject appleOBJ)
        {
            appleOBJ.transform.position = RandomHelper.RandomVector3(spawnArea.bounds.min, spawnArea.bounds.max);
            var aa = appleOBJ.GetComponent<Apple>();
            var n = random.Next(aa.MinRadius, aa.MaxRadius);
            var hitColliders = Physics.OverlapSphere(appleOBJ.transform.position, n);

            var i = 0;
            while (i < hitColliders.Length)
            {
                var ap = appleOBJ.GetInstanceID();
                if (hitColliders[i].tag == "Apple")
                {
                    var hc = hitColliders[i].GetInstanceID();
                }
                // if (hitColliders[i].tag == "Apple") { print("my instance " + appleOBJ.GetInstanceID() + " col ID " + hitColliders[i].GetInstanceID()); }
                if (hitColliders[i].tag == "Apple" &&
                    appleOBJ.GetInstanceID() != hitColliders[i].gameObject.GetInstanceID())
                    //we don't want to collide with other apples, but we obviously have to ignore ourselves. it's not as if I wasted 2 hours trying to figure out why this eouldn't work. ha ha ha
                {
                    //Debug.Log(appleOBJ.name + " collided with" + hitColliders[i].name);
                    appleLoops++;
                    if (appleLoops < maxAppleLoops)
                    {
                        SetApplepos(appleOBJ);
                    }
                    else
                    {
                        //Debug.Log("Max appleloops exceeded for " + appleOBJ.name+" Deleting...");
                        appleOBJ.GetComponent<Apple>().Destroy();
                        break;
                    }
                }
                i++;
            }
            //Debug.Log("Apples hit: " + appleLoops);
        }
    }
}