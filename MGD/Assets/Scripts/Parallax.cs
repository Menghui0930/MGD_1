using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform playerCamera;
    [Range(0, 1)] public float parallaxFactorX;
    [Range(0, 1)] public float parallaxFactorY;
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

        transform.position += new Vector3(deltaMovement.x * parallaxFactorX * -1, deltaMovement.y * parallaxFactorY * -1, 0);
        lastCameraPosition = playerCamera.position;
    }
}
