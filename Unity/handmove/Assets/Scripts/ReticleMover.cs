using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleMover : MonoBehaviour
{
    [Header("Make sure this reticle is DISabled before awake")]
    public GameObject reticle;
    [Space(10)]

    [Tooltip("The layer mask to use for floor detection.")]
    public LayerMask floorLayer;

    [Tooltip("The distance from the headset to cast the ray.")]
    public float raycastDistance = 10f;

    [Tooltip("Smoothing speed for reticle movement.")]
    public float smoothingSpeed = 5f;

    public float raycastInterval = 0.2f; // Perform raycast every 0.2 seconds (5 times per second)


    private Camera vrCamera;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float timeSinceLastRaycast = 0f;

    void Start()
    {

        vrCamera = Camera.main;
        if (vrCamera == null)
        {
            Debug.LogError("Make sure there's a VR camera in the scene tagged as 'MainCamera'.");
        }

        // Initialize the first raycast
        PerformRaycast();
        reticle.transform.position = targetPosition;

    }

    void OnEnable()
    {
        // Make the reticle appear exactly where the user looks
        PerformRaycast();
        reticle.transform.position = targetPosition;
    }

    void Update()
    {
        timeSinceLastRaycast += Time.deltaTime;

        if (vrCamera == null || reticle == null) return;

        // Perform raycast only when necessary
        if (timeSinceLastRaycast > raycastInterval)
        {
            PerformRaycast();
            timeSinceLastRaycast = 0f;

        }
        
        // Smoothly move the reticle to the target position
        reticle.transform.position = Vector3.Lerp(reticle.transform.position, targetPosition, Time.deltaTime * smoothingSpeed);
        reticle.transform.rotation = targetRotation;
    }

    void PerformRaycast()
    {
        Vector3 rayOrigin = vrCamera.transform.position;
        Vector3 rayDirection = vrCamera.transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, raycastDistance, floorLayer))
        {
            targetPosition = hit.point;
            targetRotation = reticle.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
        else
        {
            // Default to a position directly in front of the user, keeping it on the floor
            Vector3 defaultPosition = rayOrigin + rayDirection * raycastDistance;
            defaultPosition.y = 0; // Adjust to the floor height if necessary
            targetPosition = defaultPosition;
        }
    }
}
