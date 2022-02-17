using UnityEngine;
using UnityEngine.UI;

public class GameScreenUI : MonoBehaviour
{
  [SerializeField]
  private Button _restartButton;
  [SerializeField]
  private Button _mainMenuButton;
  [SerializeField]
  private GameObject _gameScreenContainer;

  [SerializeField]
  private SceneLoader _sceneLoader;

  private void Start() => 
    _gameScreenContainer.SetActive(false);

  private void OnEnable()
  {
    _restartButton.onClick.AddListener(_sceneLoader.LoadGameplayScene);
    _mainMenuButton.onClick.AddListener(_sceneLoader.LoadMainMenu);
  }

  private void OnDisable()
  {
    _restartButton.onClick.RemoveListener(_sceneLoader.LoadGameplayScene);
    _mainMenuButton.onClick.RemoveListener(_sceneLoader.LoadMainMenu);
  }

  public void Show()
  {
    _gameScreenContainer.SetActive(true);
  }
}
