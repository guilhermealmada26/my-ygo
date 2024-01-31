using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/SceneLoader")]
public class SceneLoader : ScriptableObject
{
    [SerializeField]private string previousScene = "";

    public string PreviousScene => previousScene;
    public string CurrentScene => SceneManager.GetActiveScene().name;

    public void LoadScene(string scene)
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }

    public void RestartScene()
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadPreviousScene()
    {
        if (string.IsNullOrEmpty(previousScene))
        {
            Debug.LogWarning("Called previous scene wihtout any previous. Scene reloaded");
            LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        LoadScene(previousScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}