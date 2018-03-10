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
		public float distance = 10.0f;
		// the height we want the camera to be above the target

		public float height = 5.0f;


		public float rotationDamping;

		public float heightDamping;

		// Use this for initialization
		void Start()
		{ 
			lastPos = target.position - target.rotation * Vector3.forward * distance ;
		}

		// Update is called once per frame
		void LateUpdate()
		{
			// Early out if we don't have a target
			if (!target || !lookat)
				return;

			transform.position = lastPos;
			transform.LookAt(lookat);

			// Calculate the current rotation angles
			var wantedRotationAngle = lookat.transform.eulerAngles.y;
			var wantedHeight = lookat.position.y + height;

			var currentRotationAngle = transform.eulerAngles.y;
			var currentHeight = transform.position.y;

			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			// Damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			// Convert the angle into a rotation


			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			var currentRotation = Quaternion.Euler(0, currentRotationAngle,0);
			transform.position = lookat.position - currentRotation * Vector3.forward * distance;

			// Set the height of the camera
			transform.position = new Vector3(transform.position.x ,currentHeight , transform.position.z);

			// Always look at the target
			transform.LookAt(lookat);
			lastPos = transform.position;
		}
	}
}