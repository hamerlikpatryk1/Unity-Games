using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public void PlayLvl1()
    {
        SceneManager.LoadScene("Level 1"); ;
    }
    public void PlayLvl2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
