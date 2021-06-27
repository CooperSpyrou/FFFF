using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStates : MonoBehaviour
{
	public bool canBeCooked = false;
	public bool canBeWashed = false;
	public bool canBeFixedd = false;
	public bool canBeFed = false;


    public bool cooked = false;
    public bool washed = false;
    public bool fixedd = false;
    public bool fed = false;


	private GameObject smokeEmitter;
	private GameObject waterEmitter;


	private void Start()
	{
		if (canBeCooked)
		{
			smokeEmitter = transform.Find("SmokeEmitter").gameObject;
		}

		if (canBeWashed)
		{
			waterEmitter = transform.Find("WaterEmitter").gameObject;
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		if (canBeCooked && other.tag == "Fire")
		{
			cooked = true;
			if (GetComponent<Renderer>() != null) { GetComponent<Renderer>().material.color = new Color32(50, 50, 50, 255); }
			//if (GetComponentInChildren<SkinnedMeshRenderer>() != null) { GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color32(0, 0, 0, 255); }
			if (GetComponentInChildren<SkinnedMeshRenderer>() != null) { GetComponentInChildren<SkinnedMeshRenderer>().materials[2].color = new Color32(0, 0, 0, 255); }
			}


		if (canBeWashed && other.tag == "Water")
		{
			washed = true;
		}


		if (canBeFed && other.tag == "Food")
		{
			fed = true;
			Destroy(other.transform.gameObject);
			// Play eat sound
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (canBeCooked && other.tag == "Fire")
		{
			smokeEmitter.SetActive(true);
			CancelInvoke();
		}


		if (canBeWashed && other.tag == "Water")
		{
			waterEmitter.SetActive(true);
			CancelInvoke();
		}
	}

	private void Update()
	{
		if (canBeCooked && smokeEmitter.activeSelf)
		{
			Invoke("stopSmoking", 1.5f);
		}


		if (canBeWashed && waterEmitter.activeSelf)
		{
			Invoke("stopDripping", 1f);
		}
	}




	private void stopSmoking()
	{
		smokeEmitter.SetActive(false);
		CancelInvoke();
	}


	private void stopDripping()
	{
		waterEmitter.SetActive(false);
		CancelInvoke();
	}







	public void zFixed()
	{
		fixedd = true;
	}

	public void zNotFixed()
	{
		fixedd = false;
	}
}
