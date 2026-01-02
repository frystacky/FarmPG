using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    private Camera cam;
    private float halfWidth, halfHeight;

    [Header("Referance to other componets")]
    public Transform clampMin;  //Child objects that stop the camera from moving off screen
    public Transform clampMax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindAnyObjectByType<PlayerController>().transform;

        //unparent the objects else they follow the camera
        clampMin.SetParent(null);
        clampMax.SetParent(null);

        cam = GetComponent<Camera>();
        halfHeight = cam.orthographicSize;
        halfWidth = cam.orthographicSize * cam.aspect; // 16/9 is the aspect
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z); //moves the camera with the player

        //blow blocks of code are used to clamp the camera from going off screen
        Vector3 clampedPosition = transform.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, clampMin.position.x + halfWidth, clampMax.position.x - halfWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight);

        transform.position = clampedPosition;
    }
}
