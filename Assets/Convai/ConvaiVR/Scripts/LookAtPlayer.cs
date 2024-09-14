using Unity.XR.CoreUtils;
using UnityEngine;

/// <summary>
/// Make the object look at the XR Origin's camera
/// </summary>
public class LookAtPlayer : MonoBehaviour
{
    [Tooltip("Follow x axis")]
    [SerializeField] private bool _lookX = false;

    [Tooltip("Follow y axis")]
    [SerializeField] private bool _lookY = false;

    [Tooltip("Follow z axis")]
    [SerializeField] private bool _lookZ = false;

    // Reference to the main camera
    private GameObject cameraObject = null;

    // Original rotation of the object
    private Vector3 originalRotation = Vector3.zero;

    /// <summary>
    /// Initializes necessary components and variables.
    /// </summary>
    private void Awake()
    {
        // Find the main camera
        if (Camera.main != null) 
            cameraObject = Camera.main.gameObject;
        else
        {
            Debug.LogError("Main camera not found. Make sure the camera is tagged as 'MainCamera'");
        }
        originalRotation = transform.eulerAngles;
    }

    /// <summary>
    /// LateUpdate is called once per frame after all Update functions have been called.
    /// </summary>
    private void LateUpdate()
    {
        // Adjust the object's rotation to face the camera
        LookAt();
    }

    /// <summary>
    /// Adjusts the object's rotation to face the camera based on specified axes.
    /// </summary>
    private void LookAt()
    {
        // Calculate the direction from the object to the camera
        Vector3 direction = transform.position - cameraObject.transform.position;
        // Calculate the new rotation angles using LookRotation
        Vector3 newRotation = Quaternion.LookRotation(direction, transform.up).eulerAngles;

        // Apply rotation based on specified axes
        newRotation.x = _lookX ? newRotation.x : originalRotation.x;
        newRotation.y = _lookY ? newRotation.y : originalRotation.y;
        newRotation.z = _lookZ ? newRotation.z : originalRotation.z;

        // Set the object's rotation using the new angles
        transform.rotation = Quaternion.Euler(newRotation);
    }
}