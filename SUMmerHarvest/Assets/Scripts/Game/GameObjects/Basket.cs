using UnityEngine;
using System.Collections;

public class Basket : MonoBehaviour {
    public KeyCode kL;
    public KeyCode kR;
    public float speed;
    private Vector3 c;

    private int score;

    void Start()
    {
        c = transform.position;
        score = 160;
    }

    void Update()
    {
        if (Input.GetKey(kL))
        {
            c.x = c.x - speed;
        }
        else if (Input.GetKey(kR))
        {
            c.x = c.x + speed;
        }

        transform.position = c;
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "pickup")
        {
            Apple a = col.gameObject.GetComponent<Apple>();
            a.Pickup(this);
        }
    }

    public void RemoveScore(int am)
    {
        score = score - am;
    }

    public int GetNumber()
    {
        return score;
    }
}
