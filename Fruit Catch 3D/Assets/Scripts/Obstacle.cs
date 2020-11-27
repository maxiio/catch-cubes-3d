using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Obi;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private PlayerMovement player;
    private ObiParticleAttachment[] backObiClothAttach;
    private ObiParticleAttachment[] leftObiClothAttach;
    private ObiParticleAttachment[] rightObiClothAttach;
    private GameObject leftHolder;
    private GameObject rightHolder;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        backObiClothAttach = GameObject.Find("BackObiCloth").GetComponents<ObiParticleAttachment>();
        leftObiClothAttach = GameObject.Find("LeftObiCloth").GetComponents<ObiParticleAttachment>();
        rightObiClothAttach = GameObject.Find("RightObiCloth").GetComponents<ObiParticleAttachment>();
        leftHolder = GameObject.Find("LeftHolder");
        rightHolder = GameObject.Find("RightHolder");
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("LeftChibi") || other.gameObject.tag.Equals("RightChibi"))
        {
            player.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        
            if(other.gameObject.tag.Equals("LeftChibi"))
            {
                backObiClothAttach[0].enabled = false;
                leftHolder.GetComponent<Rigidbody>().useGravity = true;
                leftHolder.GetComponent<Rigidbody>().isKinematic = false;
                leftHolder.GetComponent<Rigidbody>().velocity = Vector3.left;

                for (int i = 0; i < leftObiClothAttach.Length; i++)
                {
                    leftObiClothAttach[i].enabled = false;
                }
            }
        
            else if (other.gameObject.tag.Equals("RightChibi"))
            {
                backObiClothAttach[1].enabled = false;
                rightHolder.GetComponent<Rigidbody>().useGravity = true;
                rightHolder.GetComponent<Rigidbody>().isKinematic = false;
                rightHolder.GetComponent<Rigidbody>().velocity = Vector3.left;
                
                for (int i = 0; i < rightObiClothAttach.Length; i++)
                {
                    rightObiClothAttach[i].enabled = false;
                }
            }

            if (other.gameObject.tag.Equals("LeftChibi") || other.gameObject.tag.Equals("RightChibi"))
            {
                GameManager.Instance.DeactivateScripts();
                UIManager.Instance.GameOver();
                CameraManager.Instance.DeadCamera();
            }
            
            backObiClothAttach[2].enabled = true;
        
            AnimationsManager.Instance.WallCrashedAnim(other);
        }
    }
}
