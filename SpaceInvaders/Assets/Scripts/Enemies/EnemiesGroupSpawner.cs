using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyGroupMovement))]
public class EnemiesGroupSpawner : MonoBehaviour
{
  private readonly Color NormalColor = Color.white;
  private readonly Color ShootingColor = Color.black;

  [Header("Prefabs")]
  [SerializeField]
  private Transform _normalEnemyPrefab;
  [SerializeField]
  private Transform _shootingEnemyPrefab;
  [SerializeField]
  private Projectile _projectilePrefab;

  [Space]
  [SerializeField]
  private Texture2D _levelTexture;

  private List<Transform> _enemyList;
  private ProjectileFactory _projectileFactory;

  private void Awake() => 
    _projectileFactory = new ProjectileFactory(_projectilePrefab);

  private void Start()
  {
    Generate();

    GetComponent<EnemyGroupMovement>().StartMovement(_enemyList);
    GetComponentInParent<EnemiesMonitor>().SetEnemiesList(_enemyList);
  }

  private void Generate()
  {
    int columns = _levelTexture.width;
    int rows = _levelTexture.height;

    _enemyList = new List<Transform>(rows * columns);

    GetStartPositionAndStep(columns, out float spawnPositionStep, out Vector2 spawnStartingPosition);

    SpawnEnemiesGroup(rows, columns, spawnStartingPosition, spawnPositionStep);
  }

  private void SpawnEnemiesGroup(int rows, int columns, Vector2 spawnStartingPosition, float spawnPositionStep)
  {
    for (int x = 0; x < rows; x++)
    {
      for (int y = 0; y < columns; y++)
      {
        Color pixelColor = _levelTexture.GetPixel(y, rows - x - 1);

        Vector2 position = GetEnemyLocalPosition(x, y, spawnStartingPosition, spawnPositionStep);

        SpawnEnemy(pixelColor, position);
      }
    }
  }

  private void SpawnEnemy(Color pixelColor, Vector2 position)
  {
    if (pixelColor == NormalColor)
      Spawn(_normalEnemyPrefab, position);

    if (pixelColor == ShootingColor)
      Spawn(_shootingEnemyPrefab, position)
        .GetComponent<EnemyFire>()
        .SetProjectileSource(_projectileFactory);
  }

  private Transform Spawn(Transform enemyPrefab, Vector2 localPosition)
  {
    Transform enemyTransform = Instantiate(enemyPrefab, transform);
    enemyTransform.localPosition = localPosition;
    _enemyList.Add(enemyTransform);
    return enemyTransform;
  }

  private Vector2 GetEnemyLocalPosition(int row, int column, Vector2 spawnStartingPosition, float spawnPositionStep) =>
   (new Vector2(column, -row) * spawnPositionStep + spawnStartingPosition);

  private void GetStartPositionAndStep(int columns, out float spawnPositionStep, out Vector2 spawnStartingPosition)
  {
    Sprite enemySprite = _normalEnemyPrefab.GetComponent<SpriteRenderer>().sprite;
    float enemySpriteSizeX = enemySprite.rect.size.x / enemySprite.pixelsPerUnit;
    float allEnemiesWigth = enemySpriteSizeX * columns;
    float padding = enemySpriteSizeX * 0.5f;
    allEnemiesWigth += padding * (columns - 1);    

    spawnPositionStep = enemySpriteSizeX + padding;
    spawnStartingPosition = (allEnemiesWigth * 0.5f - padding) * Vector2.left;
  }
}