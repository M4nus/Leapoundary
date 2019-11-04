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
        if(CanShoot() && Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootBall();
        }
    }

    private void ShootBall()
    {
        ps.GetCrosshairDirection(gameObject);
        ps.ball.GetComponent<Rigidbody2D>().AddForce(ps.ballSpeed * transform.right * Time.fixedDeltaTime, ForceMode2D.Impulse);
        ps.ball.transform.parent = null;
    }

    private bool CanShoot()
    {
        if(ps.ballState == BallState.Safe)
            return true;
        else
            return false;
    }    
}
