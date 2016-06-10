using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBasket : MonoBehaviour {
    private GameObject playerToTrack;
    public float YAXisOffset;
    private Basket p;
    private Text t;
    private RectTransform rect;


    // Update is called once per frame
    void LateUpdate()
    {
        t.text = p.GetNumber().ToString();
        rect.position = new Vector3(p.transform.position.x, p.transform.position.y + YAXisOffset, transform.position.z);
    }

    public void SetOwner(GameObject o)
    {
        playerToTrack = o;
        p = playerToTrack.GetComponent<Basket>();
        t = gameObject.GetComponent<Text>();
        rect = t.GetComponent<RectTransform>();
    }
}
