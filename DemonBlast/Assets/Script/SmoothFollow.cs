using UnityEngine;

namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{

		private Vector3 lastPos;
		// The target we are following
		public Rigidbody target;
		// The distance in the x-z plane to the target
		public Transform lookat;
		public float distance = 5.0f;
		// the height we want the camera to be above the target

		public float height = 15f;

		public float cameraSensi =360f;
		public float maxHeight = 85f;
		public float minHeight = 285f;
		public float rotationDamping = 0.1f;

		public float heightDamping = 4f;

		// Use this for initialization
		void Start()
		{  
			lastPos = lookat.position - lookat.rotation * Vector3.forward * distance ;
		}

		// Update is called once per frame
		void LateUpdate()
		{
			// Early out if we don't have a target
			if (!target || !lookat)
				return;

			transform.position = lastPos;
			transform.LookAt (lookat);

			var l = Input.GetAxis ("Look X");
			var h = Input.GetAxis ("Look Y");

			var currentRotation = Quaternion.Euler (0, 0, 0);

			if (l != 0 || h != 0) 
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

				currentRotation = Quaternion.Euler (RotXangle, RotYangle, 0);
				transform.position = lookat.position - currentRotation * Vector3.forward * distance;
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
				currentRotation = Quaternion.Euler (currentRotationAngleX, currentRotationAngleY, 0);
				transform.position = lookat.position - currentRotation * Vector3.forward * distance;

				// Set the height of the camera

			}

			// Always look at the target
			lastPos = transform.position;
			transform.LookAt (lookat);
			PreventClip();
			transform.position += transform.rotation * Vector3.forward * 0.3f;
		}
		private void PreventClip()
		{
			Debug.DrawLine (lookat.position, transform.position, Color.cyan);

			RaycastHit intersect = new RaycastHit();
			if (Physics.Linecast (lookat.position, transform.position, out intersect)) 
			{
				transform.position += transform.rotation * Vector3.forward * (distance - intersect.distance);
			}
		}
	}
}