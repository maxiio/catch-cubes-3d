using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelBarManager : MonoBehaviour
{

    private int currentLevel;
    [SerializeField] private Slider levelBar;
    [SerializeField] private Text currentLevelText;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject finishLine;
    private Transform playerTransform;
    private Transform endLineTransform;
    private float maxDistance;
    

    private void Awake()
    {
        playerTransform = player.transform;
        endLineTransform = finishLine.transform;
        maxDistance = GetDistance();
    }

    private void Start()
    {
        currentLevel = LevelManager.Instance.GetCurrentLevel();
        SetCurrentLevelText();
    }

    private void SetCurrentLevelText()
    {
        currentLevelText.text = currentLevel.ToString();
    }

    private void Update()
    {
        if (playerTransform.position.x <= maxDistance && playerTransform.position.x <= endLineTransform.position.x)
        {
            float distance = 1 - (GetDistance() / maxDistance);
            levelBar.value = distance;    
        }
    }
    
    private float GetDistance()
    {
        return endLineTransform.position.x - playerTransform.position.x;
    }
    
}
