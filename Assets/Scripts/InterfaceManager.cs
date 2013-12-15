using Annotations;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public GameManager Manager;
    public tk2dTextMesh PointsMesh;

    [UsedImplicitly] private void LateUpdate()
    {
        this.PointsMesh.text = "X " + Manager.Points;
    }
}
