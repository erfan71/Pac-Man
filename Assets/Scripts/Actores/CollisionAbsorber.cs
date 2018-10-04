using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAbsorber : MonoBehaviour {

    public System.Action<GameObject> onPickupHit;
    public System.Action<GameObject> onPointHit;
    public System.Action<GameObject> onEnemyHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Point")
        {
            if (onPointHit != null)
            {
                onPointHit(collision.gameObject);
            }
        }
        else if (collision.tag == "Pickup")
        {
            if (onPickupHit != null)
            {
                onPickupHit(collision.gameObject);
            }
        }
        else if (collision.tag == "Enemy")
        {
            if (onEnemyHit != null)
            {
                onEnemyHit(collision.gameObject);
            }
        }
    }
}
