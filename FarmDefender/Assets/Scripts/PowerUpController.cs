using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public GameObject thisObject;
    int angle;
    int amount;
    // Start is called before the first frame update
    void Start()
    {
        angle = 0;
        amount = 6;

        

    }

    // Update is called once per frame
    void Update()
    {
        if (angle == 60)
        {
            amount = -6;
        }
        else if (angle == -60)
        {
            amount = 6;
        }
        thisObject.transform.eulerAngles = new Vector3(0, 0, angle += amount);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Destroys gameobject if its tag is Ball
        if (other.gameObject.CompareTag("Player"))
        {
            if (thisObject.name.Contains("ealth"))
            {
                BoundController.healthPower = true;
            }
            else if (thisObject.name.Contains("spe"))
            {
                PlayerController.speedPower = true;
            }
            else if (thisObject.name.Contains("uke"))
            {
                BoundController.nukePower = true;
            }

            FindObjectOfType<AudioManager>().Play("PowerupCollected");

            Destroy(this.gameObject);

        }
    }

}
