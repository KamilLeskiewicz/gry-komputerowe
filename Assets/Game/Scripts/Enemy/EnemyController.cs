using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public int enemyCount = 5;
    public Vector2 spawnAreaMin = new Vector2(-10, -10);
    public Vector2 spawnAreaMax = new Vector2(10, 10);

    void Start()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.AddComponent<EnemyMovement>();
    }
}

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    private Vector2 targetPosition;
    public Vector2 moveAreaMin = new Vector2(-10, -10);
    public Vector2 moveAreaMax = new Vector2(10, 10);

    void Start()
    {
        SetNewTargetPosition();
        StartCoroutine(ChangeDirectionRoutine());
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if ((Vector2)transform.position == targetPosition)
        {
            SetNewTargetPosition();
        }
    }

    void SetNewTargetPosition()
    {
        targetPosition = new Vector2(
            Random.Range(moveAreaMin.x, moveAreaMax.x),
            Random.Range(moveAreaMin.y, moveAreaMax.y)
        );
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            SetNewTargetPosition();
        }
    }
}
