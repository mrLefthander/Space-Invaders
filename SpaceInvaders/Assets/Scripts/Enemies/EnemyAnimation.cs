using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(EnemyDeath))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyAnimation : MonoBehaviour
{
  private const float AnimationDuration = 0.1f;
  private const float ShakeStrength = 0.2f;
  
  private EnemyDeath _enemyDeath;
  private SpriteRenderer _spriteRenderer;
  private Color _spriteColor;

  private void Start()
  {
    GetSpriteRenderer();
    SubscribeToEnemyDeath();
  }

  private void SubscribeToEnemyDeath()
  {
    _enemyDeath = GetComponent<EnemyDeath>();
    _enemyDeath.Died += PlayDeathAnimation;
  }

  private void GetSpriteRenderer()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _spriteColor = _spriteRenderer.color;
  }

  private void PlayDeathAnimation(EnemyDeath enemy, int score)
  {
    Shake();
    Fade();
  }
  private void Shake() =>
  transform.DOShakePosition(AnimationDuration, ShakeStrength);

  private void Fade() => 
    _spriteRenderer
      .DOFade(0f, AnimationDuration)
      .OnComplete(Deactivate);

  private void Deactivate()
  {
    _spriteRenderer.color = _spriteColor;
    gameObject.SetActive(false);
  }
}
