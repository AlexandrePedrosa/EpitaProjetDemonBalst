using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterControllerLogicMulti : NetworkBehaviour {

	[SerializeField]
	private float turnSpeed = 3.0f;
	[SerializeField]
	private Camera cam = null;
	[SerializeField]
	private float jumpForce = 3.0f;
	[SerializeField]
	private float jumpDist = 3.0f;
	[SerializeField]
	private float groundCheckDistance = 0.2f;

	private CapsuleCollider capCollider;
	new private Rigidbody rigidbody;
	private Animator animator;
	private float speed = 0.0f;
	private float sprintSpeed = 0.0f;
	private float turn = 0.0f;
	private float pivotAngle = 0.0f;
	private float horizontal = 0.0f;
	private float vertical = 0.0f;
	private Vector3 moveDirection = new Vector3();
	private float capHeight = 0.0f;
	private Vector3 capCenter = new Vector3();
	private Vector3 groundNormal = Vector3.up;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator> ();
		rigidbody = GetComponent<Rigidbody> ();
		capCollider = GetComponent<CapsuleCollider> ();
		capHeight = capCollider.height;
		capCenter = capCollider.center;
		if (animator.layerCount >= 2) 
		{
			animator.SetLayerWeight (1, 1);
		}
	}

	void FixedUpdate()
	{
		ApplyExtraRotation();
		capCollider.height = capHeight;
		capCollider.center = capCenter;
		animator.SetBool ("OnGround", IsOnGround());
		if (IsInJump())
		{
			HandleJump ();
		}

	}

	// Update is called once per frame
	void Update ()
	{
		if (!isLocalPlayer) {
			return;
		}
		if (animator)
		{
			

			horizontal = Input.GetAxis ("Horizontal");
			vertical = Input.GetAxis ("Vertical");
			animator.SetBool ("Jump", Input.GetButton ("Jump"));
			sprintSpeed = Mathf.Lerp (speed, 2.0f, Time.deltaTime);
			speed = horizontal * horizontal + vertical * vertical;
			if (Input.GetButton ("Sprint")) 
			{
				speed = sprintSpeed;

			}
			

			moveDirection = StickToWorldSpace (this.transform, cam.transform);
			Debug.DrawRay (new Vector3 (this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z), moveDirection, Color.magenta);
			moveDirection = transform.InverseTransformDirection(moveDirection);

			turn = Mathf.Atan2 (moveDirection.x, moveDirection.z);

			
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

	public bool IsInJump()
	{
		return animator.GetCurrentAnimatorStateInfo (0).IsName ("Locomotion@Run_Jump")
			|| animator.GetCurrentAnimatorStateInfo (0).IsName ("Locomotion@Idle_Jump")
			|| animator.GetCurrentAnimatorStateInfo (0).IsName ("FallDown");
	}

	public bool IsInPivot()
	{
		return animator.GetCurrentAnimatorStateInfo (0).IsName ("LocomotionPivotL")
			|| animator.GetCurrentAnimatorStateInfo (0).IsName ("LocomotionPivotR")
			|| animator.GetAnimatorTransitionInfo (0).IsName ("Locomotion2pivotL")
			|| animator.GetAnimatorTransitionInfo (0).IsName ("Locomotion2PivotR");
	}

	public bool IsOnGround()
	{
		Debug.DrawRay (capCenter, new Vector3(capCenter.x, capCenter.y -groundCheckDistance - capHeight, capCenter.z) , Color.green);
		RaycastHit hitInfo;
		int LayerMask = 1 << 8;
		LayerMask = ~LayerMask;
		if (Physics.Raycast(this.transform.position + capCenter, Vector3.down, out hitInfo, groundCheckDistance + capHeight, LayerMask))
		{
			animator.applyRootMotion = true;
			groundNormal = hitInfo.normal;
			return true;
		}
		else
		{
			animator.applyRootMotion = false;
			groundNormal = Vector3.up;
			return false;
		}
	}

	public bool NeedsExtraTurn()
	{
		return  animator.GetCurrentAnimatorStateInfo (0).IsName ("Locomotion") 
			|| animator.GetCurrentAnimatorStateInfo (0).IsName ("IdlePivotR")
			|| animator.GetCurrentAnimatorStateInfo (0).IsName ("IdlePivotL");
	}

	public void ApplyExtraRotation ()
	{
		if (NeedsExtraTurn() && ((turn<0 && horizontal<0)||(turn>0&&horizontal>0)))
		{
			this.transform.Rotate (0, horizontal * turnSpeed * Time.deltaTime, 0);
		}
	}

	public void HandleJump ()
	{
		if (animator.GetBool ("OnGround"))
		{
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Locomotion@Idle_Jump")) 
			{
				rigidbody.velocity = new Vector3 (rigidbody.velocity.x ,jumpForce, rigidbody.velocity.z);
			}

			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Locomotion@Run_Jump")) 
			{
				rigidbody.velocity = new Vector3 (rigidbody.velocity.x ,jumpForce, rigidbody.velocity.z) + this.transform.forward * jumpDist;
				Debug.DrawRay (new Vector3 (this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z), Vector3.up , Color.green);
			}
		}
		capCollider.center = capCenter * 2;
		capCollider.height = capHeight / 2;
	}

}
