using UnityEngine;
using System;

public class Score : MonoBehaviour
{
  [SerializeField]
  private FloatingText _floatingTextPrefab;

  public event Action<int> Changed;

  private FloatingTextFactory _floatingTextFactory;
  private int _score = 0;

  private void Start()
  {
    Changed?.Invoke(_score);

    _floatingTextFactory = new FloatingTextFactory(_floatingTextPrefab, transform);
  }

  public void GainScoreFrom(Vector2 position, int value)
  {
    _score += value;
    Changed?.Invoke(_score);
    ShowFloatingText(position, value);
  }

  private void ShowFloatingText(Vector2 position, int scoreValue)
  {
    var floatingText = _floatingTextFactory.Get();
    floatingText.Play(position, scoreValue);
  }
}
