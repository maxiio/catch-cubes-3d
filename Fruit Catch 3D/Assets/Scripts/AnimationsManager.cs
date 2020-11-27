using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    
    [SerializeField] private GameObject leftChibi;
    [SerializeField] private GameObject rightChibi;
    
    
    //DeadCanvas Animataions
    [SerializeField] private GameObject skipButton;
    [SerializeField] private GameObject noThanks;

    [SerializeField] private GameObject explosionParticle;
    [SerializeField] private GameObject chibiBag;
    
    
    public static AnimationsManager Instance;
    private void Awake()
    {
        SingletonPattern();
        DOTween.Init();
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
    
    
    public void CollectTextAnim(GameObject plusText)
    {
        plusText.transform.DOMoveY(2.6f,1f).OnComplete((() => Destroy(plusText)));
    }

    public void CollectEmojiAnim(GameObject Emoji)
    {
        Emoji.gameObject.transform.DOScale(.4f, 1f).SetEase(Ease.OutBounce).OnComplete((() => Destroy(Emoji)));
    }

  
    public void PlayerStartAnim()
    {
        leftChibi.GetComponent<Animator>().SetTrigger("GameStarted");
        rightChibi.GetComponent<Animator>().SetTrigger("GameStarted");
    }

    public void WallCrashedAnim(Collider other)
    {
        other.GetComponent<Animator>().SetTrigger("WallCrashed");

        if (other.gameObject.tag.Equals("LeftChibi"))
        {
            rightChibi.GetComponent<Animator>().SetTrigger("Fail");
        }
        else if (other.gameObject.tag.Equals("RightChibi"))
        {
            leftChibi.GetComponent<Animator>().SetTrigger("Fail");
        }
    }

    public void PopupCollectablesAnim(GameObject collectable)
    {
        collectable.transform.DOScale(0.5f, 1f);
        collectable.transform.DOMoveY(1.4f, 1f).SetEase(Ease.OutBounce);
    }
    
    public void PopupCollectablesSmashableAnim(GameObject collectable)
    {
        collectable.transform.DOScale(1f, 1f);
        collectable.transform.DOMoveY(1.2f, 1f).SetEase(Ease.OutBounce);
    }

    public void ExplosionParticleAnim()
    {
        //ParticleSystem.ShapeModule _editableShape = explosionParticle.GetComponent<ParticleSystem>().shape;
        //_editableShape.position = chibiBag.transform.localPosition;
        //explosionParticle.GetComponent<ParticleSystem>().shape.
    }

    public void SkipButtonAnim()
    {
        skipButton.transform.DOScale(new Vector3(.8f,.8f,.8f), .7f).SetEase(Ease.OutBounce);
    }

    public void NoThansButtonAnim()
    {
        noThanks.transform.DOScale(Vector3.one, .5f);
    }
    
}
