using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMultiplayerScene : MonoBehaviour {

	public void LoadByIndex(string name)
	{
		//canvas.enabled = true;
		SceneManager.LoadSceneAsync(name);
	}

	public void EnableCanvas(Canvas canvas)
	{
		canvas.enabled = true;
	}
}