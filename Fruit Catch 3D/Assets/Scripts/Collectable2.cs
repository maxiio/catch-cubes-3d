using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Obi;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Collectable2 : MonoBehaviour
{
    private bool die = false;
    private bool playerNear = false;
    private bool triggerParticle = false;
    private PlayerMovement player;

    private bool touchedChibi;
    private bool touchedNet;

    private Sprite[] sprites;

    private bool collideFirstTime = false;
    
    void Start()
    {
        //gameObject.transform.localScale = new Vector3(0,0,0);
        sprites = Resources.LoadAll<Sprite>("PositiveEmojis");
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        touchedChibi = false;
        touchedNet = false;
    }

    void Update()
    {
        if (die)
        {
            Destroy(gameObject);
        }
        

        if (gameObject.transform.position.x - player.gameObject.transform.position.x < 30 && playerNear == false)
        {
            //AnimationsManager.Instance.PopupCollectablesAnim(gameObject);
            AnimationsManager.Instance.PopupCollectablesSmashableAnim(gameObject);
            playerNear = true;
        }

        if (gameObject.transform.position.x - player.gameObject.transform.position.x < 4 && triggerParticle == false)
        {
            ParticleManager.Instance.CreateExplosionParticle(gameObject);
            triggerParticle = true;
        }

        if (touchedNet == true)
        {
            //Debug.Log("touched");
            GetComponent<Rigidbody>().velocity = (player.vel);
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("LeftChibi") || other.tag.Equals("RightChibi") || other.tag.Equals("Holder"))
        {
            touchedChibi = true;

            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            gameObject.GetComponent<ObiCollider>().SourceCollider = null;
            //gameObject.GetComponent<Rigidbody>().AddForce(10f,10f,10f);
            //gameObject.transform.GetChild(1).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //gameObject.transform.GetChild(1).gameObject.GetComponent<Rigidbody>().useGravity = true;
            

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                gameObject.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().useGravity = true;
                //gameObject.transform.GetChild(i).GetComponent<Rigidbody>().AddForce(10f,10f,10f);
            }

            //Destroy(gameObject);
        }
        
        else if (other.tag.Equals("Net") && !touchedChibi)
        {
            
            NetManager.Instance.Collected();
            //gameObject.transform.position = gameObject.transform.position + new Vector3(0.18311f, 0f, 0.5f);
            touchedNet = true;
            float collisionspeed = 1.5f;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
            gameObject.GetComponent<Rigidbody>().velocity = (new Vector3(1,0,0))*collisionspeed;
            //gameObject.GetComponent<BoxCollider>().enabled = false;
            //gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<ObiCollider>().Thickness += NetManager.Instance.thicknessAmount;

            //gameObject.transform.DOMove(gameObject.transform.position + new Vector3(0.18311f, 0.2f, -0.5f), .1f);
          
            
            //gameObject.transform.position = new Vector3(+0.18311f,1.388525f,-0.5331115f);
            //gameObject.transform.position = new Vector3(51.18303f,1.189945f,0.6526048f);

            
            
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        
        yield return  new WaitForSeconds(.3f - Mathf.Clamp((player.speed * .8f), 0f, .1f));

        if (collideFirstTime == false)
        {
            CollectedText();
            SetEmojis();
            ParticleManager.Instance.ExplosionParticle();
            AnimationsManager.Instance.ExplosionParticleAnim();    
            gameObject.GetComponent<ObiCollider>().enabled = false; 
        }
       

        if (!die)
            player.SpeedUp();
        //die = true;
        
        collideFirstTime = true;
    }

   
    private void CollectedText()
    {
        GameObject newObj = new GameObject("PlusText");
        newObj.transform.position = gameObject.transform.position;
        newObj.transform.Rotate(0,90,0);
        newObj.gameObject.transform.SetParent(player.gameObject.transform);
        newObj.AddComponent<TextMeshPro>();
        newObj.GetComponent<TextMeshPro>().text = "+1";
        newObj.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
        newObj.GetComponent<TextMeshPro>().color = new Color32(0, 0, 0, 255);
        newObj.GetComponent<TextMeshPro>().fontSize = 6f;
        newObj.GetComponent<TextMeshPro>().font = Resources.Load<TMP_FontAsset>("Fonts/BalooBhai2-Bold SDF");
        AnimationsManager.Instance.CollectTextAnim(newObj);
    }
    
    private void SetEmojis()
    {
        GameObject newObj = new GameObject("Emote");
        newObj.transform.position = new Vector3(gameObject.transform.position.x,5.6f,-.56f);
        newObj.transform.Rotate(0,90,0);
        newObj.gameObject.transform.SetParent(player.gameObject.transform);
        newObj.gameObject.transform.localScale = Vector3.zero;
        newObj.AddComponent<SpriteRenderer>();
        int index = Random.Range(0,sprites.Length);
        newObj.GetComponent<SpriteRenderer>().sprite = sprites[index];
        AnimationsManager.Instance.CollectEmojiAnim(newObj);
        
    }

    

}
