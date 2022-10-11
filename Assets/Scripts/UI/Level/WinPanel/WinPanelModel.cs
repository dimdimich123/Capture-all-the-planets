using UnityEngine.SceneManagement;

/// <summary>
/// Stores WinPanel data.
/// </summary>
public sealed class WinPanelModel
{
    public void GoToMenu()
    {
        SceneManager.LoadScene(SceneName.Menu.ToString());
    }
}
