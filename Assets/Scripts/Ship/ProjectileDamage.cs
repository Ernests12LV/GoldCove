using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damageAmount = 20;

    private void OnCollisionEnter(Collision collision)
    {
        ShipHealth health = collision.gameObject.GetComponent<ShipHealth>();
        if (health != null)
        {
            health.TakeDamage(damageAmount);
        }

        // Add any other collision handling logic or effects

        Destroy(gameObject);
    }
}
