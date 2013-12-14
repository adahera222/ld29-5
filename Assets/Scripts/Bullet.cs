using Annotations;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [UsedImplicitly] private void OnTriggerEnter2D()
    {
        Object.Destroy(this.gameObject);
    }
}
