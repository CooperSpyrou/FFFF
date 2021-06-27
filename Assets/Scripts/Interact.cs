using UnityEngine;
using System.Collections;
using TMPro;

public class Interact : MonoBehaviour
{
    private Camera cameraa;
    private GameObject interactText;


	private void Start()
	{
        cameraa = GetComponent<Camera>();
        interactText = GameObject.Find("InteractText");
	}

    void Update()
    {
        RaycastHit hit;
        Ray ray = cameraa.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 15))
        {
            if (hit.transform.gameObject.tag == "Interactable")
            {
                //hit.transform.gameObject.GetComponent<Outline>().enabled = true;
            }
        }
        else
        {
            //hit.transform.gameObject.GetComponent<Outline>().enabled = false;
        }


        if (Physics.Raycast(ray, out hit, 5))
        {
            if (hit.transform.gameObject.tag == "Interactable")
            {
                interactText.GetComponent<TextMeshProUGUI>().enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.gameObject.GetComponent<Interactable>().zInteract();
                }
            }
            else
            {
                interactText.GetComponent<TextMeshProUGUI>().enabled = false;
            }
        }
    }
}