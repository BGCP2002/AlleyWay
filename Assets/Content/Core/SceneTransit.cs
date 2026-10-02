using System;
using UnityEngine.SceneManagement;

public class SceneTransit
{
    public SceneTransit()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public event Action SceneLoaded;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneLoaded?.Invoke();
    }
}