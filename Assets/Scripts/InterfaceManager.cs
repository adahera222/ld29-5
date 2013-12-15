using Annotations;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public GameManager Manager;
    public tk2dTextMesh PointsMesh;

    public Player Player;
    public tk2dTextMesh BombMesh;

    [UsedImplicitly] private void LateUpdate()
    {
        this.PointsMesh.text = "X " + Manager.Points;
        this.BombMesh.text = "X " + (this.Player.Bomb ? 1 : 0);
    }
}
