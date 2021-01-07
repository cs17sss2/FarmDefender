using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallController : MonoBehaviour
{
    //speed of the ball
    public float speed = 3.5F;

    public GameObject impactEffect;

    //the initial direction of the ball
    private Vector2 spawnDir;
    //ball's components
    Rigidbody2D rig2D;
    Animator myAnim;

    // Use this for initialization
    void Start()
    {
        myAnim = GetComponent<Animator>();
        //setting balls Rigidbody 2D
        rig2D = this.gameObject.GetComponent<Rigidbody2D>();
        //generating random number based on possible initial directions
        int rand = Random.Range(1, 2);
        //setting initial direction
        if (this.gameObject.name.Contains("3")) {
            spawnDir = new Vector2(1, 0);
        }
        else if (rand == 1)
        {
            spawnDir = new Vector2(1, 1);
        }
        else if (rand == 2)
        {
            spawnDir = new Vector2(1, -1);
        }
        //moving ball in initial direction and adding speed
        rig2D.velocity = (spawnDir * speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Destroys gameobject if its tag is Ball
        if (other.gameObject.CompareTag("Player") || other.name.Equals("RightBound") && !other.gameObject.CompareTag("Bullet") && !other.gameObject.CompareTag("Spawner") && !other.gameObject.CompareTag("Powerup"))
        {
            Destroy(gameObject);
            Instantiate(impactEffect, transform.position, transform.rotation);

            if (other.name.Equals("RightBound"))
            {
                FindObjectOfType<AudioManager>().Play("eatCrop");
            }

        }
    }


}

