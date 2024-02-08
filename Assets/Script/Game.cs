using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject enemiesPrefeb;
    public int currentScore = 0;
    public int numberofEnemies = 5;


    public TMPro.TextMeshProUGUI uiScore;
    public TMPro.TextMeshProUGUI uiTime;
    public TMPro.TextMeshProUGUI uiStatus;

    public float timeLeft = 20f;

    // Flag to check if the game is over
    private bool isGameOver = false;

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        uiTime.text = "Time: " + Mathf.Round(timeLeft).ToString();
        uiScore.text = "Score: " + currentScore.ToString();

        currentScore = 0;
        isGameOver = false;
        uiStatus.enabled = false;

        // Start the countdown
        StartCoroutine(Countdown());

        DestroyPreviousEnemies();

        // Instantiate new coins
        for (int i = 0; i < numberofEnemies; i++)
        {
            GameObject newEnemies = Instantiate(enemiesPrefeb, new Vector3(Random.Range(-3f, 3f), Random.Range(3f, 5f), 0f), enemiesPrefeb.transform.rotation);
            // Add a specific tag to the new coin
            newEnemies.tag = "Enemies";
        }
    }

    void DestroyPreviousEnemies()
    {
        // Find all GameObjects with the tag "InstantiatedCoin" and destroy them
        GameObject[] previousEnemies = GameObject.FindGameObjectsWithTag("Enemies");
        foreach (var Enemies in previousEnemies)
        {
            Destroy(Enemies);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is over, and if so, prevent further updates
        if (isGameOver)
        {
            // Check for spacebar input to restart the game
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Resume the game
                Time.timeScale = 1f;

                // Restart the game
                StartGame();
            }
            return;
        }
    }

    IEnumerator Countdown()
    {
        timeLeft = 20f;

        while (timeLeft > 0)
        {
            uiTime.text = "Time: " + Mathf.Round(timeLeft).ToString();
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }

        // Game over logic here
        uiStatus.text = "Game Over \nScore: " + Mathf.Round(currentScore);
        uiStatus.enabled = true;

        // Set the game over flag to true
        isGameOver = true;

        // Pause the game
        Time.timeScale = 0f;
    }

}
