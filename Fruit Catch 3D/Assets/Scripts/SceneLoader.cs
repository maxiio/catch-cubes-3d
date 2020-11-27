using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Awake()
    {
        LoadSavedScene();
    }
    
    private void LoadSavedScene()
    {
        if (PlayerPrefs.HasKey("SAVED_LEVEL"))
        {
            int currentLevelIndex = PlayerPrefs.GetInt("SAVED_LEVEL");
            SceneManager.LoadScene(currentLevelIndex);
            Debug.Log(currentLevelIndex);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("level yok moruq");
        }
    }

}
