using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAR;

public class ResetButton : MonoBehaviour
{
    private ImageTracking imageTrackingScript;

    private void Start()
    {
        imageTrackingScript = FindObjectOfType<ImageTracking>();
    }

    public void ResetTracking()
    {
        if(imageTrackingScript != null)
        {
            imageTrackingScript.ResetTracking();
        }
    }
}
