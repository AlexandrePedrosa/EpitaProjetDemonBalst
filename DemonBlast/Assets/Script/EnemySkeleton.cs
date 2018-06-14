using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : MonoBehaviour, IAlive
{
	[SerializeField]
	private GameObject AttackZone;
	[SerializeField]
	private Object target;
	[SerializeField]
	private int health;

	private Animator animator;

	private GameObject targetGameObject;
	public Vector3 A;
	public Vector3 B;
	private Vector3 rpos;
	public float ChasingSpeed;
	public float PatrolSpeed;
	public float DetectRange;
	public float AttackRange;
	private GameObject attack;
	public int AttackDelay;
	public int damage;
	private float dtime;
	private Collider attZone;
	private Vector3 trgtTrans;


	public int HP
	{
		get{return health;}
	}
	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> ();
		health = 2;
		Object currentTarget = target ?? gameObject;
		Behaviour targetBehaviour = currentTarget as Behaviour;
		targetGameObject = currentTarget as GameObject;
		rpos = Vector3.positiveInfinity;
		attZone = AttackZone.GetComponent<Collider> ();
		trgtTrans = new Vector3(targetGameObject.transform.position.x,targetGameObject.transform.position.y,targetGameObject.transform.position.z);
		/*attack = GameObject.CreatePrimitive(PrimitiveType.Cube);
        attack.GetComponent<BoxCollider>().isTrigger = true;
        attack.transform.localScale = Vector3.one;
        attack.GetComponent<MeshRenderer>().enabled = false;
        attack.AddComponent<DealDamage>();
        attack.GetComponent<DealDamage>().damage = damage;*/
		dtime = AttackDelay;
	}
	
	// Update is called once per frame
	void Update () 
	{
		animator.SetBool ("Attack", false);
		if (CanAttack())
		{
			trgtTrans = new Vector3(targetGameObject.transform.position.x,targetGameObject.transform.position.y,targetGameObject.transform.position.z);
			trgtTrans.y = this.transform.position.y;
			gameObject.transform.LookAt(trgtTrans);
			// Attack
			if (dtime > AttackDelay)
			{
				dtime = 0;
				animator.SetBool ("Attack", true);
				attZone.enabled = true;
				Invoke("DisableAttZone", 0.5f);
			}
			dtime += Time.deltaTime;
			//
		}
		else
		{
			if (IsClose())
			{
				if (Vector3.Equals(rpos, Vector3.positiveInfinity))
					rpos = this.gameObject.transform.position;
				trgtTrans = new Vector3(targetGameObject.transform.position.x,targetGameObject.transform.position.y,targetGameObject.transform.position.z);
				trgtTrans.y = this.transform.position.y;
				gameObject.transform.LookAt(trgtTrans);
				animator.SetFloat ("Speed", ChasingSpeed);
			}
			else
			{
				if (!Vector3.Equals(rpos, Vector3.positiveInfinity))
				{
					trgtTrans = rpos;
					trgtTrans.y = this.transform.position.y;
					gameObject.transform.LookAt(trgtTrans);
					animator.SetFloat ("Speed", PatrolSpeed);
					dtime = 0;
					if (Vector3.Distance(this.gameObject.transform.position, rpos) < 1)
					{
						this.gameObject.transform.position = rpos;
						rpos = Vector3.positiveInfinity;
					}
				}
				else
				{
					trgtTrans = B;
					trgtTrans.y = this.transform.position.y;
					gameObject.transform.LookAt(trgtTrans);
					if (Vector3.Distance(this.gameObject.transform.position, B) <1)
					{
						this.gameObject.transform.position = B;
						Vector3 C = A;
						A = B;
						B = C;
					}
					else
					{
						animator.SetFloat ("Speed", PatrolSpeed);
					}
				}
			}
		}
		if ( animator.GetCurrentAnimatorStateInfo (0).IsName ("Death"))
		{
			animator.SetBool ("Dead", false);
		}
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("TakeDamage"))
		{
			animator.SetBool ("TakeDamage", false);
		}

	}

	private void DisableAttZone ()
	{
		attZone.enabled = false;
	}


	private void Depop ()
	{
		Destroy (this);
	}

	private bool IsClose()
	{
		return targetGameObject != null && Vector3.Distance (targetGameObject.transform.position, this.gameObject.transform.position) < DetectRange;
	}

	private bool CanAttack()
	{
		return targetGameObject != null && Vector3.Distance (targetGameObject.transform.position, this.gameObject.transform.position) < AttackRange;
	}



	public void TakeDamage(int amount)
	{
		health -= amount;
		if (health > 0) 
		{
			animator.SetBool ("TakeDamage", true);
		} else 
		{
			animator.SetBool ("Dead", true);
			Invoke ("Depop", 0.5f);
		}
	}
}
