using System.Threading.Tasks;
using DefaultNamespace;
using UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject mainMenuObj;
    public GameObject bottomMenuObj;
    public GameObject inGameUIObj;

    public MainMenu mainMenu;
    public BottomMenu bottomMenu;
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
        bottomMenu.OnHideButtons();
        GameManager.Instance.OnStartGame();
        await Task.Delay(2000);
        inGameUI.OnEnableUI();
    }

    public void OnReturnToMainMenu()
    {
        Time.timeScale = 1;
        GameManager.Instance.plane.SetActive(true);
        bottomMenu.OnShowSettingsButtons();
        inGameUI.HideGameOverUI();
        inGameUI.OnDisableUI();
        mainMenu.OnShowMainMenu();
    }

    public void ShowGameOverPanel()
    {
        inGameUI.ShowGameOverUI();
    }

    public void HideGameOverUI()
    {
        inGameUI.HideGameOverUI();
        OnStartGame();
    }
}