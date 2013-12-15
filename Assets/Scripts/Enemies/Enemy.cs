using System.Collections;
using Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject ChildType;
    public Vector2[] Positions;
    public float[] Angles;

    protected Player Player;
    private bool isInvincible = true;
    private Transform enemyParent;

    [UsedImplicitly] private void Start()
    {
        this.Player = Object.FindObjectOfType<Player>();
        this.enemyParent = GameObject.Find("Enemies").transform;

        // Make the enemy initially invincible
        this.StartCoroutine(this.WaitDestroy(1f));

        // Initial force toward player
        var d = (this.Player.transform.position - this.transform.position).normalized;
        var f = Random.Range(20f, 40f);
        this.rigidbody2D.AddForce(d * f);
    }

    [UsedImplicitly] private void Update()
    {
        if (this.isInvincible) return;
        this.Move();
    }

    [UsedImplicitly] private void OnCollisionEnter2D(Collision2D other)
    {
        if (this.isInvincible || other.gameObject.name == "Wall") return;
        this.Destroy(other.gameObject.rigidbody2D.velocity.normalized);
    }

    [UsedImplicitly] private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.isInvincible || other.gameObject.name == "Wall") return;
        this.Destroy(other.rigidbody2D.velocity.normalized);
    }

    internal virtual void Move()
    {
    }

    public void Destroy(Vector2 normal)
    {
        // Spawn child objects
        for (var i = 0; i < this.Positions.Length; i++) {
            var p = (Vector3)this.Positions[i];
            var a = Quaternion.Euler(new Vector3(0, 0, this.Angles[i]));
            var child = (GameObject)Object.Instantiate(this.ChildType, this.transform.position + p, a);
            child.transform.parent = this.enemyParent;

            var d = (this.transform.position - child.transform.position).normalized + (Vector3)normal;
            var f = Random.Range(80f, 150f);
            child.rigidbody2D.AddForce(d * f);

            // Add a random torque
            var torque = Random.Range(0.1f, 0.5f);
            child.rigidbody2D.AddTorque(torque);

            // Mark the child object as invincible
            var childEnemy = child.GetComponent<Enemy>();
            childEnemy.isInvincible = true;
            childEnemy.StartCoroutine(childEnemy.WaitDestroy(1f));
        }

        Object.Destroy(this.gameObject);
    }

    private IEnumerator WaitDestroy(float t)
    {
        yield return new WaitForSeconds(t);
        this.isInvincible = false;
    }
}
