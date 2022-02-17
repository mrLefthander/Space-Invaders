using System.Collections;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
  [SerializeField]
  private float _fireCooldown = 2f;

  private ProjectileFactory _factory;

  private void OnEnable() => 
    StartCoroutine(ShootWithColldown(_fireCooldown));

  private IEnumerator ShootWithColldown(float cooldown)
  {
    while (true)
    {
      yield return new WaitForSeconds(cooldown);
      Fire();
    }
  }

  public void SetProjectileSource(ProjectileFactory factory) => 
    _factory = factory;

  private void Fire() => 
    _factory.GetAt(transform.position);
}