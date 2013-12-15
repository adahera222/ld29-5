using System.Collections;
using Annotations;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject Enemy;
    public int EnemyCount = 4;

    public void StartSpawning()
    {
        this.StartCoroutine(this.SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (var i = 0; i < this.EnemyCount; i++) {
            Object.Instantiate(this.Enemy, this.transform.position, Quaternion.identity);

            var t = Random.Range(0.2f, 0.6f);
            yield return new WaitForSeconds(t);
        }

        Object.Destroy(this.gameObject);
    }
}
