using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	// player properties
	public float movementSpeed = 1;
	public float movementAcceleration = 1;
	public float movementAttenuation = 0.1f;

	public float rotationSpeed = 20;
	public float shipRotation = 0;

	// camera properties
	public float cameraMaxAngle = 90;
	public float cameraMinAngle = 0;

	public float cameraXMultiplier = 100;
	public float cameraRotation = 0;

	// key bindings

	// variables used in code
	private bool movementPressed = false;

	public GameObject camera = null;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;

		camera = GameObject.Find ("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {

		// Temp rotationVector for steering the vehicle
		var rotationVector = GetComponent<Rigidbody>().rotation;
		//var shipRotation = transform.rotation;

		#region rotation

		movementPressed = false;
	
		/*
		if (Input.GetKey(KeyCode.W))
		{
			GetComponent<Rigidbody>().AddForce (transform.forward * movementAcceleration * Time.deltaTime);
			movementPressed = true;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			GetComponent<Rigidbody>().AddForce (-transform.forward * movementAcceleration * Time.deltaTime);
			movementPressed = true;
		}
		*/

		/*
		if (Input.GetKey(KeyCode.D))
		{
			GetComponent<Rigidbody>().AddForce (transform.right * movementAcceleration * Time.deltaTime);
			movementPressed = true;
		}
		else if (Input.GetKey(KeyCode.A))
		{
			GetComponent<Rigidbody>().AddForce (-transform.right * movementAcceleration * Time.deltaTime);
			movementPressed = true;
		}
		*/

		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate (new Vector3(0, Time.deltaTime * rotationSpeed, 0));
			//shipRotation += rotationSpeed * Time.deltaTime;
			//movementPressed = true;
		}
		else if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate (new Vector3(0, -Time.deltaTime * rotationSpeed, 0));
			//shipRotation -= rotationSpeed * Time.deltaTime;
			//movementPressed = true;
		}

		if (Input.GetKey(KeyCode.W))
		{
			transform.Rotate (new Vector3(Time.deltaTime * rotationSpeed, 0, 0));
		}
		else if (Input.GetKey(KeyCode.S))
		{
			transform.Rotate (new Vector3(-Time.deltaTime * rotationSpeed, 0, 0));
		}

		transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);

		#endregion

		#region speed

		GetComponent<Rigidbody>().AddForce (transform.forward * movementAcceleration * Time.deltaTime);

		Vector3 speedVector = GetComponent<Rigidbody>().velocity;
		//speedVector.y = 0;
		if (speedVector.sqrMagnitude > movementSpeed * movementSpeed)
		{
			speedVector.Normalize();
			speedVector *= movementSpeed;
		}

		if (movementPressed == false)
		{
			if (speedVector.sqrMagnitude < 0.5f)
			{
				speedVector = Vector3.zero;
			}
			else
			{
				speedVector -= speedVector * movementAttenuation * Time.deltaTime;
			}
		}

		//speedVector.y = GetComponent<Rigidbody>().velocity.y;

		GetComponent<Rigidbody>().velocity = speedVector;

		#endregion

		#region Camera

		cameraRotation += Input.GetAxis("Mouse X") * Time.deltaTime * cameraXMultiplier;

		camera.transform.Rotate (new Vector3(-Input.GetAxis("Mouse Y") * Time.deltaTime * cameraXMultiplier, 0, 0));
		camera.transform.Rotate (new Vector3(0, Input.GetAxis("Mouse X") * Time.deltaTime * cameraXMultiplier, 0));

		//rotationVector = Quaternion.AngleAxis(cameraRotation, Vector3.up);

		camera.transform.rotation = Quaternion.LookRotation(camera.transform.forward, Vector3.up);

		//if (cameraRotation > Mathf.PI)
		//	cameraRotation -= Mathf.PI;
		//if (cameraRotation < -1)
			//cameraRotation -= 1;


		//rotationVector.y = cameraRotation;
		//GetComponent<Rigidbody>().rotation = rotationVector;

		//GetComponent<Rigidbody> ().rotation = Quaternion.AngleAxis(shipRotation, Vector3.up);

		#endregion
	}
}
