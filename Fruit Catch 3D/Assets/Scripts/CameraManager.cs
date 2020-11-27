using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    public static CameraManager Instance;

    [SerializeField] private GameObject playerFarCamera;
    [SerializeField] private GameObject playerFollowCamera;
    [SerializeField] private GameObject playerDeadCamera;
    [SerializeField] private GameObject trackWithCart;

    private void Awake()
    {
        SingletonPattern(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerFollowCamera.activeSelf)
        {
            playerFollowCamera.SetActive(false);
        }
        playerFarCamera.SetActive(true);
        
    }

    public void GameStarted()
    {
        playerFarCamera.SetActive(false);
        playerFollowCamera.SetActive(true);
    }
    
    
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


    public void DeadCamera()
    {
            trackWithCart.SetActive(true);
            playerDeadCamera.SetActive(true);
    }
}
