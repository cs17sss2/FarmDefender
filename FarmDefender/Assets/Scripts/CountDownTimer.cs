using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{

    public static float currentTime = 0;
    public static bool nextLevel = false;
    public static float startingTime;

    public Text countdownText;
    public GameObject youWinText;
    public GameObject nextLevelText;

    public GameObject youLostText;
    public GameObject restartText;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level1"))
        {
            startingTime = 60f;
            countdownText.text = "60";
            FindObjectOfType<AudioManager>().Play("level1Start");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level2"))
        {
            startingTime = 90f;
            countdownText.text = "90";
            FindObjectOfType<AudioManager>().Play("level2Start");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level3"))
        {
            startingTime = 120f;
            countdownText.text = "120";
            FindObjectOfType<AudioManager>().Play("level3Start");
        }

        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime < 10)
        {
            countdownText.color = Color.red;
        }
        else if (currentTime < startingTime / 2)
        {
            countdownText.color = Color.yellow;
        }
        else
        {
            countdownText.color = Color.black;
        }
        if ((int)currentTime == 0 && !nextLevel && BallSpawnerController1.spawnerHealth > 0 && SceneManager.GetActiveScene().name.Equals("Level3"))
        {
            Time.timeScale = 0;
            restartText.SetActive(true);
            youLostText.SetActive(true);
            BoundController.gameEnd = true;
            FindObjectOfType<AudioManager>().Play("loseSound");

            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

            for (var i = 0; i < balls.Length; i++)
            {
                Destroy(balls[i]);
            }
        }


        if (((int)currentTime == 0 && !nextLevel && !SceneManager.GetActiveScene().name.Equals("Level3"))
            || ((int)currentTime == 0 && !nextLevel && BallSpawnerController1.spawnerHealth <= 0 && SceneManager.GetActiveScene().name.Equals("Level3")))
        {
            
            if (SceneManager.GetActiveScene().name.Equals("Level1"))
            {
                FindObjectOfType<AudioManager>().Play("level1End");
            }
            else if (SceneManager.GetActiveScene().name.Equals("Level2"))
            {
                FindObjectOfType<AudioManager>().Play("level2End");
            }
            else if (SceneManager.GetActiveScene().name.Equals("Level3"))
            {
                FindObjectOfType<AudioManager>().Play("level3End");
            }

            Time.timeScale = 0;
            youWinText.SetActive(true); // false to hide, true to show
            nextLevelText.SetActive(true);

            nextLevel = true;

        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter)) && nextLevel)
        {
            if (SceneManager.GetActiveScene().name.Equals("Level1"))
            {
                SceneManager.LoadScene("Level2");
                Time.timeScale = 1;
                nextLevel = false;
            }
            else if (SceneManager.GetActiveScene().name.Equals("Level2"))
            {
                SceneManager.LoadScene("Level3");
                Time.timeScale = 1;
                nextLevel = false;
            }


        }

    }
}
