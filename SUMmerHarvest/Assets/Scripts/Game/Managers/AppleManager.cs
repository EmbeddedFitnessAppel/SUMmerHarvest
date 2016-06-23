using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game.GameObjects;
using Assets.Scripts.Game.UI.InWorld;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Utility.Random;
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

        private Dictionary<int, float> numberPossibility = new Dictionary<int, float>();
        private Dictionary<int, float> numberPossibilityPercentage = new Dictionary<int, float>();
        public AnimationCurve appleValueRatio;

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

            PreparePossibilityPercentageAppleRatio(applePrefab);
        }

        public void SpawnApple()
        {
            var a = Instantiate(applePrefab);
            a.transform.SetParent(gameWorld.transform, false);
            
            var aU = Instantiate(appleUIPrefab);
            aU.transform.SetParent(InworldCanvas.transform);
            aU.GetComponent<ScoreApple>().SetApple(a.GetComponent<Apple>());
            CalculateAppleValue(a);
            SetApplepos(a);
            appleLoops = 0;
        }

        //TODO CHANGE SHIT TO APPLE !!
        /// <summary>
        /// This method will fill the possibality percentage for each value the apple can get.
        /// This ration is calculate form an AnimationCurve.
        /// </summary>
        /// <param name="a">Example Apple for min- and maxValues</param>
        private void PreparePossibilityPercentageAppleRatio(GameObject appleOBJ)
        {
            appleOBJ.transform.position = RandomHelper.RandomVector3(spawnArea.bounds.min, spawnArea.bounds.max);
            var apple = appleOBJ.GetComponent<Apple>();

            //MATH
            int totalPossibleValues = apple.MaxValue - apple.MinValue;
            float xSpace = 1f / totalPossibleValues;
            //Debug.Log("XPACE: " + xSpace + "1 / 15: " + (1 / 15));

            float tempXPosition = xSpace;
            int tempAppleValue = apple.MinValue;
            for (int i = 0; i <= totalPossibleValues; i++)
            {
                //Debug.Log("Test waardes:  X:" + tempXPosition + " Y:" + appleValueRatio.Evaluate(tempXPosition));
                numberPossibility.Add(tempAppleValue, appleValueRatio.Evaluate(tempXPosition));
                tempAppleValue++;
                tempXPosition += xSpace;
            }

            string output = "";
            float totalYValues = 0f;
            foreach (KeyValuePair<int, float> entry in numberPossibility)
            {
                output += entry.Key + " - " + entry.Value + "     ";
                totalYValues += entry.Value;
            }
            //Debug.Log(output);
            //Debug.Log("Totaal Y: " + totalYValues);

            float valuePerOnePercent = totalYValues / 100f;
            float totalPercentage = 0f;
            foreach (KeyValuePair<int, float> entry in numberPossibility)
            {
                numberPossibilityPercentage.Add(entry.Key, entry.Value / valuePerOnePercent);
                totalPercentage += (entry.Value / valuePerOnePercent);
                //Debug.Log("Procentje: " + (entry.Value / valuePerOnePercent));
            }
            //Debug.Log("Total percentage: " + totalPercentage);
            //END MATH
        }

        /// <summary>
        /// Gives the spawned apple an random value.
        /// This value is based on the possibilityPercentages for each value.
        /// </summary>
        /// <param name="appleOBJ">The apple that needs to get a value</param>
        private void CalculateAppleValue(GameObject appleOBJ)
        {
            appleOBJ.transform.position = RandomHelper.RandomVector3(spawnArea.bounds.min, spawnArea.bounds.max);
            var apple = appleOBJ.GetComponent<Apple>();
            
            List<ProportionValue<int>> list = new List<ProportionValue<int>>();
            foreach (KeyValuePair<int, float> entry in numberPossibilityPercentage)
            {
                list.Add(ProportionValue.Create((entry.Value / 100), entry.Key));
            }
     
            apple.SetScore(list.ChooseByRandom());
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
                        appleOBJ.GetComponent<Apple>().DestroyApple();
                        break;
                    }
                }
                i++;
            }
            //Debug.Log("Apples hit: " + appleLoops);
        }
    }
}