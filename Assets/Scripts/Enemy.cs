using Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [UsedImplicitly] private void OnCollisionEnter2D()
    {
        Object.Destroy(this.gameObject);
    }

    [UsedImplicitly] private void OnTriggerEnter2D()
    {
        Object.Destroy(this.gameObject);
    }
}
