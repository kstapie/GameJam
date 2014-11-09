using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	public int scoreBest1;
	public int scoreBest2;

	void Start()
	{
		IniFile ini=new IniFile("save.ini");
		scoreBest1 = ini.get ("level_1",0);
		scoreBest2 = ini.get ("level_2",0);
		ini.save ("save.ini");
	}

	void OnGUI () {
		// Make a background box
        GUI.Box(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 25, 165, 100), "Loader Menu");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 120, 20), "Level 1 (High: "+scoreBest1.ToString()+")"))
        {
			Application.LoadLevel("level_1");
		}
		
		// Make the second button.
		if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 25, 120, 20), "Level 2 (High: "+scoreBest2.ToString()+")"))
        {
			Application.LoadLevel(2);
		}
	}
}