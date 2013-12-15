using System.Collections;
using Annotations;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Transform Player;
    public GameManager Manager;

    public void Detonate()
    {
        this.GetComponent<Animator>().SetTrigger("Exploding");
        this.transform.parent = null;
        this.Manager.IsStopped = true;
    }

    // Called by animation event
    [UsedImplicitly] private void DetonationFinished()
    {
        // Reset transform
        this.transform.position = this.Player.position;
        this.transform.parent = this.Player;
        this.transform.localScale = new Vector3(0, 0, 1);

    }

    [UsedImplicitly] private void OnTriggerEnter2D(Collider2D other)
    {
        var enemyScript = other.GetComponent<Enemy>();
        if (enemyScript) {
            enemyScript.Destroy(other.transform.position - this.transform.position);
        }
        else {
            Object.Destroy(other.gameObject);
        }
    }
}
