using UnityEngine.SceneManagement;

public class SceneLoader
{
    public void LoadGameScene() => 
        SceneManager.LoadScene(SceneNames.GameScene);

    public void RestartScene() => 
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
    private void LoadScene(string name) => 
        SceneManager.LoadScene(name);
}