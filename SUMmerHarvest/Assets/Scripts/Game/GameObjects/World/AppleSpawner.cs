﻿using UnityEngine;
using System.Collections.Generic;

public class AppleSpawner : MonoBehaviour {

    public int spawnRate;//every [spawnRate] frames an apple will be spawned
    private int appleTicker = 0;
    public BoxCollider spawnArea;
    public GameObject applePrefab;
    public GameObject appleUIPrefab;
    public Canvas InworldCanvas;
    public int maxAppleLoops;
    int appleLoops = 0;
    private int currAppleLoops;
    public GameObject gameWorld;//za warudo
    private List<Apple> apples = new List<Apple>();


    // Update is called once per frame
    void Update()
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
    }

    public void SpawnApple()
    {
        GameObject a = Instantiate(applePrefab);
        a.transform.SetParent(gameWorld.transform, false);
        
        GameObject aU = Instantiate(appleUIPrefab);
        aU.transform.SetParent(InworldCanvas.transform);
        aU.GetComponent<ScoreApple>().SetApple(a.GetComponent<Apple>());
        setApplepos(a);
        appleLoops = 0;
    }

    void setApplepos(GameObject appleOBJ)
    {
        appleOBJ.transform.position = RandomHelper.RandomVector3(spawnArea.bounds.min, spawnArea.bounds.max);
        System.Random random = new System.Random();//geen unity random :^)
        Apple aa = appleOBJ.GetComponent<Apple>();
        int n = random.Next(aa.minRadius,aa.maxRadius);
        Collider[] hitColliders = Physics.OverlapSphere(appleOBJ.transform.position,(float)n );
        
            int i = 0;
        while (i < hitColliders.Length) {
            if (hitColliders[i].tag=="Apple"&&!appleOBJ==hitColliders[i])//we don't want to collide with other apples, but we obviously have to ignore ourselves. it's not as if I wasted 2 hours trying to figure out why this eouldn't work. ha ha ha
            {
                Debug.Log(appleOBJ.name + " collided with" + hitColliders[i].name);
                appleLoops++;
                print(appleLoops);
                if(appleLoops<maxAppleLoops)
                {
                    setApplepos(appleOBJ);
                }
                else
                {
                    Debug.Log("Max appleloops exceeded for " + appleOBJ.name+" Deleting...");
                    appleOBJ.GetComponent<Apple>().Destroy();
                    break;
                }
            }
            i++;
        }
        Debug.Log("Apples hit: " + appleLoops);
    
           
    }
}
