using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public Animator anim;
    public static string sceneName;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
        anim = GetComponent<Animator>();
        DontDestroyOnLoad(instance);
    }

    public void FadeOutMusic()
    {
        anim.SetTrigger("FadeOut");
        StartCoroutine(destroyMusicManager());
    }

    IEnumerator destroyMusicManager()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
