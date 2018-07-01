using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public Vector3 velocity;
    public float lifeTime;
    // Use this for initialization

    bool started = false;

    void TrueStart(float dirX)
    {
        float randX = Random.value * 2 - 0.5f;
        float randY = Random.value * 2 - 1;

        velocity.x += randX;
        velocity *= dirX;
        velocity.y += randY;
        Destroy(gameObject,lifeTime);

        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(started)
         transform.Translate(velocity * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("MEEEEEP");
    }
    
}

