using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AcidBehavior : NetworkBehaviour
{
    [SerializeField] float scrollRate = 0.2f;


    void Update()
    {
        if (GameObject.Find("Local"))
        { 
            float yMove = scrollRate * Time.deltaTime;
            transform.Translate(new Vector2(0f, scrollRate * Time.deltaTime));
        }
    }
}
