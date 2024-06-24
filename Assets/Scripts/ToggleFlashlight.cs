using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleFlashlight : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] private Light flashlight;
    private XRGrabInteractable grabInteractable;
    private bool isLightOn = false;
    private bool isHeld = false;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    void Update()
    {
        if (isHeld && triggerAction.action.triggered)
        {
            ToggleLight();
        }
    }

    private void OnGrab(SelectEnterEventArgs arg)
    {
        isHeld = true;
    }

    private void OnRelease(SelectExitEventArgs arg)
    {
        isHeld = false;
    }

    private void ToggleLight()
    {
        isLightOn = !isLightOn;
        flashlight.enabled = isLightOn;
    }
}
