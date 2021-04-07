using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBorder : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collided = collision.gameObject;
        if(collided.GetComponent<Projectile>())
        {
            Destroy(collided);
        }
    }
}
