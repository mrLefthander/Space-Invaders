using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IInputProvider))]
public class PlayerFire : MonoBehaviour
{
  [Header("Player Attack Values")]
  [SerializeField]
  private float _fireCooldown;
  [SerializeField]
  private Projectile _projectilePrefab;

  private ProjectileFactory _projectileFactory;
  private IInputProvider _inputProvider;
  private bool _canShoot = true;

  private void Awake()
  {
    _projectileFactory = new ProjectileFactory(_projectilePrefab);
    _inputProvider = GetComponent<IInputProvider>();
  }

  private void OnEnable()
  {
    PauseService.Paused += OnPause;
    _inputProvider.FireEvent += OnFireInput;    
  }

  private void OnDisable()
  {
    PauseService.Paused -= OnPause;
    _inputProvider.FireEvent -= OnFireInput;    
  }

  private void OnPause(bool isPaused) => 
    _canShoot = !isPaused;

  private void OnFireInput()
  {
    if (_canShoot == false) return;

    Fire();
  }

  private void Fire()
  {
    ShootProjectile();
    _canShoot = false;    
    StartCoroutine(StartCooldown());
  }

  private void ShootProjectile() => 
    _projectileFactory.GetAt(transform.position);

  private IEnumerator StartCooldown()
  {
    yield return new WaitForSeconds(_fireCooldown);
    _canShoot = true;
  }
}
