using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticle;
    [SerializeField] private GameObject runningParticle;
    
    public static ParticleManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        SingletonPattern();
        runningParticle.GetComponent<ParticleSystem>().Stop();
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
    
    public void ExplosionParticle()
    {
        //Debug.Log("test");
        explosionParticle.GetComponent<ParticleSystem>().Play();
    }

    public void RunningParticle()
    {
        runningParticle.GetComponent<ParticleSystem>().Play();
    }

    public void CreateExplosionParticle(GameObject obj)
    {
        //tempParticle = Instantiate(explosionParticlePrefab,obj.transform.position - new Vector3(.5f,-.5f,0f),Quaternion.identity);
        explosionParticle.transform.position = obj.transform.position - new Vector3(.5f,-.5f,0f);
    }
    
    
    
    
}
