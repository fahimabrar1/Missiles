using UnityEngine;

public class WebApiManager : MonoBehaviour
{
    private MyWebRequest myWebRequest;

    private void Start()
    {
        myWebRequest = new MyWebRequest();

        // Fetch user data and send score
        // StartCoroutine(myWebRequest.FetchUserData(userData => { MyDebug.Log($"User Data Fetched: {userData}"); },
        //     error => MyDebug.LogError($"Error Fetching User Data: {error}")));
    }


    public void OnSendScore()
    {
        var score = 100; // Example score
        StartCoroutine(myWebRequest.SendScore(score,
            response => MyDebug.Log($"Score Sent Successfully: {response}"),
            error => MyDebug.LogError($"Error Sending Score: {error}")
        ));
    }
}