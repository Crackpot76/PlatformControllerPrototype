using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTestScript : MonoBehaviour {

    public float thrustX;
    public float thrustY;
    public Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        Vector2 v = new Vector2(thrustX, thrustY);
        rb.AddForce(v);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            Vector2 v = new Vector2(thrustX, thrustY);
            rb.AddForce(v);
        }
    }
}
