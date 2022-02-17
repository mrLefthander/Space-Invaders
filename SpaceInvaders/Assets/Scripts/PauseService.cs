using System;
using UnityEngine;

public class PauseService
{
  public static event Action<bool> Paused;

  public void Pause() =>
    ChangePauseStateTo(true);

  public void UnPause() =>
    ChangePauseStateTo(false);

  private void ChangePauseStateTo(bool isPaused)
  {
    Paused?.Invoke(isPaused);
    Time.timeScale = isPaused ? 0f : 1f;
  }
}
