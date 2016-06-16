using System.Collections.Generic;
using Assets.Scripts.Game.UI.InWorld;
using UnityEngine;

namespace Assets.Scripts.Game.Managers
{
    public class AppleManager : MonoBehaviour {

        public int spawnRate;//every [spawnRate] frames an apple will be spawned
        private int appleTicker = 0;
        public BoxCollider spawnArea;
        public GameObject applePrefab;
        public GameObject appleUIPrefab;
        public Canvas InworldCanvas;
        public int maxAppleLoops;
        int appleLoops = 0;
        public GameObject gameWorld;//za warudo
        private List<Apple> apples = new List<Apple>();
        System.Random random;


        // Update is called once per frame
        void FixedUpdate()
        {
            if (appleTicker <= spawnRate)
            {
                appleTicker++;
            }
            else
            {
                appleTicker = 0;
                SpawnApple();
            }
        }
        void Start()
        {
            foreach (Apple a in apples)
            {
                GameObject.Destroy(a.gameObject);
            }
            apples = new List<Apple>();
            random = new System.Random();

        }

        public void SpawnApple()
        {
            GameObject a = Instantiate(applePrefab);
            a.transform.SetParent(gameWorld.transform, false);

            StartCoroutine(a.GetComponent<Apple>().Drop());

            GameObject aU = Instantiate(appleUIPrefab);
            aU.transform.SetParent(InworldCanvas.transform);
            aU.GetComponent<ScoreApple>().SetApple(a.GetComponent<Apple>());
            SetApplepos(a);
            appleLoops = 0;
        }

        void SetApplepos(GameObject appleOBJ)
        {
            appleOBJ.transform.position = RandomHelper.RandomVector3(spawnArea.bounds.min, spawnArea.bounds.max);     
            Apple aa = appleOBJ.GetComponent<Apple>();
            int n = random.Next(aa.MinRadius,aa.MaxRadius);
            Collider[] hitColliders = Physics.OverlapSphere(appleOBJ.transform.position,(float)n );
        
            int i = 0;
            while (i < hitColliders.Length) {
                int ap = appleOBJ.GetInstanceID();
                if (hitColliders[i].tag == "Apple") { int hc = hitColliders[i].GetInstanceID(); }
                // if (hitColliders[i].tag == "Apple") { print("my instance " + appleOBJ.GetInstanceID() + " col ID " + hitColliders[i].GetInstanceID()); }
                if (hitColliders[i].tag=="Apple"&&appleOBJ.GetInstanceID()!=hitColliders[i].gameObject.GetInstanceID())//we don't want to collide with other apples, but we obviously have to ignore ourselves. it's not as if I wasted 2 hours trying to figure out why this eouldn't work. ha ha ha
                {
                    //Debug.Log(appleOBJ.name + " collided with" + hitColliders[i].name);
                    appleLoops++;
                    print(appleLoops);
                    if(appleLoops<maxAppleLoops)
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
