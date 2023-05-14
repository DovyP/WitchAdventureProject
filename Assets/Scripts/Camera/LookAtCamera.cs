using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // look at camera settings/modes
    private enum LookAtMode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted,
    }

    [SerializeField] private LookAtMode lookAtMode;

    // late update better for camera
    private void LateUpdate()
    {
        switch (lookAtMode)
        {
            case LookAtMode.LookAt:
                // this is cached on default by unity, so no performance hit
                transform.LookAt(Camera.main.transform);
                break;
            case LookAtMode.LookAtInverted:
                Vector3 directionFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + directionFromCamera);
                break;
            case LookAtMode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case LookAtMode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
