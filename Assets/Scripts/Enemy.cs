using System.Collections;
using Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] Children;
    private Player player;
    private bool isInvincible;

    [UsedImplicitly] private void Start()
    {
        this.player = Object.FindObjectOfType<Player>();
    }

    [UsedImplicitly] private void Update()
    {
        if (this.isInvincible) return;
        this.rigidbody2D.velocity = new Vector2();
        var target = Vector3.Lerp(this.transform.position, this.player.transform.position, Time.deltaTime);
        this.transform.position = target;
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
