using UnityEngine;
using UnityEngine.Pool;

public class ProjectileFactory
{
  private readonly Projectile _projectilePrefab;
  private readonly IObjectPool<Projectile> _projectilePool;

  private Transform _parentTransform;

  public ProjectileFactory(Projectile projectile, Transform parent = null)
  {
    _projectilePrefab = projectile;
    _parentTransform = parent;
    _projectilePool = new ObjectPool<Projectile>(Create, actionOnDestroy: OnDestruction);
  }

  public Projectile GetAt(Vector2 position)
  {
    Projectile projectile = Get();
    projectile.transform.position = position;
    return projectile;
  }

  public Projectile Get()
  {
    Projectile projectile = _projectilePool.Get();
    projectile.gameObject.SetActive(true);
    return projectile;
  }

  private void CreateParentObjectOnScene()
  {
    _parentTransform = new GameObject(_projectilePrefab.name + "s").transform;
    _parentTransform.position = Vector3.zero;
  }

  private Projectile Create()
  {
    if (_parentTransform == null)
      CreateParentObjectOnScene();

    Projectile projectile = Object.Instantiate(_projectilePrefab, _parentTransform);
    projectile.ReleaseEvent += OnReleaseRequest;
    return projectile;
  }

  private void OnDestruction(Projectile projectile) =>
    projectile.ReleaseEvent -= OnReleaseRequest;

  private void OnReleaseRequest(Projectile projectile) =>
    _projectilePool.Release(projectile);
}
