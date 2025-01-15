using System.Collections.Generic;
using UnityEngine;

public class LevelAudioPlayer : MonoBehaviour
{
    public static LevelAudioPlayer instance;

    public List<AudioModel> AllAudios;
    public AudioListener audioListener;

    /// <summary>
    ///     Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        var isAudioOn = PlayerPrefs.GetInt("audio_on", 1);
        AudioListener.volume = isAudioOn;
    }


    /// <summary>
    ///     Start is called on the frame when a script is enabled just before
    ///     any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        for (var i = 0; i < AllAudios.Count; i++)
        {
            AllAudios[i].audioSource = gameObject.AddComponent<AudioSource>();
            AllAudios[i].InitializeAudioSource();
        }
    }


    public void OnPlayAudioByName(string name)
    {
        var foundModel = AllAudios.Find(model => model.name.Equals(name));
        if (foundModel != null)
        {
            MyDebug.Log($"On {name} audio played");
            foundModel.Play();
        }
        else
        {
            MyDebug.Log($"AudioModel with name {name} not found.");
        }
    }

    public void OnStopAudioByName(string name)
    {
        var foundModel = AllAudios.Find(model => model.name.Equals(name));
        if (foundModel != null)
        {
            MyDebug.Log("On Token Move audio played");
            foundModel.Stop();
        }
        else
        {
            MyDebug.Log($"AudioModel with name {name} not found.");
        }
    }
}