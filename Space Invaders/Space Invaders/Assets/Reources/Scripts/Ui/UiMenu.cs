using UnityEngine;
using UnityEngine.SceneManagement;

public class UiMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SfxManager.instance.PlaySfxUi();
        Time.timeScale = 1;
        SfxManager.instance.SetTrack(1, 0.5f);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        SfxManager.instance.PlaySfxUi();
        Application.Quit();
    }

}
