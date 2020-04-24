using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Animator anim;
    public GameObject panel;
    public GameObject deathIcons;
    public GameObject optionsIcons;


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

    public void ToggleOptions()
    {
        if(!optionsIcons.active)
        {
            Time.timeScale = 0f;
            optionsIcons.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            optionsIcons.SetActive(false);
        }
    }

    public void FadeOut()
    {
        anim.SetBool("TurnWhite", false);
        panel.SetActive(false);
    }

    public void FadeIn()
    {
        panel.SetActive(true);
        anim.SetBool("TurnWhite", true);
    }

    public void Retry()
    {
        FadeOut();
        Time.timeScale = 1f;
        PlayerSettings.instance.ballState = BallState.Safe;
        if(SceneManager.GetActiveScene().name == "ClassicMode")
            SceneManager.LoadScene("ClassicMode");
        if(SceneManager.GetActiveScene().name == "RainbowMode")
            SceneManager.LoadScene("RainbowMode");
    }

    public void GoToMenu()
    {
        FadeOut();
        Time.timeScale = 1f;
        PlayerSettings.instance.ballState = BallState.Safe;
        SceneManager.LoadScene("Menu");
    }

    ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
    ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for(int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if(curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }
    ///Gets all event systen raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
