using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
	private Transform pickUpParent;

	private Vector3 previousGrabPosition;

	private float maxThrowSpeed = 15f;

	private bool pickedUp = false;

	private GameObject playerTarget;

	private Vector3 ogScale;

	private void Start()
	{
		ogScale = transform.localScale;
		pickUpParent = GameObject.Find("PickUpParent").transform;
		playerTarget = GameObject.Find("Main Camera");
	}

	public void OnMouseDown()
	{
		if (!pickedUp)
		{
			GetComponent<Rigidbody>().useGravity = false;
			this.transform.position = pickUpParent.position;
			this.transform.parent = pickUpParent;
			GetComponent<Rigidbody>().useGravity = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			pickedUp = true;
			//var lookRot = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
			//transform.rotation = lookRot;
			transform.rotation.SetLookRotation(Camera.main.transform.position);
			if (GetComponent<BoxCollider>() != null) { GetComponent<BoxCollider>().enabled = false; }
			if (GetComponent<SphereCollider>() != null) { GetComponent<SphereCollider>().enabled = false; }
		}
	}

	public void OnMouseUp()
	{
		if (pickedUp)
		{
			this.transform.parent = null;
			transform.localScale = ogScale;
			//GetComponent<Rigidbody>().useGravity = true;

			var rb = GetComponent<Rigidbody>();
			Vector3 throwVector = transform.position - previousGrabPosition;
			float speed = throwVector.magnitude / Time.deltaTime;
			Vector3 throwVelocity = speed * throwVector.normalized;
			if (throwVelocity.x > maxThrowSpeed) { throwVelocity.x = maxThrowSpeed; }
			if (throwVelocity.y > maxThrowSpeed) { throwVelocity.y = maxThrowSpeed; }
			if (throwVelocity.z > maxThrowSpeed) { throwVelocity.z = maxThrowSpeed; }
			if (throwVelocity.x < -maxThrowSpeed) { throwVelocity.x = -maxThrowSpeed; }
			if (throwVelocity.y < -maxThrowSpeed) { throwVelocity.y = -maxThrowSpeed; }
			if (throwVelocity.z < -maxThrowSpeed) { throwVelocity.z = -maxThrowSpeed; }
			rb.velocity = throwVelocity;
			Debug.Log(throwVelocity);
			rb.useGravity = true;
			pickedUp = false;
			if (GetComponent<BoxCollider>() != null) { GetComponent<BoxCollider>().enabled = true; }
			if (GetComponent<SphereCollider>() != null) { GetComponent<SphereCollider>().enabled = true; }
		}
	}


	private void Update()
	{
		previousGrabPosition = transform.position;

		if (pickedUp)
		{
			transform.rotation = playerTarget.transform.rotation;

			transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
		}
	}
}
