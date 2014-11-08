using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{
	public float explodeRadius;
	public float explodePower;
	public Vector3 shotPos;
	public float startTime;
	public float endTime;

    public bool isDrawPowerInd = false;
  
	// Use this for initialization
	void Start () {
		explodeRadius = 1f;
		explodePower = 5.0f;
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			shotPos = Input.mousePosition;
			shotPos = Camera.main.ScreenToWorldPoint(shotPos);
			shotPos.z = 0;
			startTime = Time.time;

            isDrawPowerInd = true;
		}

		if(Input.GetMouseButtonUp (0))
		{
			endTime = Time.time;
			explodePower = (endTime - startTime)*5;

			Collider[] colliders = Physics.OverlapSphere(shotPos, explodeRadius);
			
			foreach (Collider collider in colliders)
			{
				if (collider && collider.rigidbody && !IsInside(collider ,shotPos, explodeRadius) /*&& !collider.bounds.Contains(mousePos)*/)
				{
					Vector3 ray = collider.transform.position - shotPos;
					ray.z=0;
					Vector3 rayDirection = ray.normalized;
					float rayDistance = ray.magnitude;
					
					collider.rigidbody.AddForce(rayDirection*explodePower/rayDistance, ForceMode.Impulse);
					
					//collider.rigidbody.AddExplosionForce(explodePower, mousePos, explodeRadius, 0.0f, ForceMode.Impulse);
				}
			}

            isDrawPowerInd = false;
		}


		if(Input.GetAxis("Mouse ScrollWheel") != 0){
			explodeRadius += Input.GetAxis("Mouse ScrollWheel");
		}
		if (explodeRadius < 1) {
			explodeRadius = 1;
		}
		if (explodeRadius > 5) {
			explodeRadius = 5;
		}

		if (Input.GetKeyDown ("r"))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	
    // GUI
    void OnGUI()
    {
        // Power indicator
        if (isDrawPowerInd)
        {
            GUI.Box(new Rect(125, 585, (Time.time - startTime + 1)*50, 25),"");
        }
		float diameter = 2 * explodeRadius;
		//GUI.DrawTexture(Rect(Event.current.mousePosition.x-explodeRadius, Event.current.mousePosition.y-explodeRadius, diameter, diameter), Texture);
		//GUI.DrawTexture (Rect (100, 100, 50, 50), ripple2_KG);
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
