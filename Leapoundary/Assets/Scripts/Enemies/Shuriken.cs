using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shuriken : MonoBehaviour
{
    private SpriteGlow.SpriteGlowEffect glow;
    private Rigidbody2D rb;
    private Color color;
    private float angle;

    public float speed = 100f;
    public float rotateSpeed = 100f;

    public Color[] colors;
    public Settings settings;

    private void OnEnable()
    {

        if(settings.GetGraphicalOption() == 0)
        {
            GetComponentInChildren<SpriteRenderer>().color = colors[Random.Range(7, 13)];
        }
        else if(settings.GetGraphicalOption() == 1)
        {
            GetComponentInChildren<SpriteGlow.SpriteGlowEffect>().GlowColor = colors[Random.Range(0, 6)];
        }

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Random.insideUnitCircle.normalized * speed;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private void Start()
    {
        if(settings.GetGraphicalOption() == 0)
        {
            GetComponentInChildren<SpriteGlow.SpriteGlowEffect>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        angle += rotateSpeed * Time.fixedDeltaTime;


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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            PlayerSettings.instance.ResetBall();
            PlayerSettings.instance.HurtBall();
        }
    }
}
