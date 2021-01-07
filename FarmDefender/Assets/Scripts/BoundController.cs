using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BoundController : MonoBehaviour
{
    GameObject[] enemies;
    int livesLost;
    public GameObject endingText;
    public GameObject restartText;
    public static bool gameEnd;

    public static bool healthPower = false;
    public static bool nukePower = false;

    public GameObject impactEffect;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Wheat");
        livesLost = 0;
        gameEnd = false;
    }

    private void Update()
    {
        // if the user chooses to replay the game
        if (Input.GetKeyDown(KeyCode.R) && gameEnd)
        {
            Time.timeScale = 1;
            Destroy(GameObject.FindGameObjectWithTag("Ball"));
            restartText.SetActive(false); // false to hide, true to show
            endingText.SetActive(false);

            // restart values
            CountDownTimer.currentTime = CountDownTimer.startingTime;
            livesLost = 0;
            foreach (GameObject i in enemies)
            {
                i.SetActive(true);
            }

            gameEnd = false;
            BallSpawnerController1.spawnerHealth = 20;

            if (SceneManager.GetActiveScene().name.Equals("Level1"))
            {
                FindObjectOfType<AudioManager>().Play("level1Start");
            }
            else if (SceneManager.GetActiveScene().name.Equals("Level2"))
            {
                FindObjectOfType<AudioManager>().Play("level2Start");
            }
            else if (SceneManager.GetActiveScene().name.Equals("Level3"))
            {
                FindObjectOfType<AudioManager>().Play("level3Start");
            }


        }

        if (nukePower)
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

            for (var i = 0; i < balls.Length; i++)
            {
                Destroy(balls[i]);
                Instantiate(impactEffect, balls[i].transform.position, balls[i].transform.rotation);
            }
            nukePower = false;
        }
        if (healthPower)
        {
            if (livesLost > 0)
            {
                enemies[livesLost - 1].SetActive(true);
                livesLost--;
            }
            healthPower = false;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Destroys gameobject if its tag is Ball
        if (other.gameObject.CompareTag("Ball"))
        {
            if (livesLost == enemies.Length - 1)
            {
                Destroy(GameObject.FindGameObjectWithTag("Ball"));
                Time.timeScale = 0;
                restartText.SetActive(true); // false to hide, true to show
                endingText.SetActive(true);
                gameEnd = true;
                FindObjectOfType<AudioManager>().Play("loseSound");

                GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

                for (var i = 0; i < balls.Length; i++)
                {
                    Destroy(balls[i]);
                }

            }
            else
            {
                StartCoroutine(Death());
            }
        }
    }
    IEnumerator Death()
    {
        enemies[livesLost].gameObject.GetComponent<Animator>().SetTrigger("isHit");
        yield return new WaitForSeconds(0.3f);
        enemies[livesLost].SetActive(false);
        livesLost++;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
    }

}