using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Referance to other componets")]
    public Rigidbody2D rb2d;    //rb2d attached to this gameObject
    public InputActionReference moveInput;  //"Player/Move" from InputAction
    public Animator anim;   //the animator componet on the sprite child object

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

        //flips the local scale if move left or right to have one animation for walking
        if (rb2d.linearVelocity.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb2d.linearVelocity.x > 0f)
        {
            transform.localScale = Vector3.one;
        }

        anim.SetFloat("speed", rb2d.linearVelocity.magnitude);
    }
}
