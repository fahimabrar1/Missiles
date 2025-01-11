using System.Collections.Generic;
using UnityEngine;

public class LevelAudioPlayer : MonoBehaviour
{
    public static LevelAudioPlayer instance;

    public List<AudioModel> AllAudios;

    /// <summary>
    ///     Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
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
        MyDebug.Log($"On {name} audio played");

        var foundModel = AllAudios.Find(model => model.name.Equals(name));
        foundModel.Play();
    }


    public void OnStopAudioByName(string name)
    {
        MyDebug.Log("On Token Move audio played");

        var foundModel = AllAudios.Find(model => model.name.Equals(name));
        foundModel.Stop();
    }
}