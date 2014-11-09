using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour 
{
	public class Ripple {
		public Vector3 shotPos;
		public float explodeRadius;
		public float explodePower;
		public float rippleRadius;

		public Ripple(Vector3 shotP, float explodeR, float explodeP) {
			rippleRadius = 0.0f;
			shotPos = shotP;
			explodePower = explodeP;
			explodeRadius = explodeR;
		}
	}

	public float explodeRadius;
	public float explodePower;
	public Vector3 shotPos;
	public Vector3 ripplePos;
	public float startTime;
	public float endTime;
    public bool isDrawPowerInd = false;
	public Texture tex;

	
	public int scoreBest = 0;
	public int turn = 0;
	public int par = 10;


	public List<Ripple> ripples;



	public void Win()
	{
		if ((par - turn) > scoreBest) 
		{
			IniFile ini=new IniFile("save.ini");
			ini.set (Application.loadedLevelName,par - turn);
			ini.save ("save.ini");
		}
	}

	// Use this for initialization
	void Start () {
		explodeRadius = 1f;
		explodePower = 5.0f;
		Time.timeScale = 1;

		IniFile ini=new IniFile("save.ini");
		scoreBest = ini.get (Application.loadedLevelName,0);
		ini.save ("save.ini");

		ripples = new List<Ripple> ();	
		
	}
	

	// Update is called once per frame
	void Update () 
	{

		if (Input.GetMouseButtonDown (0)) 
		{

			startTime = Time.time;
            isDrawPowerInd = true;
		}

		if(Input.GetMouseButtonUp (0))
		{
			endTime = Time.time;
			explodePower = (endTime - startTime)*5;
			isDrawPowerInd = false;

			shotPos = Input.mousePosition;
			shotPos = Camera.main.ScreenToWorldPoint(shotPos);
			shotPos.z = 0;

			ripplePos = Input.mousePosition;
			ripplePos.z = 0;
			ripplePos.y = Mathf.Abs (ripplePos.y - Screen.height);
			ripples.Add(new Ripple(ripplePos, explodeRadius, explodePower));

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
			turn++;
			if (turn>par) turn = par;

			          
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
		float diameter = (2 * explodeRadius)*50;

		GUI.DrawTexture(new Rect(Event.current.mousePosition.x - diameter/2, Event.current.mousePosition.y - diameter/2, diameter, diameter), tex,ScaleMode.StretchToFill);


		// Par / Best par
		GUI.Label (new Rect (25, 25, 300, 50), "Score: " + (par-turn).ToString ());
		GUI.Label (new Rect (25, 50, 300, 50), "Best score: " + (scoreBest).ToString ());
		GUI.Label (new Rect (25, 70, 300, 50), "Par: " + par.ToString());

		foreach (Ripple ripple in ripples) {
			if (ripple.rippleRadius < ripple.explodeRadius) {
				ripple.rippleRadius += (ripple.explodeRadius / 100);
				float xPos = ripple.shotPos.x;
				float yPos = ripple.shotPos.y;
				float rad = ripple.rippleRadius * 50;
				Rect rect = new Rect(xPos - rad, yPos - rad, rad*2, rad*2);
				GUI.DrawTexture( rect, tex );
			}
			else
			{
				//ripples.Remove (ripple);
			}
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
