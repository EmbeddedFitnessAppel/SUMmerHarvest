using UnityEngine;
using UnityEngine.UI;

public class ScoreBasket : MonoBehaviour
{
    private GameObject playerToTrack;
    public Vector3 Offset;
    private Basket basket;
    private Text textScript;
    private RectTransform rect;


    // Update is called once per frame
    private void LateUpdate()
    {
        textScript.text = basket.GetNumber().ToString();
        rect.position = basket.transform.position + Offset;
    }

    public void SetOwner(GameObject o)
    {
        playerToTrack = o;
        basket = playerToTrack.GetComponent<Basket>();
        textScript = gameObject.GetComponent<Text>();
        rect = textScript.GetComponent<RectTransform>();
    }
}