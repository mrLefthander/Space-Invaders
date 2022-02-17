using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
  private const float AnimationDuration = 0.4f;
  private const float ShakeStrength = 0.4f;

  [SerializeField]
  private PlayerHealth _playerHealth;

  private void OnEnable() => 
    _playerHealth.Died += Shake;

  private void OnDisable() => 
    _playerHealth.Died -= Shake;

  private void Shake(PlayerHealth player)
  {
    transform.DOShakePosition(AnimationDuration, ShakeStrength);

    if (player.LivesRemaining < 0)
      _playerHealth.Died -= Shake;    
  }
}
