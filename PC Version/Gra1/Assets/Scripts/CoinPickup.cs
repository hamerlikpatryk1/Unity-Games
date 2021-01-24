using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CoinPickup : NetworkBehaviour
{
    [SerializeField] AudioClip coinPickUp;
    [SerializeField] int pointsForCoinPickup = 100; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinPickUp, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
        NetworkServer.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
