using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderHit : MonoBehaviour
{
    ParticleContainer pc;

    private void Awake()
    {
        pc = GameObject.Find("GameManager").GetComponent<ParticleContainer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            Instantiate(pc.borderHit, collision.transform.position, Quaternion.identity);
        }
    }
}
