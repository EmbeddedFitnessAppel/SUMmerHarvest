using UnityEngine;
using System.Collections.Generic;

public class AppleSpawner : MonoBehaviour {

    public int AppleDelay;
    private int appleTicker = 0;
    public BoxCollider spawnArea;
    public GameObject applePrefab;
    public GameObject appleUIPrefab;
    public Canvas InworldCanvas;
    public GameObject gameWorld;//za warudo
    private List<Apple> apples = new List<Apple>();


    // Update is called once per frame
    void Update()
    {
        if (appleTicker <= AppleDelay)
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
        a.transform.position = RandomHelper.RandomVector3(spawnArea.bounds.min, spawnArea.bounds.max);
        GameObject aU = Instantiate(appleUIPrefab);
        aU.transform.SetParent(InworldCanvas.transform);
        aU.GetComponent<scoreApple>().SetApple(a.GetComponent<Apple>());
    }
}
