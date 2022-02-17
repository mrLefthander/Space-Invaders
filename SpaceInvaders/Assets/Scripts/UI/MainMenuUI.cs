using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
  [SerializeField]
  private Button _playButton;
  [SerializeField]
  private SceneLoader _sceneLoader;

  private void OnEnable() => 
    _playButton.onClick.AddListener(_sceneLoader.LoadGameplayScene);

  private void OnDisable() => 
    _playButton.onClick.RemoveListener(_sceneLoader.LoadGameplayScene);
}
