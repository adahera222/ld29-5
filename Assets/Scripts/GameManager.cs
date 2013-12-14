using Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [UsedImplicitly] private void Start()
    {
        Application.targetFrameRate = 60;
    }
}
