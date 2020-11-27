using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    private Vector3 desiredPosition;
    private GameObject leftChibi;
    private GameObject rightChibi;

    public GameObject obiCloth;
    public ParticleSystem speedParticle;

    private ParticleSystem.MainModule mainParticle;

    private Touch touch;
    private Vector2 startPosition, currentPosition;

    private BoxCollider clothCollider;

    public bool isOpen = true;
    
    private void Awake()
    {
        leftChibi = GameObject.Find("LeftChibi");
        rightChibi = GameObject.Find("RightChibi");

        clothCollider = obiCloth.GetComponent<BoxCollider>();

        mainParticle = speedParticle.main;
    }

    private void FixedUpdate()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startPosition = touch.position;
                currentPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                // current - start
                // -, left
                // +, right

                // right
                if (touch.position.x - currentPosition.x >= 0)
                {
                    if (leftChibi.transform.position.z >= 1.59f) return;
                    leftChibi.transform.position = new Vector3(leftChibi.transform.position.x, leftChibi.transform.position.y, Mathf.Clamp(leftChibi.transform.position.z + (Math.Abs(touch.position.x - currentPosition.x) * (1.5f / (Screen.width / 2))), 0.5f, 1.59f));
                    rightChibi.transform.position = new Vector3(rightChibi.transform.position.x, rightChibi.transform.position.y, Mathf.Clamp(rightChibi.transform.position.z - (Math.Abs(touch.position.x - currentPosition.x) * (1.5f / (Screen.width / 2))), -1.66f, -0.56f));

                    mainParticle.startSpeed = Mathf.Clamp(mainParticle.startSpeed.constant + (Math.Abs(touch.position.x - currentPosition.x) * (20f / (Screen.width / 2))), 0f, 20f);

                    clothCollider.size = new Vector3(clothCollider.size.x, clothCollider.size.y, Mathf.Clamp(clothCollider.size.z + (Math.Abs(touch.position.x - currentPosition.x) * (2f / (Screen.width / 2))), 1,2));

                    if (leftChibi.transform.position.z >= 1.30f) isOpen = true;

                    if (clothCollider.size.z < 0.26) clothCollider.enabled = false;
                }

                // left
                if (touch.position.x - currentPosition.x < 0)
                {
                    if (leftChibi.transform.position.z <= 0.5f) return;
                    leftChibi.transform.position = new Vector3(leftChibi.transform.position.x, leftChibi.transform.position.y, Mathf.Clamp(leftChibi.transform.position.z - (Math.Abs(touch.position.x - currentPosition.x) * (1.5f / (Screen.width / 2))), .8f, 1.59f));
                    rightChibi.transform.position = new Vector3(rightChibi.transform.position.x, rightChibi.transform.position.y, Mathf.Clamp(rightChibi.transform.position.z + (Math.Abs(touch.position.x - currentPosition.x) * (1.5f / (Screen.width / 2))), -1.66f, -0.86f));

                    clothCollider.size = new Vector3(clothCollider.size.x, clothCollider.size.y, Mathf.Clamp(clothCollider.size.z - (Math.Abs(touch.position.x - currentPosition.x) * (2f / (Screen.width / 2))), 0, 2));

                    mainParticle.startSpeed = Mathf.Clamp(mainParticle.startSpeed.constant - (Math.Abs(touch.position.x - currentPosition.x) * (20f / (Screen.width / 2))), 0f, 20f);

                    if (leftChibi.transform.position.z < 1.30f) isOpen = false;

                    if (clothCollider.size.z > 0.25) clothCollider.enabled = true;
                }

                mainParticle.maxParticles = (int)mainParticle.startSpeed.constant * 5;

                currentPosition = touch.position;
            }

        }
    }
    
}


















//Vector3 leftChibiMin = new Vector3(leftChibi.transform.position.x, leftChibi.transform.position.y, 1.59f);
//Vector3 leftChibiMax = new Vector3(leftChibi.transform.position.x, leftChibi.transform.position.y, .7f);

//if (swipeControls.SwipeRight)
//{
//    Debug.Log("swipe right");
//    //desiredPosition += Vector3.back;
//    //leftChibi.transform.position -= new Vector3(leftChibi.transform.position.x,leftChibi.transform.position.y,1f);
//    leftChibi.transform.position = Vector3.Lerp(leftChibi.transform.position, leftChibiMax, 5 * Time.deltaTime);
//}

///*leftChibi.gameObject.transform.position = Vector3.MoveTowards(leftChibi.gameObject.transform.position,desiredPosition,3f*Time.deltaTime);
//rightChibi.gameObject.transform.position = Vector3.MoveTowards(rightChibi.gameObject.transform.position,-desiredPosition,3f*Time.deltaTime);*/

//if (swipeControls.SwipeLeft)
//{
//    Debug.Log("swipe left");
//}