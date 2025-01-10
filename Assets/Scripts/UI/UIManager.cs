using System.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject mainMenuObj;
    public GameObject inGameUIObj;

    public MainMenu mainMenu;
    public InGameUI inGameUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public async void OnStartGame()
    {
        GameManager.Instance.OnStartGame();
        await Task.Delay(2000);
        inGameUIObj.SetActive(true);
    }
}