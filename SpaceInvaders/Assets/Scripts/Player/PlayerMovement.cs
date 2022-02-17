using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IInputProvider))]
public class PlayerMovement : MonoBehaviour
{
  [SerializeField]
  private float _moveSpeed;

  private IInputProvider _inputProvider;
  private Rigidbody2D _rigidbody;
  private Vector2 _movementVector;
  private float _xMin, _xMax;

  private void Start()
  {
    _inputProvider = GetComponent<IInputProvider>();
    _rigidbody = GetComponent<Rigidbody2D>();
    _movementVector = transform.position;

    SetUpMoveBoundaries();
  }

  private void FixedUpdate() => 
    Move();

  private void Move()
  {
    float deltaX = _inputProvider.Horizontal * _moveSpeed * Time.deltaTime;
    _movementVector.x = Mathf.Clamp(transform.position.x + deltaX, _xMin, _xMax);
    _rigidbody.MovePosition(_movementVector);
  }

  private void SetUpMoveBoundaries()
  {
    Sprite playerSprite = GetComponent<SpriteRenderer>().sprite;
    float xPadding = playerSprite.rect.size.x / playerSprite.pixelsPerUnit / 2f;

    Camera gameCamera = Camera.main;
    _xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
    _xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
  }
}
