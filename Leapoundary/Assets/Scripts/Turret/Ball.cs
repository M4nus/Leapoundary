using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    ParticleContainer pc;

    void Start()
    {
        pc = GameObject.Find("GameManager").GetComponent<ParticleContainer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            Instantiate(pc.ballBreak, collision.transform.position, Quaternion.identity);
            Instantiate(pc.enemyDeath, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            AudioManager.instance.PlayRandom("EnemyHit");
            AudioManager.instance.PlayRandom("BallReturn");
        }
    }
}
