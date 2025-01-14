using UI;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public ToggleButton audioToggle;
    private AudioListener AudioListener;

    private void Awake()
    {
        AudioListener = FindObjectOfType<AudioListener>();
    }

    private void OnEnable()
    {
        var isAudioOn = PlayerPrefs.GetInt("audio_on", 1);

        audioToggle.ToggleButtonOn(isAudioOn == 1);
        AudioListener.enabled = isAudioOn == 1;
    }


    public void OnChangeSound(bool value)
    {
        AudioListener.enabled = value;
        PlayerPrefs.SetInt("audio_on", value ? 1 : 0);
        PlayerPrefs.Save();
    }
}