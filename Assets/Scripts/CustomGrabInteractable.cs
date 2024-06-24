using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomGrabInteractable : XRGrabInteractable
{
    private int avatarLayer; // A layer do avatar

    protected override void Awake()
    {
        base.Awake();
        avatarLayer = LayerMask.NameToLayer("Body");
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(OnGrab);
        selectExited.AddListener(OnRelease);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectEntered.RemoveListener(OnGrab);
        selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs arg)
    {
        // Ignora colisões com toda a layer "body" quando o objeto é agarrado
        Collider[] objectColliders = GetComponentsInChildren<Collider>();
        foreach (var col in objectColliders)
        {
            Physics.IgnoreLayerCollision(col.gameObject.layer, avatarLayer, true);
        }
    }

    private void OnRelease(SelectExitEventArgs arg)
    {
        // Reativa colisões com a layer "body" quando o objeto é solto
        Collider[] objectColliders = GetComponentsInChildren<Collider>();
        foreach (var col in objectColliders)
        {
            Physics.IgnoreLayerCollision(col.gameObject.layer, avatarLayer, false);
        }
    }
}
