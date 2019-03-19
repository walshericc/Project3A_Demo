using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisionHandler : MonoBehaviour
{
    public delegate void PlayerDestroyed();
    public static event PlayerDestroyed OnPlayerDestroyed;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Asteroid>() == null)
            return;

        Debug.Log("Player destroyed");
        
        OnPlayerDestroyed?.Invoke();
    }
}
