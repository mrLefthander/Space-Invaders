using UnityEngine;
using System;
using System.Collections;

public class GameSession : MonoBehaviour
{
  private const int CountDownTime = 3;
  private const float Delay = 0.5f;

  [Header("UI")]
  [SerializeField]
  private CountDownUI _countDownUI;
  [SerializeField]
  private GameScreenUI _gameOverUI;
  [SerializeField]
  private GameScreenUI _gameWinUI;

  [Space] [Header("Signal Sources")]
  [SerializeField]
  private PlayerHealth _playerHealth;
  [SerializeField]
  private EnemiesMonitor _enemiesMonitor;

  private PauseService _pauseService;

  private void OnEnable()
  {
    _playerHealth.Died += OnPlayerDeath;
    _enemiesMonitor.GameWon += OnGameWin;
  }

  private void Start()
  {
    _pauseService = new PauseService();
    StartCoroutine(PauseForTime(CountDownTime));
  }

  private void OnDisable()
  {
    _playerHealth.Died -= OnPlayerDeath;
    _enemiesMonitor.GameWon -= OnGameWin;
  }

  private void OnGameWin() => 
    StartCoroutine(ShowGameScreen(_gameWinUI));

  private void OnPlayerDeath(PlayerHealth player)
  {
    if (player.LivesRemaining < 0)
      StartCoroutine(ShowGameScreen(_gameOverUI));
    else
      StartCoroutine(PauseForTime(CountDownTime));
  }

  private IEnumerator ShowGameScreen(GameScreenUI screen)
  {
    _pauseService.Pause();
    yield return new WaitForSecondsRealtime(Delay);
    screen.Show();
  }

  private IEnumerator PauseForTime(int seconds)
  {
    _pauseService.Pause();
    yield return new WaitForSecondsRealtime(Delay);
    StartIUCountdown(seconds);
    yield return new WaitForSecondsRealtime(seconds);
    _pauseService.UnPause();
  }

  private void StartIUCountdown(int seconds)
  {
    if (_countDownUI == null) return;

    _countDownUI.StartCoundown(seconds);
  }
}
