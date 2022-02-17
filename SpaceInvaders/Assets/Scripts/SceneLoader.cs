using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneLoader", menuName = "ScriptableObjects/Scene Loader")]
public class SceneLoader : ScriptableObject
{
  private const string MainMenuScene = "MainMenu";
  private const string GameplayScene = "Gameplay";

  public void LoadGameplayScene() => 
    SceneManager.LoadScene(GameplayScene);

  public void LoadMainMenu() => 
    SceneManager.LoadScene(MainMenuScene);
}
