using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTrigger : MonoBehaviour
{
    public OVRHand leftHand; // Assign the left hand OVRHand component in the Inspector
    public OVRHand rightHand; // Assign the right hand OVRHand component in the Inspector
    //public UnityEngine.Events.UnityEvent onFirstTap; // Event triggered when a tap is detected
    //public UnityEngine.Events.UnityEvent onSecondTap; // Event trigger
    public GameObject reticle;
    public OVRCameraRig cameraRig;

    private bool wasTapped = false;
    private bool firstTap = true;

    // Update is called once per frame
    void Update()
    {
        if (IsFingerTapped(leftHand) || IsFingerTapped(rightHand))
        {
            if (!wasTapped) // To avoid multiple triggers in the same tap
            {
                wasTapped = true;
                
                if (firstTap)
                {
                    //onFirstTap?.Invoke(); // Trigger the event
                    Debug.Log("First finger tap!");
                    reticle.SetActive(true);
                    firstTap = false;
                }
                else 
                {
                    //onSecondTap?.Invoke(); // Trigger second event
                    Debug.Log("Second tap!");
                    cameraRig.transform.position = reticle.transform.position;
                    reticle.SetActive(false);
                    firstTap = true;
                }
                
                
            }
        }
        else
        {
            wasTapped = false; // Reset when no tap is detected
        }
    }

    private bool IsFingerTapped(OVRHand hand)
    {
        // Example: Check if the index fingertip is close to the thumb tip
        if (hand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            return true; // Finger tap detected
        }
        return false;
    }
    
}
