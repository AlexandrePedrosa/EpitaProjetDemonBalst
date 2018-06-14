using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {

	[SerializeField]
	private int amount;

	private void OnTriggerEnter(Collider other)
	{
		var a = other.gameObject.GetComponent<IAlive>();
		if (a != null) 
		{
			a.TakeDamage (amount);
		}
	}
}
