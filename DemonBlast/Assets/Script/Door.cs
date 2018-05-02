using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

	private int key = 0;
	public int value;
	
	void GetKey()
	{
		key += 1;
		if (key >= value)
			Destroy(gameObject);
	}
}
