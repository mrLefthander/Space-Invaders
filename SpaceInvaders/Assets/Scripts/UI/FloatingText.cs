using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

[RequireComponent(typeof(TMP_Text))]
public class FloatingText : MonoBehaviour
{
  private const float DefaultDuration = 0.6f;
  private const float AnimationXYShift = 0.4f;
  private const float AnimationEndScale = 0.5f;

  public event Action<FloatingText> Deactivated;

  [SerializeField]
  private TMP_Text _text;

  public void Play(Vector2 at, int value, float duration = DefaultDuration)
  {
    gameObject.SetActive(true);
    _text.text = value.ToString();

    CalculatePositions(at, out Vector2 from, out Vector2 to);
    CreateTweens(from, to, duration);
  }

  private void CalculatePositions(Vector2 at, out Vector2 from, out Vector2 to)
  {
    from = at + AnimationXYShift * Vector2.one;
    to = from + AnimationXYShift * Vector2.one;
  }

  private void CreateTweens(Vector2 from, Vector2 to, float duration)
  {
    transform.DOMove(to, duration).From(from);
    transform.DOScale(AnimationEndScale, duration).From(1f);

    _text.DOFade(0f, duration).From(1f)
      .OnComplete(Deactivate);
  }

  private void Deactivate()
  {
    Deactivated?.Invoke(this);
    gameObject.SetActive(false);
  }    
}
