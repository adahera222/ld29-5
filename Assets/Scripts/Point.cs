using System;
using Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class Point : MonoBehaviour
{
    public float Velocity;
    public float Variance;

    public float Lifetime;
    private float age;

    [UsedImplicitly] private void Start()
    {
        this.transform.parent = GameObject.Find("Points").transform;
        this.MoveRandomly();
    }

    [UsedImplicitly] private void Update()
    {
        if (this.age > this.Lifetime) {
            GameObject.Destroy(this.gameObject);
        }
        else {
            var t = this.age / this.Lifetime;
            var sprite = (SpriteRenderer)this.renderer;
            var color = sprite.color;
            color.a = Mathf.Lerp(1.0f, 0.0f, t);
            sprite.color = color;
        }

        this.age += Time.deltaTime;
    }

    private void MoveRandomly()
    {
        var x = Random.Range(-1f, 1f);
        var y = Random.Range(-1f, 1f);

        var v = Random.Range(this.Velocity - this.Variance, this.Velocity + this.Variance);
        this.rigidbody2D.velocity = new Vector2(x, y).normalized * v;
    }
}
