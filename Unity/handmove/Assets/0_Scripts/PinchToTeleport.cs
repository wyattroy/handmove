using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchToTeleport : MonoBehaviour
{
    [Header("OVR Hand Settings")]
    public OVRHand hand; // Assign the OVRHand component from the Inspector
    public OVRInput.Controller handController; // Specify whether it's left or right hand

    [Header("Reticle Settings")]
    public GameObject reticlePrefab; // Assign a prefab for the reticle
    public LayerMask groundLayer; // Layer mask for valid ground surfaces
    public float maxDistance = 10f; // Maximum distance for raycast
    public float smoothingSpeed = 5f; // Speed for reticle movement smoothing

    private GameObject reticleInstance;
    private Camera vrCamera;
    private bool isPinching;
    private Vector3 targetPosition;

    private void Awake()
    {
        // Find and cache the VR camera
        vrCamera = Camera.main;
        if (vrCamera == null)
        {
            Debug.LogError("No Main Camera found! Ensure your OVRCamera is tagged as MainCamera.");
            enabled = false;
            return;
        }

        // Create the reticle instance
        if (reticlePrefab != null)
        {
            reticleInstance = Instantiate(reticlePrefab);
            reticleInstance.SetActive(false); // Start with the reticle hidden
        }
        else
        {
            Debug.LogError("Reticle prefab is not assigned!");
            enabled = false;
        }
    }

    void Update()
    {
        // Guard clause!!
        if (hand == null || reticleInstance == null)
            return;

        // Check for pinch gesture
        isPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);

        if (isPinching)
        {
            // Perform raycast to position the reticle
            UpdateReticle();

            if (!reticleInstance.activeSelf)
            {
                reticleInstance.SetActive(true);
            }
                
        }
        else if (reticleInstance.activeSelf)
        {
            // Perform teleportation if the pinch was released
            TeleportPlayer();
        }
    }


  

    private void UpdateReticle()
    {
        Vector3 rayOrigin = vrCamera.transform.position;
        Vector3 rayDirection = vrCamera.transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, maxDistance, groundLayer))
        {
            targetPosition = hit.point;
            
            // Start the reticle's position exactly where the viewer's looking
            reticleInstance.transform.position = targetPosition;
        }
        else
        {
            // If no hit, place the reticle at max distance
            targetPosition = rayOrigin + rayDirection * maxDistance;
        }

        // Smoothly move the reticle to the target position
        reticleInstance.transform.position = Vector3.Lerp(reticleInstance.transform.position, targetPosition, Time.deltaTime * smoothingSpeed);
        reticleInstance.transform.rotation = Quaternion.identity; // Ensure the reticle stays flat
    }

    private void TeleportPlayer()
    {
        // Move the player to the reticle's position
        Vector3 teleportPosition = targetPosition;
        teleportPosition.y += 0.5f; // Slight offset to prevent the player from being placed inside the ground
        transform.position = teleportPosition;

        // Hide the reticle
        reticleInstance.SetActive(false);
    }
}
