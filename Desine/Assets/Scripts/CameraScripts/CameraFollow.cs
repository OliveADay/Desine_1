using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    Transform target;
    public CinemachineVirtualCamera vcam;
    public string playerName;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(playerName).transform;
        vcam.Follow = target;
    }
    private void FixedUpdate()
    {

    }
}
