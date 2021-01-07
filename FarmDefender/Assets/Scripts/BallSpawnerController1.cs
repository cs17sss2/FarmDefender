using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallSpawnerController1 : MonoBehaviour
{

    public GameObject bird1;
    public GameObject bird2;
    public GameObject bird3;

    public GameObject youWinText;
    public Text livesText;

    public float spawnTime = 0f;
    public bool stopSpawing = false;

    //Speed of the enemy
    public float speed = 5f;
    //bounds of spawner
    public float topBound = 4.5F;
    public float bottomBound = -4.5F;

    public GameObject health;
    public GameObject fast;
    public GameObject nuke;

    public static int spawnerHealth;

    bool goUp = true;

    int spawnCount = 1;
    Scene currentScene;

    // Use this for initialization
    void Start()
    { 

        if (SceneManager.GetActiveScene().name.Equals("Level1"))
        {
            InvokeRepeating("SpawnBirds1", 5, 2f);
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level2"))
        {
            InvokeRepeating("SpawnBirds2", 1, 1.8f);
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level3"))
        {
            InvokeRepeating("SpawnBirds3", 1, 1.6f);
            InvokeRepeating("SpawnPowerups", 10f, 10f);
            livesText.text = "Spawner Health: 2000";
            spawnerHealth = 20;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Destroys gameobject if its tag is Ball
        if (other.gameObject.CompareTag("Bullet") && !other.gameObject.CompareTag("Ball"))
        {
            StartCoroutine(HitEffect());

            Destroy(other.gameObject);
            spawnerHealth--;
            livesText.text = ("Spawner Health: " + spawnerHealth + "00");

            FindObjectOfType<AudioManager>().Play("spawnerImpact");


            if (BallSpawnerController1.spawnerHealth == 0)
            {
                Time.timeScale = 0;
                youWinText.SetActive(true);

                FindObjectOfType<AudioManager>().Play("level3End");

            }
        }
    }

    public void SpawnBirds1()
    {
        int bird = (int)Random.Range(0f, 2f);
        this.transform.position = new Vector3(-10, Random.Range(-4.5f, 4.5f), 0);

        if (bird == 1 && (int)(CountDownTimer.currentTime) < (int)(CountDownTimer.startingTime) - 15)
        {
            Instantiate(bird1, this.transform.position, this.transform.rotation);
        }
        else
        {
            Instantiate(bird2, this.transform.position, this.transform.rotation);
        }


        if (stopSpawing)
        {
            CancelInvoke("SpawnObject");
        }

        spawnCount++;
    }

    public void SpawnBirds2()
    {

        int bird = (int)Random.Range(0f, 3f);
        this.transform.position = new Vector3(-10, Random.Range(-4.5f, 4.5f), 0);

        if (bird == 1 && (int)(CountDownTimer.currentTime) < (int)(CountDownTimer.startingTime) - 10)
        {
            Instantiate(bird1, this.transform.position, this.transform.rotation);
        }
        else if (bird == 2 && (int)(CountDownTimer.currentTime) < (int)(CountDownTimer.startingTime) - 20)
        {
            Instantiate(bird3, this.transform.position, this.transform.rotation);
        }
        else
        {
            Instantiate(bird2, this.transform.position, this.transform.rotation);
        }

        if (stopSpawing)
        {
            CancelInvoke("SpawnObject");
        }

        spawnCount++;
    }

    public void SpawnBirds3()
    {

        int bird = (int)Random.Range(0f, 4f);

        if (bird == 1)
        {
            Instantiate(bird1, this.transform.position, this.transform.rotation);
        }
        else if (bird == 2 && (int)(CountDownTimer.currentTime) < (int)(CountDownTimer.startingTime) - 5)
        {
            Instantiate(bird2, this.transform.position, this.transform.rotation);
        }
        else if (bird == 3 && (int)(CountDownTimer.currentTime) < (int)(CountDownTimer.startingTime) - 10)
        {
            Instantiate(bird3, this.transform.position, this.transform.rotation);
        }
        else
        {
            Instantiate(bird2, this.transform.position, this.transform.rotation);
        }

        if (stopSpawing)
        {
            CancelInvoke("SpawnObject");
        }

        spawnCount++;
    }

    private void FixedUpdate()
    {

        if (goUp) {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        transform.Translate(Vector3.forward * 1 * Time.deltaTime);


        //set bounds of enemy
        if (transform.position.y > topBound)
        {
            goUp = false;
        }
        else if (transform.position.y < bottomBound)
        {
            goUp = true;
        }

    }

    public void SpawnPowerups()
    {
        int random = (int)Random.Range(0f, 3f);
        Vector3 pos = new Vector3(4, Random.Range(-4.5f, 4.5f), 0);
        if (random == 2) {
            Instantiate(nuke, pos, this.transform.rotation);
        }
        else if (random == 1) {
            Instantiate(health, pos, this.transform.rotation);
        }
        else {
            Instantiate(fast, pos, this.transform.rotation);
        }
    }
    IEnumerator HitEffect()
    {
        Color originalColour = gameObject.GetComponent<Renderer>().material.color;
        gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f); // red
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Renderer>().material.color = originalColour;
    }

}
