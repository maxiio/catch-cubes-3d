using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
public class Chibis : MonoBehaviour
{
    private float distToGround;
    private bool isFalling = false;

    private GameObject player;
    private ObiParticleAttachment[] netAttechment;
    private ObiParticleAttachment[] leftObiClothAttach;
    private ObiParticleAttachment[] rightObiClothAttach;
    private GameObject otherChibi;
    
    private void Start()
    {
        distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
        player = gameObject.transform.parent.gameObject;
        netAttechment = GameObject.Find("BackObiCloth").GetComponents<ObiParticleAttachment>();
        leftObiClothAttach = GameObject.Find("LeftObiCloth").GetComponents<ObiParticleAttachment>();
        rightObiClothAttach = GameObject.Find("RightObiCloth").GetComponents<ObiParticleAttachment>();
        

        if (gameObject.name.Equals("LeftChibi"))
        {
            otherChibi = GameObject.Find("RightChibi");
        }
        
        else if (gameObject.name.Equals("RightChibi"))
        {
            otherChibi = GameObject.Find("LeftChibi");
        }
    }

    private void Update()
    {

        if (!IsGrounded() && isFalling == false)
        {
            player.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            gameObject.AddComponent<Rigidbody>();
            GameManager.Instance.DeactivateScripts();
            UIManager.Instance.GameOver();
            CameraManager.Instance.DeadCamera();
            isFalling = true;

            if (gameObject.name.Equals("LeftChibi"))
            {
                netAttechment[0].enabled = false;

                for (int i = 0; i < leftObiClothAttach.Length; i++)
                {
                    leftObiClothAttach[i].enabled = false;
                }
                
            }
            
            else if (gameObject.name.Equals("RightChibi"))
            {
                netAttechment[1].enabled = false;
                
                for (int i = 0; i < rightObiClothAttach.Length; i++)
                {
                    rightObiClothAttach[i].enabled = false;
                }

            }
            otherChibi.gameObject.GetComponent<Animator>().SetTrigger("Fail");
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("FinishLine"))
        {
            GameManager.Instance.NextLevel();
        }
    }


    public bool IsGrounded()
    {
        return Physics.Raycast(gameObject.transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
