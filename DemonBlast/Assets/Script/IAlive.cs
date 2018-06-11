using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAlive
{
	int HP{ get;}
	void TakeDamage(int amount);
}

