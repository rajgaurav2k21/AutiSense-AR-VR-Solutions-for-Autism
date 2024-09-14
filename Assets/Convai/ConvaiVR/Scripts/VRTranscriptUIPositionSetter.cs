using Convai.Scripts.Runtime.Core;
using UnityEngine;

/// <summary>
/// Positions the VR transcript UI relative to the active ConvaiNPC based on appearance changes.
/// </summary>
public class VRTranscriptUIPositionSetter : MonoBehaviour
{
    // Offset for positioning the UI relative to the ConvaiNPC
    [SerializeField] private Vector3 _offset;

    /// <summary>
    /// Subscribes to events when the script is enabled.
    /// </summary>
    private void OnEnable()
    {
        ConvaiNPCManager.Instance.OnActiveNPCChanged += ConvaiNPCManager_OnActiveNPCChanged;
    }

    /// <summary>
    /// Unsubscribes from events to prevent issues when the script is disabled.
    /// </summary>
    private void OnDisable()
    {
        ConvaiNPCManager.Instance.OnActiveNPCChanged -= ConvaiNPCManager_OnActiveNPCChanged;
    }

    /// <summary>
    /// Updates the position of the current UI when the active ConvaiNPC changes.
    /// </summary>
    /// <param name="convaiNpc">The newly active ConvaiNPC.</param>
    private void ConvaiNPCManager_OnActiveNPCChanged(ConvaiNPC convaiNPC)
    {
        if (convaiNPC == null)
        {
            return;
        }
        
        UpdateCurrentUIPosition(convaiNPC);
    }

    /// <summary>
    /// Updates the position of the current UI based on the location and offset of the active ConvaiNPC.
    /// </summary>
    /// <param name="convaiNPC">The active ConvaiNPC.</param>
    private void UpdateCurrentUIPosition(ConvaiNPC convaiNPC)
    {
        Transform npcTransform = convaiNPC.transform;
        Vector3 targetOffset = _offset.x * npcTransform.right + _offset.y * npcTransform.up + _offset.z * npcTransform.forward;
        transform.position = npcTransform.position + targetOffset;
    }
}