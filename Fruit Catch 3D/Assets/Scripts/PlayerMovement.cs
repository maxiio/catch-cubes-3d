using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Vector3 vel;
    private Rigidbody rb;
    private Rigidbody rbLeft;
    private Rigidbody rbRight;

    public int collectableCount;

    [SerializeField] private Animator LeftChibiAnimator;
    [SerializeField] private Animator RightChibiAnimator;

    private SwipeManager Swipe;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rbLeft = LeftChibiAnimator.gameObject.GetComponent<Rigidbody>();
        rbRight = RightChibiAnimator.gameObject.GetComponent<Rigidbody>();

        Swipe = GetComponent<SwipeManager>();
    }
    
    void FixedUpdate()
    {
        vel = new Vector3((Swipe.isOpen ? 1.5f : 3) + speed, 0, 0);
        vel.x = Mathf.Clamp(vel.x, 0, 3 + (collectableCount * .5f));
        rb.velocity = vel;
        LeftChibiAnimator.SetFloat("Speed", Mathf.Clamp(((Swipe.isOpen ? 1.5f : 3) + speed) * 1.8f / 8, 1f, 1.8f));
        RightChibiAnimator.SetFloat("Speed", Mathf.Clamp(((Swipe.isOpen ? 1.5f : 3) + speed) * 1.8f / 8, 1f, 1.8f));

    }

    public void SpeedUp()
    {
        speed += 0.5f;
    }
    
    
    
    
    
}
