using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    ParticleSystem ps;

    void Start()
    {
        ps = GameObject.Find("BallBreakParticles").GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            ps.gameObject.transform.position = collision.transform.position;
            ps.Emit(30);
        }
    }
}
