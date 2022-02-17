using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreUI : MonoBehaviour
{
  [SerializeField]
  private TMP_Text _scoreText;

  [SerializeField]
  private Score _gameScore;

  private int _lastScoreValue = 0;

  private void OnEnable() => 
    _gameScore.Changed += OnChange;

  private void OnDisable() => 
    _gameScore.Changed -= OnChange;

  private void OnChange(int newScore)
  {    
    DOVirtual.Int(_lastScoreValue, newScore, 0.4f, OnValueUpdate);
    _lastScoreValue = newScore;
  }

  private void OnValueUpdate(int score) => 
    _scoreText.text = score.ToString();
}
