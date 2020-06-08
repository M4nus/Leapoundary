using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 100f;

    private void OnEnable()
    {
        Vector3 dir = Vector3.zero - this.transform.position;
        this.transform.up = -dir;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.up * speed;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        StartCoroutine(DisableCooldown());
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    
    void Update()
    {
        if(PlayerSettings.instance.ballState != BallState.Safe && PlayerSettings.instance.ballState != BallState.Upgrade)
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }

    IEnumerator DisableCooldown()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            PlayerSettings.instance.ResetBall();
            PlayerSettings.instance.HurtBall();
        }
    }
}
