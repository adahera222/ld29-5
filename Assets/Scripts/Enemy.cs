using Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player player;

    [UsedImplicitly] private void Start()
    {
        this.player = Object.FindObjectOfType<Player>();
    }

    [UsedImplicitly] private void Update()
    {
        var target = Vector3.Lerp(this.transform.position, this.player.transform.position, Time.deltaTime);
        this.transform.position = target;
    }

    [UsedImplicitly] private void OnCollisionEnter2D()
    {
        Object.Destroy(this.gameObject);
    }

    [UsedImplicitly] private void OnTriggerEnter2D()
    {
        Object.Destroy(this.gameObject);
    }
}
