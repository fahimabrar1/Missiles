using DefaultNamespace;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnStartGame()
    {
        GameManager.Instance.OnStartGame();
    }
}