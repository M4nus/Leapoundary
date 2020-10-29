using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "Settings")]
public class Settings : ScriptableObject
{
    [SerializeField] private string graphicalSettingsName;
    [SerializeField] private string volumeSettingsName;
    
    void OnEnable()
    {
        Debug.Log("SDK lvl: " + GetSDKLevel());
        if(GetSDKLevel() <= 23 && GetSDKLevel() != 0)
        {
            SetGraphicalOption(0);
        }
        else
        {
            SetGraphicalOption(1);
        }
    }

    // 0 - unlit
    // 1 - glow
    public void SetGraphicalOption(int value)
    {
        PlayerPrefs.SetInt(graphicalSettingsName, value);
        PlayerPrefs.Save();
    }

    public int GetGraphicalOption()
    {
        return PlayerPrefs.GetInt(graphicalSettingsName);
    }

    // 0 - low
    // 1 - high
    public void SetVolumeOption(float value)
    {
        PlayerPrefs.SetFloat(volumeSettingsName, value);
        PlayerPrefs.Save();
    }

    public float GetVolumeOption()
    {
        return PlayerPrefs.GetFloat(volumeSettingsName);
    }

    public int GetSDKLevel()
    {
        var clazz = AndroidJNI.FindClass("android.os.Build$VERSION");
        var fieldID = AndroidJNI.GetStaticFieldID(clazz, "SDK_INT", "I");
        var sdkLevel = AndroidJNI.GetStaticIntField(clazz, fieldID);
        return sdkLevel;
    }
}
