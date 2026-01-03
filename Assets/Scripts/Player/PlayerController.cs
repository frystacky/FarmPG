using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static GrowBlock;

public class PlayerController : MonoBehaviour
{
    [Header("Referance to other componets")]
    public Rigidbody2D rb2d;    //rb2d attached to this gameObject
    public InputActionReference moveInput;  //"Player/Move" from InputAction
    public InputActionReference actionInput; //"Player/Attack" from InputAction
    public Animator anim;   //the animator componet on the sprite child object

    [Header("Player movement config")]
    public float moveSpeed;

    //keeps track of what tools the player can use
    public enum ToolType
    {
        plough,
        wateringCan,
        seeds,
        basket
    }

    public ToolType currentTool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIController.instance.SwitchTool((int)currentTool);
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

        bool hasSwitchedTool = false;

        //switches tools. use a inventory belt later
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            currentTool++;

            if ((int)currentTool >= Enum.GetValues(typeof(ToolType)).Length)
            {
                currentTool = ToolType.plough;
            }

            hasSwitchedTool = true;
        }

        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentTool = ToolType.plough;
            hasSwitchedTool = true;
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentTool = ToolType.wateringCan;
            hasSwitchedTool = true;
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            currentTool = ToolType.seeds;
            hasSwitchedTool = true;
        }
        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            currentTool = ToolType.basket;
            hasSwitchedTool = true;
        }

        if (hasSwitchedTool == true)
        {
            UIController.instance.SwitchTool((int)currentTool);
        }

        if (actionInput.action.WasPressedThisFrame())
        {
            UseTool();
        }


        anim.SetFloat("speed", rb2d.linearVelocity.magnitude);
    }

    void UseTool()
    {
        GrowBlock block = null;

        block = FindFirstObjectByType<GrowBlock>();

        //block.PloughSoil();

        if(block != null)
        {
            switch(currentTool)
            {
                case ToolType.plough:

                    block.PloughSoil();

                    break;
                case ToolType.wateringCan:

                    break;
                case ToolType.seeds:

                    break;

                case ToolType.basket:
                    
                    break;
            }
        }
    }


}
