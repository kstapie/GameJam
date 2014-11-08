using UnityEngine;
using System.Collections;

public class SphereScript : MonoBehaviour 
{
    private isOutOfPool = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Outside of pool, destroy
    void OnTriggerStay(Collider other)
    {
        // Check if the other object is a Pool
        PoolScript pool = other.GetComponent<PoolScript>();
        if (pool != null)
        {
            DestroyImmediate(gameObject);
        }
    }
}
