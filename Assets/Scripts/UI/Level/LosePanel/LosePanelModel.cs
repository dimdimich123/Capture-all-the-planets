using UnityEngine.SceneManagement;

public sealed class LosePanelModel
{
    public void GoToMenu()
    {
        SceneManager.LoadScene(SceneName.Menu.ToString());
    }
}
