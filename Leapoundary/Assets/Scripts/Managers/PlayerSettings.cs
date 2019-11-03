using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallState
{
    Safe,
    Launched
}

public class PlayerSettings : MonoBehaviour
{
    // Script with all the variables needed for ball, turret, enemies
    public static PlayerSettings instance;

    public BallState ballState;

    public GameObject ball;
    public GameObject currentTurret;
    public GameObject nextTurret;

    public int lives;
    public int leaps = 0;
    public float ballSpeed;
    public float triangleSpeed;
    public float randomerSpeed;


    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        //Checking whether ball was launched or not
        if(ball.transform.parent == currentTurret.transform)
            ballState = BallState.Safe;
        else if(ball.transform.parent != currentTurret.transform)
            ballState = BallState.Launched;

        //Cheats
        if(Input.GetKeyDown(KeyCode.R))
            ResetBall();
    }

    public void ResetBall()
    {
        //Setting ball in CurrentTurret
        ball.transform.parent = currentTurret.transform;
        ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        ball.transform.position = currentTurret.transform.position;
    }

    public void MoveTurrets()
    {
        //Moving CurrentTurret to NextTurret position and then generating NextTurret new position
        currentTurret.transform.position = nextTurret.transform.position;
        nextTurret.transform.position = new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(-3.5f, 3.5f));
        leaps++;
    }

    public void GetCrosshairDirection(GameObject obj)
    {
        // Converting mouse position into world coordinates
        // Getting direction to point at
        // Setting vector of transform directly
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseScreenPosition - (Vector2)obj.transform.position).normalized;
        obj.transform.right = direction;
    }

}
