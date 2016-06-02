using UnityEngine;
using System.Collections;

public class Monkey : Player {
    private readonly float speed = 2.5f;
    [SerializeField]
    private Vector3 centerOfTree;

    private bool moveToCenter = false;

    void FixedUpdate() {
        Vector3 pos = this.transform.localPosition;

        if (Input.GetKey(KeyCode.A))
            pos.x -= speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
            pos.y += speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            pos.y -= speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            pos.x += speed * Time.deltaTime;


        if (moveToCenter) {
            float step = speed * Time.deltaTime * (Vector3.Distance(pos, centerOfTree) / 2);
            pos = Vector3.MoveTowards(pos, centerOfTree, step);
        }

        this.transform.localPosition = pos;
    }

    void OnTriggerEnter(Collider other) {
        moveToCenter = false;
    }

    void OnTriggerExit(Collider other) {
        moveToCenter = true;
    }
}
