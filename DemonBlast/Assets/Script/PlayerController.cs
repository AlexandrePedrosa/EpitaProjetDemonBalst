using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{


	void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

	}

	// This [Command] code is called on the Client …
	// … but it is run on the Server!
	public override void OnStartLocalPlayer ()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}
}