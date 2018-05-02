using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightStick : MonoBehaviour {
	
	private Vector3 lastPos;
	// Use this for initialization
	void Start () {
		lastPos = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = lastPos;
		lastPos = transform.position;
	}
}
