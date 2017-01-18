using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        var hitedObject = collision.gameObject;
        var health = hitedObject.GetComponent<Health>();
        if(health != null) health.TakeDamage(10);
        Destroy(gameObject);
    }

}
