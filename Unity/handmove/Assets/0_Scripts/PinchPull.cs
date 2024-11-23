using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchSlide : MonoBehaviour
{
    [Header("[BuildingBlock] Hand Tracking left")]
    public OVRHand leftHand; // Assign the left hand OVRHand component in the Inspector

    [Header("[BuildingBlock] Hand Tracking right")]
    public OVRHand rightHand; // Assign the right hand OVRHand component in the Inspector

    [Header("[BuildingBlock] Camera Rig")]
    public OVRCameraRig cameraRig;

    [Range(0f, 10f)]
    public float speed = 5f;

    private Vector3 startPos;
    private Vector3 endPos;
    private bool firstFrame = true;


    void Update()
    {
        if (IsPinching(rightHand))
        {
            if (firstFrame)
            {
                startPos = rightHand.transform.position;
                firstFrame = false;
            } 
            else
            {
                MovePlayer(rightHand);
            }
        }
        else
        {
            firstFrame = true;
        }

        //if (IsPinching(leftHand))
        //    {
        //        if (firstFrame)
        //        {
        //            startPos = leftHand.transform.position;
        //            firstFrame = false;
        //        }
        //        else
        //        {
        //            MovePlayer(rightHand);
        //        }
        //    }
        //else
        //{
        //    firstFrame = true;
        //}
    }

    private void MovePlayer(OVRHand hand)
    {
        // set the new hand pos to be the endpoint
        endPos = rightHand.transform.position;

        // calculate the vector between the start and end
        Vector3 trans = startPos - endPos;

        // ignore the y difference to keep you on the ground
        trans.y = 0f;

        // move the camera the amount that the hand moved
        cameraRig.transform.position += trans;

        // and set the start to be the end point
        startPos = endPos;
    }

    private bool IsPinching(OVRHand hand)
    {
        if (hand == null) return false;

        // Check if the index finger is pinching
        return hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
    }
}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PinchSlide : MonoBehaviour
//{
//    [Header("[BuildingBlock] Hand Tracking left")]
//    public OVRHand leftHand; // Assign the left hand OVRHand component in the Inspector

//    [Header("[BuildingBlock] Hand Tracking right")]
//    public OVRHand rightHand; // Assign the right hand OVRHand component in the Inspector

//    [Header("[BuildingBlock] Camera Rig")]
//    public OVRCameraRig cameraRig;

//    [Range(0f, 10f)]
//    public float speed = 5f;

//    private Vector3 leftStartPos;  // Start position for left hand
//    private Vector3 rightStartPos; // Start position for right hand
//    private bool leftPinching = false; // Track left hand pinch state
//    private bool rightPinching = false; // Track right hand pinch state

//    void Update()
//    {
//        // Handle left hand pinching
//        if (IsPinching(leftHand))
//        {
//            if (!leftPinching)
//            {
//                // Start pinching: set the starting position
//                leftStartPos = leftHand.transform.position;
//                leftPinching = true;
//            }
//            else
//            {
//                // Continue pinching: move the player
//                leftStartPos = MovePlayer(leftHand, ref leftStartPos);
//            }
//        }
//        else
//        {
//            leftPinching = false;
//        }

//        // Handle right hand pinching
//        if (IsPinching(rightHand))
//        {
//            if (!rightPinching)
//            {
//                // Start pinching: set the starting position
//                rightStartPos = rightHand.transform.position;
//                rightPinching = true;
//            }
//            else
//            {
//                // Continue pinching: move the player
//                rightStartPos = MovePlayer(rightHand, ref rightStartPos);
//            }
//        }
//        else
//        {
//            rightPinching = false;
//        }
//    }

//    private Vector3 MovePlayer(OVRHand hand, ref Vector3 startPos)
//    {
//        // Calculate the movement based on the distance from the start position
//        Vector3 currentPos = hand.transform.position;
//        Vector3 translation = (startPos - currentPos);
//        Debug.Log("translation = " + translation);

//        // Apply movement to the camera rig (only in the forward direction)
//        cameraRig.transform.position += new Vector3(translation.x, 0f, translation.z);

//        // Update the start position to the current position for continuous movement
//        return currentPos;
//    }

//    private bool IsPinching(OVRHand hand)
//    {
//        // Check if the index finger is pinching
//        return hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
//    }
//}

//******************************************


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PinchSlide: MonoBehaviour
//{
//    [Header("[BuildingBlock] Hand Tracking left")]
//    public OVRHand leftHand; // Assign the left hand OVRHand component in the Inspector

//    [Header("[BuildingBlock] Hand Tracking right")]
//    public OVRHand rightHand; // Assign the right hand OVRHand component in the Inspector

//    [Header("[BuildingBlock] Camera Rig")]
//    public OVRCameraRig cameraRig;

//    [Range(0f,1f)]
//    public float speed = .5f;

//    private float speedLimiter = .1f;
//    private bool hasOldPos = false;

//    // Update is called once per frame
//    void Update()
//    {
//        if (IsPinching(leftHand))
//        {
//            MovePlayer(leftHand);
//        }
//        else if (IsPinching(rightHand))
//        {
//            MovePlayer(rightHand);
//        }
//    }

//    private void MovePlayer(OVRHand hand)
//    {
//        // store the new hand position
//        Vector3 newPos = hand.transform.position;

//        // if there was an old hand position, compare them
//        if (hasOldPos)
//        {
//            Vector3 translation = new Vector3 (newPos.x - oldPos.x, 0f, newPos.z - oldPos.z);
//            cameraRig.transform.position += speed * translation;
//        }

//        hasOldPos = true;
//        Vector3 oldPos = newPos;
//    }
//    private bool IsPinching(OVRHand hand)
//    {
//        // Example: Check if the index fingertip is close to the thumb tip
//        if (hand.GetFingerIsPinching(OVRHand.HandFinger.Index))
//        {
//            return true; // Finger pinch detected
//        }
//        return false;
//    }

//}
