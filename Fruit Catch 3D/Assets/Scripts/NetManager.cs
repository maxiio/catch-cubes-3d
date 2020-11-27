using System;
using System.Collections;
using System.Collections.Generic;
using Obi;
using UnityEngine;

public class NetManager : MonoBehaviour
{
    
    public static NetManager Instance;

    [SerializeField] private GameObject backNet;
    public float thicknessAmount;

    private void Awake()
    {
        SingletonPattern();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collected()
    {
        thicknessAmount = Mathf.Clamp(thicknessAmount + .07f, 0f, .5f);
        //thicknessAmount += .1f;
    }
    
    
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
}
