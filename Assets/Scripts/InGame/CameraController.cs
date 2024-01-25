using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Update()
    {
        transform.position = PlayerManager.GetPosition() + new Vector3(0, 0, -10);
    }
}
