using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent interact;
    public UnityEvent on;
    public UnityEvent off;

    private bool interaction = false;

    public void zInteract()
	{
        interaction = !interaction;
        interact.Invoke();
        if (interaction) { zOn(); }
        if (!interaction) { zOff(); }
	}

    public void zOn()
    {
        on.Invoke();
    }

    public void zOff()
    {
        off.Invoke();
    }
}
