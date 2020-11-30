using UnityEngine;

public class CameraDrivingFollow : MonoBehaviour
{
    [SerializeField] private GameObject targetCameraPosition = default;
    [SerializeField] private GameObject targetLookAt = default;

    private void LateUpdate()
    {
        transform.position = targetCameraPosition.transform.position;
        transform.rotation = Quaternion.LookRotation(targetLookAt.transform.position - transform.position, Vector3.back);
    }
}
