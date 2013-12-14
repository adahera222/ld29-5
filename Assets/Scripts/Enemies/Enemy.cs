using System.Collections;
using Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject ChildType;
    public Vector2[] Positions;
    public float[] Angles;

    protected Player Player;
    private bool isInvincible;

    [UsedImplicitly] private void Start()
    {
        this.Player = Object.FindObjectOfType<Player>();
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

    private void Destroy(Vector2 normal)
    {
        // Spawn child objects
        for (var i = 0; i < this.Positions.Length; i++) {
            var p = (Vector3)this.Positions[i];
            var a = Quaternion.Euler(new Vector3(0, 0, this.Angles[i]));
            var child = (GameObject)Object.Instantiate(this.ChildType, this.transform.position + p, a);

            // Add a force in the direction of the colliding object
            var f = Random.Range(100f, 200f);
            child.rigidbody2D.AddForce(normal * f);

            // Add a random torque
            var torque = Random.Range(0.1f, 0.5f);
            child.rigidbody2D.AddTorque(torque);

            // Mark the child object as invincible
            var childEnemy = child.GetComponent<Enemy>();
            childEnemy.isInvincible = true;
            childEnemy.StartCoroutine(childEnemy.WaitDestroy());
        }

        Object.Destroy(this.gameObject);
    }

    private IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(1f);
        this.isInvincible = false;
    }
}
