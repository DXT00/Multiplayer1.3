using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


	
	void OnCollisionEnter(Collision collision)
    {
        GameObject hit= collision.gameObject;
        PlayerHealth health = hit.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage();
        }

        Destroy(gameObject);
    }
}
