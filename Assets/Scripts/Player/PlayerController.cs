using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Referance to other componets")]
    public Rigidbody2D rb2d;    //rb2d attached to this gameObject
    public InputActionReference moveInput;  //"Player/Move" from InputAction

    [Header("Player movement config")]
    public float moveSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //adds movement action to rb2d to move player
        rb2d.linearVelocity = moveInput.action.ReadValue<Vector2>().normalized * moveSpeed;
    }
}
