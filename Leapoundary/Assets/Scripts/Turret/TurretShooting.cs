using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    [HideInInspector]
    public PlayerSettings ps;
    public GameObject gameManager;

    void Start()
    {
        ps = gameManager.GetComponent<PlayerSettings>(); 
    }
    
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            // Make a pointer that shows direction
        }
        if(CanShoot() && Input.GetKeyUp(KeyCode.Mouse0))
        {
            ShootBall();
        }
    }

    private void ShootBall()
    {
        if(!UIManager.IsPointerOverUIElement())
        {
            ps.GetCrosshairDirection(gameObject);
            ps.ball.GetComponent<Rigidbody2D>().AddForce(ps.ballSpeed * transform.right * Time.fixedDeltaTime, ForceMode2D.Impulse);
            ps.ball.transform.parent = null;
            AudioManager.instance.PlayRandom("TurretShoot");
        }
    }

    private bool CanShoot()
    {
        if(ps.ballState == BallState.Safe)
            return true;
        else
            return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerSettings.instance.ResetBall();
    }
}
