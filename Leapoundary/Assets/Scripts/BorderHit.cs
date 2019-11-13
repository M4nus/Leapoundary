using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderHit : MonoBehaviour
{
    ParticleSystem ps;

    private void Awake()
    {
        ps = GameObject.Find("BorderHitParticles").GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            ps.gameObject.transform.position = collision.transform.position;
            ps.Emit(20);
        }
    }
}
