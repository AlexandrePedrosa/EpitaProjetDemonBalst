using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerLogic : MonoBehaviour {

	[SerializeField]
	private float turnSpeed = 3.0f;
	[SerializeField]
	private Camera cam = null;


	private Animator animator;
	private float speed = 0.0f;
	private float turn = 0.0f;
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
			speed = moveDirection.z;


			Debug.DrawRay (new Vector3 (this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z), this.transform.forward , Color.green);

			animator.SetFloat ("Speed", speed, 0.1f, Time.deltaTime);
			animator.SetFloat ("Turn", turn, 0.1f, Time.deltaTime);
		}
	}

	public Vector3 StickToWorldSpace(Transform root, Transform camera)
	{
		Vector3 moveDirection =horizontal * camera.right  + vertical * Vector3.Scale(camera.forward, new Vector3(1,0,1)).normalized;
		return moveDirection;
	}

	public float CalculateTurn(Vector3 rootDirection,Vector3 moveDirection)
	{
		Vector3 sign = Vector3.Cross(rootDirection, moveDirection);
		return (Vector3.Angle(rootDirection, moveDirection) * (sign.y < 0 ? -1 : 1))/180;
	}

	public void ApplyExtraRotation ()
	{
		this.transform.Rotate (0, turn * turnSpeed * Time.deltaTime, 0);
	}
}
