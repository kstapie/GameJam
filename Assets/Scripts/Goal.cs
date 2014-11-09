using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	void OnTriggerEnter(Collider collider)
    {
		if(collider.Equals(GameObject.Find("Player").collider)){
			Time.timeScale=0;
			Debug.Log ("win");
			/* FIXME
			 * Add GUI element to game (like popup screen) telling player their score for the level
			 * ...and has buttons to take player to main menu or next level...
			 */
		}

        // If it's a DeathBall, lose
        if (collider.gameObject.name == "DeathBall")
        {
            Time.timeScale = 0;
            Debug.Log("lose");
        }
	}
}
