using UnityEngine;
using System.Collections;

public class SphereScript : MonoBehaviour 
{
    public Transform bound1;
    public Transform bound2;
    public Transform bound3;
    public Transform bound4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // If ball touch bounds, destroy
    void OnTriggerEnter(Collider other)
    {
        // Check if this is the boundary
        if ((other.transform == bound1)||(other.transform == bound2)||(other.transform == bound3)||(other.transform == bound4))
        {
            Destroy(gameObject);
        }
    }
}
