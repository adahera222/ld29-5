using System.Collections;
using Annotations;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public float Cooldown;
    public float Variance = 0.5f;

    [UsedImplicitly] private void Start()
    {
        this.StartCoroutine(this.SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true) {
            //const float ppm = 100f;
            //var boundsX = (Screen.width / 2f) / ppm;
            //var boundsY = (Screen.height / 2f) / ppm;

            //var x = Random.Range(-boundsX + this.Buffer, boundsX - this.Buffer);
            //var y = Random.Range(-boundsY + this.Buffer, boundsY - this.Buffer);

            Object.Instantiate(this.Enemy, this.transform.position, Quaternion.identity);

            var t = Random.Range(this.Cooldown - this.Variance, this.Cooldown + this.Variance);
            yield return new WaitForSeconds(t);
        }
    }
}
