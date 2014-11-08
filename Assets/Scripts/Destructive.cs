using UnityEngine;
using System.Collections;

public class Destructive : MonoBehaviour {

	void OnTriggerEnter(Collider collider){
			Destroy (collider.gameObject);
	}
}
