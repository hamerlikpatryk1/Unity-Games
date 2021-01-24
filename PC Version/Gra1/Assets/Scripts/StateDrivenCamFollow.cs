using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Mirror;

public class StateDrivenCamFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var vcam = GetComponent<CinemachineStateDrivenCamera>();
        vcam.m_AnimatedTarget = GameObject.Find("Local").GetComponent<Animator>();
        vcam.m_Follow = GameObject.Find("Local").GetComponent<Transform>();
    }
}
