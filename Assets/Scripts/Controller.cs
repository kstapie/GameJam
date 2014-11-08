using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{
	public float explodeRadius = 1f;
	public float explodePower = 5.0f;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);
			mousePos.z = 0;
			
			Collider[] colliders = Physics.OverlapSphere(mousePos, explodeRadius);
			
			foreach (Collider collider in colliders)
			{
				if (collider && collider.rigidbody && !IsInside(collider ,mousePos, explodeRadius) /*&& !collider.bounds.Contains(mousePos)*/)
				{
					Vector3 ray = collider.transform.position - mousePos;
					ray.z=0;
					Vector3 rayDirection = ray.normalized;
					float rayDistance = ray.magnitude;
					
					collider.rigidbody.AddForce(rayDirection*explodePower/rayDistance, ForceMode.Impulse);
					
					//collider.rigidbody.AddExplosionForce(explodePower, mousePos, explodeRadius, 0.0f, ForceMode.Impulse);
				}
			}
		}
		if (Input.GetKeyDown ("r"))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	
	//function from DrakharStudio http://answers.unity3d.com/questions/163864/test-if-point-is-in-collider.html
	static public bool IsInside ( Collider test, Vector3 point, float explodeRadius)
	{
		Vector3    center;
		Vector3    direction;
		Ray        ray;
		RaycastHit hitInfo;
		bool       hit;
		
		// Use collider bounds to get the center of the collider. May be inaccurate
		// for some colliders (i.e. MeshCollider with a 'plane' mesh)
		center = test.bounds.center;
		
		// Cast a ray from point to center
		direction = center - point;
		ray = new Ray(point, direction);
		
		hit = test.Raycast(ray, out hitInfo, direction.magnitude);
		
		// If we hit the collider, point is outside. So we return !hit
		return !hit;
	}
	
}
