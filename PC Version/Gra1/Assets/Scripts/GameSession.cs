using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror;

public class GameSession : NetworkBehaviour 
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    [SerializeField] private Transform respawnPoint;
    //   [SerializeField] private Transform acidPoint;

    NetworkManager manager;

    private void Awake() //Singleton
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if (numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            //StopHosting();
            ResetGameSession();
        }
    }
    public void TakeLife()
    {
        playerLives--;
        //var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //if (SceneManager.sceneCountInBuildSettings == 2)
        //GameObject.FindGameObjectWithTag("Acid").transform.position = acidPoint.transform.position;

        GameObject.Find("Local").transform.position = respawnPoint.transform.position;
        //SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    public void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    void StopHosting()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            manager.StopHost();
        }
        else if (NetworkServer.active)
        {
            manager.StopServer();
        }
        else if (NetworkClient.isConnected)
        {
            manager.StopClient();
        }
    }

}
