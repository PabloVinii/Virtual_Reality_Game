using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomGrabInteractable : XRGrabInteractable
{
    [SerializeField]
    private List<Collider> avatarCollidersToIgnore = new List<Collider>(); // Lista de colisores do avatar para ignorar

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
        // Ignora colisão com os colisores especificados quando o objeto é agarrado
        Collider[] objectColliders = GetComponentsInChildren<Collider>();
        foreach (var objectCollider in objectColliders)
        {
            foreach (var avatarCollider in avatarCollidersToIgnore)
            {
                Physics.IgnoreCollision(objectCollider, avatarCollider, true);
            }
        }
    }

    private void OnRelease(SelectExitEventArgs arg)
    {
        // Reativa colisão com os colisores especificados quando o objeto é solto
        Collider[] objectColliders = GetComponentsInChildren<Collider>();
        foreach (var objectCollider in objectColliders)
        {
            foreach (var avatarCollider in avatarCollidersToIgnore)
            {
                Physics.IgnoreCollision(objectCollider, avatarCollider, false);
            }
        }
    }
}
