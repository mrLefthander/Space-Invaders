using UnityEngine;
using UnityEngine.Pool;

public class FloatingTextFactory
{
  private readonly FloatingText _floatingText;
  private readonly IObjectPool<FloatingText> _pool;
  private Transform _parentTransform;

  public FloatingTextFactory(FloatingText floatingText, Transform parent = null)
  {
    _floatingText = floatingText;
    _parentTransform = parent;
    _pool = new ObjectPool<FloatingText>(Create, actionOnDestroy: OnDestruction);
  }

  public FloatingText Get() => 
    _pool.Get();

  private FloatingText Create()
  {
    if (_parentTransform == null)
      CreateParentObjectOnScene();

    FloatingText floatingText = Object.Instantiate(_floatingText, _parentTransform);
    floatingText.Deactivated += OnReleaseRequest;
    return floatingText;
  }

  private void OnReleaseRequest(FloatingText floatingText) => 
    _pool.Release(floatingText);

  private void OnDestruction(FloatingText floatingText) =>
    floatingText.Deactivated -= OnReleaseRequest;

  private void CreateParentObjectOnScene()
  {
    _parentTransform = new GameObject(_floatingText.name + "s").transform;
    _parentTransform.position = Vector3.zero;
  }
}
