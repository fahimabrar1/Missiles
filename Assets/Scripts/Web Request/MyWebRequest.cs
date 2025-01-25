using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class MyWebRequest
{
    // AES Encryption Key and IV (16 bytes each)
    private const string EncryptionKey = "your16bytekey123";
    private const string EncryptionIV = "your16byteiv1234";
    protected string baseUrl = "https://api.baseurl.com";

    protected string mssidn = "";

    public MyWebRequest()
    {
        var fullUrl = Application.absoluteURL;

        mssidn = GetParameterFromUrl(fullUrl, "mssidn");
    }

    // Method to extract parameters from the URL
    private string GetParameterFromUrl(string url, string parameterName)
    {
        if (string.IsNullOrEmpty(url) || !url.Contains("?")) return null;

        var parts = url.Split('?');
        if (parts.Length < 2) return null;

        var parameters = parts[1].Split('&');
        foreach (var param in parameters)
        {
            var keyValue = param.Split('=');
            if (keyValue.Length == 2 && keyValue[0] == parameterName) return UnityWebRequest.UnEscapeURL(keyValue[1]);
        }

        return null;
    }

    // Encrypt data using AES-128
    private string EncryptData(string plainText)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aes.IV = Encoding.UTF8.GetBytes(EncryptionIV);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var encryptor = aes.CreateEncryptor())
            {
                var plainBytes = Encoding.UTF8.GetBytes(plainText);
                var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }

    // Fetch user data using mssidn
    // public IEnumerator FetchUserData(Action<string> onSuccess, Action<string> onFailure)
    // {
    //     var url = $"{baseUrl}/fetchUser?mssidn={mssidn}";
    //
    //     using var www = UnityWebRequest.Get(url);
    //     yield return www.SendWebRequest();
    //
    //     if (www.result != UnityWebRequest.Result.Success)
    //     {
    //         MyDebug.LogError($"Error fetching user data: {www.error}");
    //         onFailure?.Invoke(www.error);
    //     }
    //     else
    //     {
    //         try
    //         {
    //             var decryptedData = DecryptData(www.downloadHandler.text);
    //             MyDebug.Log($"Decrypted User Data: {decryptedData}");
    //             onSuccess?.Invoke(decryptedData);
    //         }
    //         catch (Exception e)
    //         {
    //             MyDebug.LogError($"Decryption failed: {e.Message}");
    //             onFailure?.Invoke($"Decryption failed: {e.Message}");
    //         }
    //     }
    // }

    // Send score with mssidn
    public IEnumerator SendScore(int score, Action<string> onSuccess, Action<string> onFailure)
    {
        var url = $"{baseUrl}/submitScore";

        // Combine mssidn and score into a JSON object
        var jsonData = JsonUtility.ToJson(new { mssidn, score });
        var encryptedData = EncryptData(jsonData);

        using var www = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(encryptedData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            MyDebug.LogError($"Error sending score: {www.error}");
            onFailure?.Invoke(www.error);
        }
        else
        {
            MyDebug.Log($"Score sent successfully: {www.downloadHandler.text}");
            onSuccess?.Invoke(www.downloadHandler.text);
        }
    }

    // Decrypt data using AES-128
    private string DecryptData(string encryptedText)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aes.IV = Encoding.UTF8.GetBytes(EncryptionIV);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var decrypt = aes.CreateDecryptor())
            {
                var encryptedBytes = Convert.FromBase64String(encryptedText);
                var decryptedBytes = decrypt.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}