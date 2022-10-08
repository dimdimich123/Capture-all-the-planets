using UnityEngine.SceneManagement;

public sealed class WinPanelModel
{
    public void GoToMenu()
    {
        SceneManager.LoadScene(SceneName.Menu.ToString());
    }
}
