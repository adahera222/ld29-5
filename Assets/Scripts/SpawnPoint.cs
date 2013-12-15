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
            var enemy = (GameObject)Object.Instantiate(this.Enemy, this.transform.position, Quaternion.identity);

            var torque = Random.Range(-1f, 1f);
            enemy.rigidbody2D.AddTorque(torque);

            var t = Random.Range(0.2f, 0.6f);
            yield return new WaitForSeconds(t);
        }

        Object.Destroy(this.gameObject);
    }
}
