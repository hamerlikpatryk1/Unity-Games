using System;
using UnityEngine;
using Mirror;

    public class Camera2DFollow : NetworkBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            if (isLocalPlayer)
            {
                GameObject.Find("Main Camera").gameObject.transform.parent = this.transform;
            }
        }


        // Update is called once per frame
        private void Update()
        {
            if (isLocalPlayer)
            {
                GameObject.Find("Main Camera").gameObject.transform.parent = this.transform;
            }
        }
    }
