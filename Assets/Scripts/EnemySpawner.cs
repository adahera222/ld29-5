using System.Collections;
using Annotations;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    private Transform enemyParent;
    public float Cooldown;
    public float Variance = 0.5f;

    [UsedImplicitly] private void Start()
    {
        this.enemyParent = GameObject.Find("Enemies").transform;
        this.StartCoroutine(this.SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true) {
            var enemy = (GameObject)Object.Instantiate(this.Enemy, this.transform.position, Quaternion.identity);
            enemy.transform.parent = this.enemyParent;

            var t = Random.Range(this.Cooldown - this.Variance, this.Cooldown + this.Variance);
            yield return new WaitForSeconds(t);
        }
    }
}
