using Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Velocity;
    public Weapon EquippedWeapon;

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
    }
}
