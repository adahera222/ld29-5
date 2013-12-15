using System.Collections;
using Annotations;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject Enemy;
    public int EnemyCount = 4;

    public float AnimationDuration;
    private float currentDuration;

    [UsedImplicitly] private void Start()
    {
        this.StartCoroutine(this.ShowAnimation());
    }

    public void StartSpawning()
    {
        this.StartCoroutine(this.SpawnEnemies());
    }

    IEnumerator ShowAnimation()
    {
        while (this.AnimationDuration > this.currentDuration) {
            var t = this.currentDuration / this.AnimationDuration;
            this.transform.localScale = Vector3.Lerp(new Vector3(), new Vector3(1, 1, 1), t);

            // Animate alpha
            var sprite = (SpriteRenderer) this.renderer;
            var color = sprite.color;
            color.a = Mathf.Lerp(0f, 1f, t);
            sprite.color = color;

            this.currentDuration += Time.deltaTime;
            yield return null;
        }
        this.currentDuration = 0;
    }

    IEnumerator HideAnimation()
    {
        while (this.AnimationDuration > this.currentDuration) {
            var t = this.currentDuration / this.AnimationDuration;
            this.transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(), t);

            // Animate alpha
            var sprite = (SpriteRenderer) this.renderer;
            var color = sprite.color;
            color.a = Mathf.Lerp(1f, 0f, t);
            sprite.color = color;

            this.currentDuration += Time.deltaTime;
            yield return null;
        }
        Object.Destroy(this.gameObject);
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(1f);

        for (var i = 0; i < this.EnemyCount; i++) {
            var enemy = (GameObject)Object.Instantiate(this.Enemy, this.transform.position, Quaternion.identity);

            var torque = Random.Range(-1f, 1f);
            enemy.rigidbody2D.AddTorque(torque);

            var t = Random.Range(0.2f, 0.6f);
            yield return new WaitForSeconds(t);
        }

        this.StartCoroutine(this.HideAnimation());
    }
}
