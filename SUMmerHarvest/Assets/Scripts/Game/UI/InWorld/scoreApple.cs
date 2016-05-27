using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scoreApple : MonoBehaviour
{
    public float YAXisOffset;
    private Apple a;
    private Text t;
    private RectTransform rect;
    void Start()
    {
        t = gameObject.GetComponent<Text>();
        rect = t.GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (a != null)
        {
            t.text = a.GetNumber().ToString();
            rect.position = new Vector3(a.transform.position.x, a.transform.position.y + YAXisOffset, transform.position.z);
        }
    }
    public void SetApple(Apple ap)
    {
        a = ap;
        a.SetAppleUI(this);
    }
}
