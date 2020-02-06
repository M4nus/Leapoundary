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
            GameObject ballParticles = ObjectPooler.sharedInstance.GetPooledObject("BallBreak");
            if(ballParticles != null)
            {
                ballParticles.transform.position = collision.transform.position;
                ballParticles.SetActive(true);
            }

            GameObject enemyParticles = ObjectPooler.sharedInstance.GetPooledObject("EnemyDeath");
            if(ballParticles != null)
            {
                enemyParticles.transform.position = collision.gameObject.transform.position;
                enemyParticles.SetActive(true);
            }
            collision.gameObject.SetActive(false);
            AudioManager.instance.PlayRandom("EnemyHit");
            AudioManager.instance.PlayRandom("BallReturn");
        }
    }
}
