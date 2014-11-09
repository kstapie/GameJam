using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	public int scoreBest1;
	public int scoreBest2;
	public int scoreBest3;
	public int scoreBest4;
	public int scoreBest5;

	void Start()
	{
		IniFile ini=new IniFile("save.ini");
		scoreBest1 = ini.get ("level_1",0);
		scoreBest2 = ini.get ("level_2",0);
		scoreBest3 = ini.get ("level_3",0);
		scoreBest4 = ini.get ("level_4",0);
		scoreBest5 = ini.get ("level_5",0);
		ini.save ("save.ini");
	}

	void OnGUI () {
		// Make a background box
        GUI.Box(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 25, 165, 150), "Ripple");
		
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

		// Make the third button.
		if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 50, 120, 20), "Level 3 (High: "+scoreBest3.ToString()+")"))
		{
			Application.LoadLevel(3);
		}

		// Make the fourth button.
		if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 75, 120, 20), "Level 4 (High: "+scoreBest4.ToString()+")"))
		{
			Application.LoadLevel(4);
		}
		
		// Make the fifth button.
		if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 100, 120, 20), "Level 5 (High: "+scoreBest5.ToString()+")"))
		{
			Application.LoadLevel(5);
		}
	}
}