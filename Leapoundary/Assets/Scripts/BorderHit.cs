using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BorderHit : MonoBehaviour
{
    ParticleContainer pc;
    Animator anim;


    private void Awake()
    {
        pc = GameObject.Find("GameManager").GetComponent<ParticleContainer>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            GameObject hitParticles = ObjectPooler.sharedInstance.GetPooledObject("BorderHit");
            if(hitParticles != null)
            {
                hitParticles.transform.position = collision.transform.position;
                hitParticles.SetActive(true);
            }
            PlayerSettings.instance.borderHitCount++;

            anim.SetTrigger("Shine");
            AudioManager.instance.PlayRandom("WallHit");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        anim.SetTrigger("Shine");
    }
}
