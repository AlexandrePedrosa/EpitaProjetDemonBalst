using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {

	private Collider player;
	[SerializeField]
	private int amount;

	private void OnTriggerEnter(Collider other)
	{
		var a = other.gameObject.GetComponent<IAlive>();
		a.TakeDamage(amount);
	}
}
