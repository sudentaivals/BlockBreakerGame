using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactVFXScript : MonoBehaviour
{
    private ParticleSystem ParticleComponent => GetComponent<ParticleSystem>();

    // Start is called before the first frame update
    void Start()
    {
        DestroyAfterDuration();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckForExpire();

    }

    private void CheckForExpire()
    {
        if (!ParticleComponent.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    private void DestroyAfterDuration()
    {
        Destroy(gameObject, ParticleComponent.main.duration + ParticleComponent.main.startLifetime.constant);
    }
}
