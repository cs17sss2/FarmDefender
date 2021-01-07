using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotController : MonoBehaviour
{
    float speed;
    private Vector2 shootDir;
    Rigidbody2D rig2D;


    // Start is called before the first frame update
    void Start()
    {
        // assign movement values values
        speed = 20f;
        rig2D = this.gameObject.GetComponent<Rigidbody2D>();
        shootDir = new Vector2(-1, 0);


        // move bullet forward
        transform.Rotate(0, 0, 90);
        rig2D.velocity = (shootDir * speed);

        // play bullet sound
        FindObjectOfType<AudioManager>().Play("BulletShot");

        StartCoroutine(deleteBullet());

    }

    IEnumerator deleteBullet()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);

    }

    private void Update()
    {
        
    }

}
