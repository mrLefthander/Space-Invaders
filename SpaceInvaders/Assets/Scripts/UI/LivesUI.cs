using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
  [SerializeField]
  private TMP_Text _livesText;

  [SerializeField]
  private PlayerHealth _playerHealth;

  private void OnEnable() => 
    _playerHealth.Died += UpdateRemainingLives;

  private void OnDisable() => 
    _playerHealth.Died -= UpdateRemainingLives;

  private void UpdateRemainingLives(PlayerHealth player)
  {
    if (player.LivesRemaining < 0) return;

    _livesText.text = player.LivesRemaining.ToString();
  }
}
