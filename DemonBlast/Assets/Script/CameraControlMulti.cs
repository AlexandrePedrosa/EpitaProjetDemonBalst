using UnityEngine;
using UnityEngine.Networking;

namespace UnityStandardAssets.Utility
{
	public class CameraControlMulti : NetworkBehaviour
	{

		private Vector3 lastPos;
		// The target we are following
		public GameObject player;
		// The distance in the x-z plane to the target
		private Rigidbody target;
		private Transform lookat;
		public Transform rightStick;
		public float distance = 5.0f;
		// the height we want the camera to be above the target

		public float height = 15f;
		private float l, h;
		public float cameraSensi =360f;
		public float maxHeight = 85f;
		public float minHeight = 285f;
		public float rotationDamping = 0.1f;

		public float heightDamping = 4f;

		// Use this for initialization
		void Start()
		{  
			
			target = player.GetComponent<Rigidbody>();
			lookat = player.transform.GetChild(0);
			lastPos = lookat.position - lookat.rotation * Vector3.forward * distance ;
		}

		// Update is called once per frame
		void LateUpdate()
		{
			// Early out if we don't have a target
			if (!target || !lookat) 
			{
				return;
			}
			 
			Debug.DrawRay (transform.position, Vector3.up);

			transform.position = lastPos;
			transform.LookAt (lookat);

			l = rightStick.position.x;
			h = rightStick.position.y;


			if (l != 0 || h != 0 ) 
			{
				MoveCamera (l, h);
			} 
			else
			{
				// Calculate the current rotation angles
				var wantedRotationAngleY = lookat.transform.eulerAngles.y;
				var wantedRotationAngleX = lookat.transform.eulerAngles.x + height;

				var currentRotationAngleY = transform.eulerAngles.y;
				var currentRotationAngleX = transform.eulerAngles.x;

				// Damp the rotation around the y-axis
				currentRotationAngleY = Mathf.LerpAngle (currentRotationAngleY, wantedRotationAngleY,  target.velocity.magnitude * rotationDamping * Time.deltaTime);
				currentRotationAngleX = Mathf.LerpAngle (currentRotationAngleX, wantedRotationAngleX, target.velocity.magnitude * heightDamping * Time.deltaTime);
				// Damp the height


				// Convert the angle into a rotation


				// Set the position of the camera on the x-z plane to:
				// distance meters behind the target
				var currentRotation = Quaternion.Euler (currentRotationAngleX, currentRotationAngleY, 0);
				transform.position = lookat.position - currentRotation * Vector3.forward * distance;

				// Set the height of the camera

			}

			// Always look at the target
			lastPos = transform.position;
			transform.LookAt (lookat);
			PreventClip();
			transform.position += transform.rotation * Vector3.forward * 0.3f;
		}



		private void MoveCamera(float l, float h)
		{
			var RotYangle = transform.eulerAngles.y + l * cameraSensi * Time.deltaTime;
			var RotXangle = transform.eulerAngles.x + h * cameraSensi * Time.deltaTime;
			if (h > 0 && RotXangle > maxHeight && RotXangle < minHeight) 
			{
				RotXangle = maxHeight;
			}
			else if (h < 0 && RotXangle < minHeight && RotXangle > maxHeight) 
			{
				RotXangle = minHeight;
			}

			var currentRotation = Quaternion.Euler (RotXangle, RotYangle, 0);
			transform.position = lookat.position - currentRotation * Vector3.forward * distance;
		}

		private void PreventClip()
		{
			var currentRotation = Quaternion.Euler (0, 0, 0);
			Debug.DrawLine (lookat.position, transform.position, Color.cyan);

			RaycastHit intersect = new RaycastHit();
			if (Physics.Linecast (lookat.position, transform.position, out intersect)) 
			{
				transform.position += transform.rotation * Vector3.forward * (distance - intersect.distance);
			}
		}

		public float L
		{
			get{return l;}
			set{l = value;}
		}
		public float H
		{
			get{return h;}
			set{h = value;}
		}
	}
}