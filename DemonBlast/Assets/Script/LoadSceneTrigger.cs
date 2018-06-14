using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneTrigger : MonoBehaviour {

	[SerializeField]
	private string sceneName;
	[SerializeField]
	private GameObject loadScreen;

	private Canvas canvas;

	private void Start()
	{
		canvas = loadScreen.GetComponent<Canvas> ();
	}

	private void OnTriggerEnter(Collider other)
	{
		var a = other.gameObject.GetComponent<CharacterControllerLogic>();
		if (a != null)
		{
			loadScene();
		}
	}

	private void loadScene()
	{
		canvas.enabled = true;
		SceneManager.LoadSceneAsync(sceneName);
	}
}
