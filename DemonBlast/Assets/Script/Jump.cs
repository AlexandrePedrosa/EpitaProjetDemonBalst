using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

	public float speed = 5;

	private Rigidbody rb;

	public float jump = 7;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}

	void Update ()
	{
		float moveh = Input.GetAxis ("Horizontal");
		float movev = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveh, 0, movev);
		float h = transform.position.y;
		if (h < 0.2 && Input.GetKeyDown ("space"))
			rb.AddForce (Vector3.up * jump, ForceMode.Impulse);
	}

	private bool IsGrounded()
	{
		
		return false;
	}
		 
	
}
