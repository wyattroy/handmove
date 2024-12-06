using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchToTeleport : MonoBehaviour
{
    [Header("OVR Hand Settings")]
    [SerializeField] private OVRHand hand; // Assign the OVRHand component from the Inspector
    [SerializeField] private OVRInput.Controller handController; // Specify whether it's left or right hand

    [Header("Reticle Settings")]
    [SerializeField] private GameObject reticlePrefab; // Assign a prefab for the reticle
    [SerializeField] private LayerMask groundLayer; // Layer mask for valid ground surfaces
    [SerializeField] private float maxDistance = 10f; // Maximum distance for raycast
    [SerializeField] private Material validMaterial;   // Assign a valid material in the Inspector
    [SerializeField] private Material invalidMaterial; // Assign an invalid material in the Inspector

    private GameObject reticleInstance;
    private Renderer reticleRenderer;
    private Camera vrCamera;
    private bool isPinching;
    private Vector3 targetPosition;
    private bool validTeleportLocation;

    private void Awake()
    {
        // Find and cache the VR camera
        vrCamera = Camera.main;
        if (vrCamera == null)
        {
            Debug.LogError("No Main Camera found! Ensure your OVRCamera is tagged as MainCamera.");
            
            // Immediately stop calling this script so we don't freeze
            enabled = false;
            return;
        }

        // Create the reticle instance
        if (reticlePrefab != null)
        {
            // Make a new reticle out of the prefab
            reticleInstance = Instantiate(reticlePrefab);

            // Start with the reticle hidden
            reticleInstance.SetActive(false); 

            // Cache the Renderer so we can change the Reticle's color later
            reticleRenderer = reticleInstance.GetComponent<Renderer>();
        }
        else
        {
            Debug.LogError("Reticle prefab is not assigned!");
            
            // Immediately quit calling the script so we don't freeze
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
        else if (reticleInstance.activeSelf && validTeleportLocation)
        {
            // Perform teleportation if the pinch was released
            TeleportPlayer();

            // And hide the reticle
            reticleInstance.SetActive(false);
        } 
        else
        {
            // Hide the reticle after the person stops pinching, even if they can't teleport
            reticleInstance.SetActive(false);
        }
        
        
    }


  

    private void UpdateReticle()
    {
        Vector3 rayOrigin = vrCamera.transform.position;
        Vector3 rayDirection = vrCamera.transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, maxDistance, groundLayer))
        {
            targetPosition = hit.point;

            // Start the reticle's position exactly where the viewer's looking, instead of having it lerp from (0,0,0)
            reticleInstance.transform.position = targetPosition;
            validTeleportLocation = true;

            // Set reticle material to the valid color
            if (reticleRenderer != null && validMaterial != null)
                reticleRenderer.material = validMaterial;
        }
        else
        {
            // If no hit, place the reticle at max distance
            targetPosition = rayOrigin + rayDirection * maxDistance;
            reticleInstance.transform.position = targetPosition;
            validTeleportLocation = false;
            
            // Make the reticle red
            reticleRenderer.material = invalidMaterial;
        }


    }

    private void TeleportPlayer()
    {
        // Move the player to the reticle's position
        Vector3 teleportPosition = targetPosition;
        teleportPosition.y += 0.5f; // Slight offset to prevent the player from being placed inside the ground
        transform.position = teleportPosition;
    }
}
