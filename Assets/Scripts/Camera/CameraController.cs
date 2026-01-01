using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

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
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z); //moves the camera with the player

        //blow blocks of code are used to clamp the camera from going off screen
        Vector3 clampedPosition = transform.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, clampMin.position.x, clampMax.position.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, clampMin.position.y, clampMax.position.y);

        transform.position = clampedPosition;
    }
}
