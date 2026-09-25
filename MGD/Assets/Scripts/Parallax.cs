using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform playerCamera;
    [Range(0, 1)] public float parallaxFactor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector3 lastCameraPosition;
    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main.transform;

        lastCameraPosition = playerCamera.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = playerCamera.position - lastCameraPosition;

        transform.position += new Vector3(deltaMovement.x * parallaxFactor * -1, deltaMovement.y * parallaxFactor, 0);
        lastCameraPosition = playerCamera.position;
    }
}
