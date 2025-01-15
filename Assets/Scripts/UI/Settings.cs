using UI;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public ToggleButton audioToggle;


    private Coroutine _toggleCoroutine;

    private void OnEnable()
    {
        var isAudioOn = PlayerPrefs.GetInt("audio_on", 1);
        audioToggle.ToggleButtonOn(isAudioOn == 1);
    }

    public void OnChangeSound(bool value)
    {
        AudioListener.volume = value ? 1 : 0;
        PlayerPrefs.SetInt("audio_on", value ? 1 : 0);
        PlayerPrefs.Save();
    }
}