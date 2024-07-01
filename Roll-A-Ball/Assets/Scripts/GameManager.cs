using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //create script singleton
    public static GameManager Instance { get; private set;}
    public int score = 0; //Player score
    public TextMeshProUGUI scoreText; //Holds score text
    public GameObject victoryText; // A Var to hold our victory text object
    public GameObject pickupParent; // A var to hold the pickup parent game object; this is used to count our pickups.
    public int totalPickups = 0; // A var to store the total # of pickups in the scene
    public GameObject Player;
    public List<GameObject> enemies;

    private void Awake()
    {
        if (Instance == null) //If there is no other copy of the script
        {
            Instance = this; //This = "This Instance / Self"
        }
        else
        {
            //Delete the extra copy
            Debug.LogWarning("There can only be one instance of [GameManger]");
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        score = 0;
        UpdateScoreText();

        victoryText.SetActive(false);

        AudioManager.Instance.PlaySound("theme music");
    }


    public void UpdateScore(int amount)
    {
        score += amount;
        UpdateScoreText();
        if (totalPickups <= 0) // If there are no pickups in the level
        {
            WinGame();
        }
    }

    public void UpdateScoreText()
    {
        totalPickups = pickupParent.transform.childCount - 1;
        scoreText.text = "Score: " + score.ToString();
    }

    public void WinGame()
    {
        victoryText.SetActive(true); // Enable our victory text
        DisableEnemies();

    }

    public void DisableEnemies()
    {
        foreach(GameObject enemy in enemies) // loop that iterates through enemy list
        {
            if (enemy != null) //Checks each index for an active enemy
            {
                enemy.SetActive(false);
            }
        }
    }

    public void GameOver() // A function that is called whenever the player loses
    {
        AudioManager.Instance.PlaySound("player death");
        Invoke("LoadCurrentLevel", 2f);
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
    }

    void Update()
    {
       if (Player == null)
        {
            GameOver();
        }
    }
}
