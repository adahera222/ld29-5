using System.Collections;
using Annotations;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Enemy;
    public float Cooldown;
    public float Buffer = 0.1f;

    [UsedImplicitly] private void Start()
    {
        this.StartCoroutine(this.SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true) {
            const float ppm = 100f;
            var boundsX = (Screen.width / 2f) / ppm;
            var boundsY = (Screen.height / 2f) / ppm;

            var x = Random.Range(-boundsX + this.Buffer, boundsX - this.Buffer);
            var y = Random.Range(-boundsY + this.Buffer, boundsY - this.Buffer);

            Object.Instantiate(this.Enemy, new Vector3(x, y, 0), Quaternion.identity);

            yield return new WaitForSeconds(this.Cooldown);
        }
    }
}
