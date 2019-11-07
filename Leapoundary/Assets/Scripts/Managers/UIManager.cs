using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Animator anim;
    public GameObject panel;
    public GameObject deathIcons;


    // Start is called before the first frame update
    void Start()
    {
        anim = panel.GetComponent<Animator>();
    }

    private void Update()
    {
        if(PlayerSettings.instance.ballState == BallState.Death)
        {
            FadeIn();
            deathIcons.SetActive(true);
        }
    }

    public void FadeOut()
    {
        anim.SetBool("TurnWhite", false);
    }

    public void FadeIn()
    {
        anim.SetBool("TurnWhite", true);
    }

    public void Retry()
    {
        FadeOut();
        PlayerSettings.instance.ballState = BallState.Safe;
        SceneManager.LoadScene("Base");
    }

    public void GoToMenu()
    {
        FadeOut();
        PlayerSettings.instance.ballState = BallState.Safe;
        SceneManager.LoadScene("Menu");
    }
}
