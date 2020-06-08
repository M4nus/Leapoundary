using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;


     
public class LoadAd : MonoBehaviour
{
#if UNITY_ANDROID
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            ShowRewardedAd();
        }
    }

    public void ShowRewardedAd()
    {
        if(Advertisement.IsReady())
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Finished:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case ShowResult.Failed:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }

#elif UNITY_IPHONE

     // iPhone specific code


    
#elif UNITY_EDITOR
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            ShowRewardedAd();
        }
    }

    public void ShowRewardedAd()
    {
        if(Advertisement.IsReady())
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Finished:
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //
            // YOUR CODE TO REWARD THE GAMER
            // Give coins etc.
            break;
            case ShowResult.Skipped:
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            break;
            case ShowResult.Failed:
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            break;
        }
    }

#else

    // other platforms

#endif


}
