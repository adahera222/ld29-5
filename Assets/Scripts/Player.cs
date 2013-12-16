using System.Collections;
using Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Velocity;
    public Weapon EquippedWeapon;
    public Bomb Bomb;
    public GameObject Explosion;
    public GameManager Manager;

    [UsedImplicitly] private void Update()
    {
        // Handle movement
        var v = new Vector2();
        if (Input.GetKey("w")) {
            v += Vector2.up * this.Velocity;
        }
        if (Input.GetKey("s")) {
            v += -Vector2.up * this.Velocity;
        }
        if (Input.GetKey("a")) {
            v += -Vector2.right * this.Velocity;
        }
        if (Input.GetKey("d")) {
            v += Vector2.right * this.Velocity;
        }

        this.rigidbody2D.velocity = v;

        // Handle shooting
        if (Input.GetButton("Fire1")) {
            var pm = Input.mousePosition;
            pm.z = 0;
            var d = pm - Camera.main.WorldToScreenPoint(this.transform.position);
            this.EquippedWeapon.Fire(d.normalized);
        }

        if ((Input.GetButtonDown("Fire2") || Input.GetKeyDown("space")) && this.Bomb) {
            this.Bomb.Detonate();
            this.Bomb = null;
        }
    }

    [UsedImplicitly] private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy") {
            this.StartCoroutine(this.Die());
        }
    }

    private IEnumerator Die()
    {
        this.Explosion.transform.parent = null;
        this.Explosion.particleSystem.Play();
        this.Explosion.audio.Play();
        this.renderer.enabled = false;
        this.collider2D.enabled = false;
        yield return new WaitForSeconds(this.Explosion.particleSystem.duration);
        Application.LoadLevel("Menu");
    }
}
