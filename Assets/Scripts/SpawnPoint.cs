using System.Collections;
using Annotations;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject Enemy;
    private Transform enemyParent;
    public int EnemyCount = 4;

    [UsedImplicitly] private void Start()
    {
        this.enemyParent = GameObject.Find("Enemies").transform;
    }

    public void StartSpawning()
    {
        this.StartCoroutine(this.SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (var i = 0; i < this.EnemyCount; i++) {
            var enemy = (GameObject)Object.Instantiate(this.Enemy, this.transform.position, Quaternion.identity);
            enemy.transform.parent = this.enemyParent;

            var t = Random.Range(0.2f, 0.6f);
            yield return new WaitForSeconds(t);
        }

        Object.Destroy(this.gameObject);
    }
}
