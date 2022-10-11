using UnityEngine.SceneManagement;

/// <summary>
/// Stores LosePanel data.
/// </summary>

public sealed class LosePanelModel
{
    public void GoToMenu()
    {
        SceneManager.LoadScene(SceneName.Menu.ToString());
    }
}
