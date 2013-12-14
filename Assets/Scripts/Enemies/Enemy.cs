using System.Collections;
using Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] Children;
    protected Player Player;
    private bool isInvincible;

    //public float MaxDistance;
    //public AnimationCurve MovementCurve;
    //public float Velocity;

    [UsedImplicitly] private void Start()
    {
        this.Player = Object.FindObjectOfType<Player>();
    }

    [UsedImplicitly] private void Update()
    {
        if (this.isInvincible) return;
        this.Move();
        //this.rigidbody2D.velocity = new Vector2();

        //var d = this.player.transform.position - this.transform.position;
        //var t = d.magnitude / this.MaxDistance;
        //var v = this.Velocity * this.MovementCurve.Evaluate(t);

        //this.transform.position += (d.normalized * v) * Time.deltaTime;
    }

    [UsedImplicitly] private void OnCollisionEnter2D(Collision2D other)
    {
        if (this.isInvincible) return;
        this.Destroy(other.gameObject.rigidbody2D.velocity.normalized);
    }

    [UsedImplicitly] private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.isInvincible) return;
        this.Destroy(other.rigidbody2D.velocity.normalized);
    }

    internal virtual void Move()
    {
        
    }

    private void Destroy(Vector2 normal)
    {
        foreach (var child in this.Children) {
            child.parent = null;
            child.gameObject.SetActive(true);

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
