using System.Collections;
using Annotations;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    private Transform enemyParent;
    public int EnemyCount = 4;
    public int Variance = 1;

    [UsedImplicitly] private void Start()
    {
        this.enemyParent = GameObject.Find("Enemies").transform;

        var count = Random.Range(this.EnemyCount - this.Variance, this.EnemyCount + this.Variance);
        this.StartCoroutine(this.SpawnEnemies(count));
    }

    IEnumerator SpawnEnemies(int enemyCount)
    {
        for (var i = 0; i < enemyCount; i++) {
            var enemy = (GameObject)Object.Instantiate(this.Enemy, this.transform.position, Quaternion.identity);
            enemy.transform.parent = this.enemyParent;

            var t = Random.Range(0.2f, 0.6f);
            yield return new WaitForSeconds(t);
        }

        Object.Destroy(this.gameObject);
    }
}
