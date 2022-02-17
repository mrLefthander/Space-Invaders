using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimation : MonoBehaviour
{
  private const float AnimationDuration = 0.5f;
  private const float ShakeStrength = 0.2f;

  private PlayerHealth _playerHealth;

  private SpriteRenderer _spriteRenderer;
  private Color _spriteColor;

  private Vector2 _initialPosition;

  private void Start()
  {
    _initialPosition = transform.position;
    GetSpriteRenderer();

    SubscribeOnPlayerDeath();
  }

  private void SubscribeOnPlayerDeath()
  {
    _playerHealth = GetComponent<PlayerHealth>();
    _playerHealth.Died += PlayDeathAnimation;
  }

  private void GetSpriteRenderer()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _spriteColor = _spriteRenderer.color;
  }

  private void PlayDeathAnimation(PlayerHealth player)
  {
    transform.DOShakeScale(AnimationDuration, ShakeStrength);

    if (_playerHealth.LivesRemaining < 0) return;

    _spriteRenderer
      .DOFade(0f, AnimationDuration)
      .OnComplete(ResetPlayer);
  }

  private void ResetPlayer()
  {
    transform.position = _initialPosition;
    _spriteRenderer.color = _spriteColor;
  }
}
