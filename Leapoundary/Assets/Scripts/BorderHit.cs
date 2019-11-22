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
            Instantiate(pc.borderHit, collision.transform.position, Quaternion.identity);
            anim.SetTrigger("Shine");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        anim.SetTrigger("Shine");
    }
}
