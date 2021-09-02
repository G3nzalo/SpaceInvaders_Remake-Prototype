using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance = null;

    #region Unity ref
    public GameObject panelGame = null;
    public GameObject panelPause = null;
    public GameObject panelCredits = null;

    public TextMeshProUGUI life = null;

    #endregion


    #region Unity Calls
    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        Inpause();
    }

    #endregion

    #region Public Methods
    public void CurrentLife(int _currentLife)
    {
        life.text = _currentLife.ToString();
    }

    public void Inpause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (!panelPause.activeInHierarchy)
            {
                SfxManager.instance.PlaySfxUi();
                Time.timeScale = 0;
                panelGame.SetActive(false);
                panelPause.SetActive(true);
            }

           else {
                SfxManager.instance.PlaySfxUi();
                Time.timeScale = 1;
                panelGame.SetActive(true);
                panelPause.SetActive(false);
            }

        }
    }

    public void InCredits()
    {
        Time.timeScale = 0;
        panelGame.SetActive(false);
        panelCredits.SetActive(true);
    }

    public void RestarGame()
    {
        SfxManager.instance.PlaySfxUi();
        SceneManager.LoadScene(1);
        Time.timeScale = 1; 
    }

    public void ReturnGame()
    {
        SfxManager.instance.PlaySfxUi();
        panelPause.SetActive(false);
        panelGame.SetActive(true);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {

        SfxManager.instance.PlaySfxUi();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        SfxManager.instance.PlaySfxUi();
        Application.Quit();
    }
    #endregion

    private void Initialize()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
