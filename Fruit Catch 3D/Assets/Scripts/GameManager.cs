using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameObject player;
    private void Awake()
    {
        player = GameObject.Find("Player");
        SingletonPattern();
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        DeactivateScripts();
    }

    public void DeactivateScripts()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<SwipeManager>().enabled = false;
    }

    // Update is called once per frame
   

    #region Singleton

    private void SingletonPattern()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    

    #endregion
    
    
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        SaveGame();
    }

    public void GameStarted()
    {
        ActivateScripts();
        AnimationsManager.Instance.PlayerStartAnim();
        UIManager.Instance.GameStarted();
        ParticleManager.Instance.RunningParticle();
        CameraManager.Instance.GameStarted();
    }

    public void NextLevel()
    {
        UIManager.Instance.NextLevel();
        DeactivateScripts();
        player.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    public void RestartLevel()
    {
        
    }

    private void ActivateScripts()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<SwipeManager>().enabled = true;
    }


    private void SaveGame()
    {
        SavedDatas savedDatas = new SavedDatas();
        savedDatas.SaveLevel(LevelManager.Instance.GetCurrentLevel());
        PlayerPrefs.Save();
    }
}
