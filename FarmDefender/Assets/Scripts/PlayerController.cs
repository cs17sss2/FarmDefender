using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    //speed of player
    float speed = 8f;
    //bounds of player
    public float topBound = 4.5F;
    public float bottomBound = -4.5F;
    bool startofGame;
    Rigidbody2D rbody;
    Animator myAnim;
    public GameObject titleText;

    public static bool speedPower = false;

    // Use this for initialization
    void Start()
    {
        rbody = this.gameObject.GetComponent<Rigidbody2D>();
        if(SceneManager.GetActiveScene().name.Equals("Level1")) startofGame = true;
        myAnim = GetComponent<Animator>();

        if (SceneManager.GetActiveScene().name.Equals("Level1"))
        {
            speed = 9f;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        //Destroys gameobject if its tag is Ball
        if (other.gameObject.CompareTag("Ball"))
        {
            //play animation
            myAnim.SetTrigger("isHit");
            FindObjectOfType<AudioManager>().Play("PlayerMelee");
        }

    }
    void playerIntro()
    {

        if (transform.position.x > 4)
        {
            rbody.velocity = (new Vector2(-1, 0) * 5f);
            Debug.Log("start movement");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("level1Start");
            startofGame = false;
            rbody.velocity = (new Vector2(-1, 0) * 0f);
        }
    }

    void Update()
    {
        if (startofGame) {
            playerIntro();
            Debug.Log("startofGame");
        }

        //pauses or plays game when player hits p
        if (Input.GetKeyDown(KeyCode.P) && Time.timeScale == 0 && !BoundController.gameEnd && !CountDownTimer.nextLevel && CountDownTimer.currentTime < (CountDownTimer.startingTime - 3))
        {
            Time.timeScale = 1;
            titleText.SetActive(false); // false to hide, true to show
        }
        else if (Input.GetKeyDown(KeyCode.P) && Time.timeScale == 1 && !BoundController.gameEnd && !CountDownTimer.nextLevel && CountDownTimer.currentTime < (CountDownTimer.startingTime - 3))
        {
            Time.timeScale = 0;
            titleText.SetActive(true);
        }

        if (speedPower)
        {
            StartCoroutine(speedRoutine());
            speedPower = false;
        }

    }

    IEnumerator speedRoutine()
    {
        speed = 16f;
        yield return new WaitForSeconds(5);
        speed = 8f;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //get player input and set speed
        float movementSpeedY = speed * Input.GetAxis("Vertical")* Time.deltaTime;
        transform.Translate(0, movementSpeedY, 0);
        myAnim.SetFloat("speed", (Mathf.Abs(Input.GetAxis("Vertical"))));

        //set bounds of player
        if (transform.position.y > topBound)
        {
            transform.position = new Vector3(transform.position.x, topBound, 0);
        }
        else if (transform.position.y < bottomBound)
        {
            transform.position = new Vector3(transform.position.x, bottomBound, 0);
        }
    }

   
}
