using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnight : MonoBehaviour, IAlive
{

	private Animator animator;
	private int health;


	public int HP
	{
		get{return health;}
	}
	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> ();
		health = 15;
	}

	// Update is called once per frame
	void Update () 
	{

		if ( animator.GetCurrentAnimatorStateInfo (0).IsName ("Death"))
		{
			animator.SetBool ("Dead", false);
		}
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("TakeDamage"))
		{
			animator.SetBool ("TakeDamage", false);
		}
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
		}
	}
}
