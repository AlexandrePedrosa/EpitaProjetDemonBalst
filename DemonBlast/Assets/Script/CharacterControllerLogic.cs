﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerLogic : MonoBehaviour {

	[SerializeField]
	private float turnSpeed = 3.0f;
	[SerializeField]
	private Camera cam = null;


	private Animator animator;
	private float speed = 0.0f;
	private float sprintSpeed = 0.0f;
	private float turn = 0.0f;
	private float pivotAngle = 0.0f;
	private float horizontal = 0.0f;
	private float vertical = 0.0f;
	private Vector3 moveDirection = new Vector3();
 
	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator> ();
		if (animator.layerCount >= 2) 
		{
			animator.SetLayerWeight (1, 1);
		}
	}

	void FixedUpdate()
	{
		ApplyExtraRotation();
	}

	// Update is called once per frame
	void Update ()
	{
		if (animator)
		{
			
			horizontal = Input.GetAxis ("Horizontal");
			vertical = Input.GetAxis ("Vertical");
			moveDirection = StickToWorldSpace (this.transform, cam.transform);
			Debug.DrawRay (new Vector3 (this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z), moveDirection, Color.magenta);
			moveDirection = transform.InverseTransformDirection(moveDirection);

			turn = Mathf.Atan2 (moveDirection.x, moveDirection.z);

			animator.SetBool ("Jump", Input.GetButton ("Jump"));

			sprintSpeed = Mathf.Lerp (speed, 2.0f, Time.deltaTime);
			speed = horizontal * horizontal + vertical * vertical;


			if (Input.GetButton ("Sprint")) 
			{
				speed = sprintSpeed;
				Debug.DrawRay (new Vector3 (this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z), Vector3.up , Color.green);
			}
			if (!IsInPivot ())
			{
				pivotAngle = turn * 180 / Mathf.PI;
			}

			Debug.DrawRay (new Vector3 (this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z), this.transform.forward , Color.green);

			animator.SetFloat ("Speed", speed, 0.4f, Time.deltaTime);
			animator.SetFloat ("Turn", turn, 0.1f, Time.deltaTime);
			animator.SetFloat ("PivotAngle", pivotAngle);
		}
	}

	public Vector3 StickToWorldSpace(Transform root, Transform camera)
	{
		Vector3 moveDirection = horizontal * camera.right  + vertical * Vector3.Scale(camera.forward, new Vector3(1,0,1)).normalized;
		return moveDirection;
	}

	public float CalculateTurn(Vector3 rootDirection,Vector3 moveDirection)
	{
		Vector3 sign = Vector3.Cross(rootDirection, moveDirection);
		return (Vector3.Angle(rootDirection, moveDirection) * (sign.y < 0 ? -1 : 1))/180;
	}

	public bool IsInPivot()
	{
		return animator.GetCurrentAnimatorStateInfo (0).IsName ("LocomotionPivotL")
			|| animator.GetCurrentAnimatorStateInfo (0).IsName ("LocomotionPivotR")
		    || animator.GetAnimatorTransitionInfo (0).IsName ("Locomotion2pivotL")
		    || animator.GetAnimatorTransitionInfo (0).IsName ("Locomotion2PivotR");
	}

	public bool NeedsExtraTurn()
	{
		return  animator.GetCurrentAnimatorStateInfo (0).IsName ("Locomotion") 
			 || animator.GetCurrentAnimatorStateInfo (0).IsName ("IdlePivotR")
			 || animator.GetCurrentAnimatorStateInfo (0).IsName ("IdlePivotL")
			;
	}

	public void ApplyExtraRotation ()
	{
		if (NeedsExtraTurn() && ((turn<0 && horizontal<0)||(turn>0&&horizontal>0)))
		{
			this.transform.Rotate (0, horizontal * turnSpeed * Time.deltaTime, 0);
		}
	}

}
