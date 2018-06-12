using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {

	private Collider player;
	[SerializeField]
	public int damage;

	private void OnTriggerEnter(Collider other)
	{
		var a = other.gameObject.GetComponent<IAlive>();
		a.TakeDamage(damage);
	}
}
