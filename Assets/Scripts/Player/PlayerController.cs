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
    public Transform toolIndicator; //the tool indicator child object on the player

    [Header("Player movement config")]
    public float moveSpeed;

    [Header("Player tool config")]
    public float toolWaitTime = .5f;
    private float toolWaitCounter;
    public float toolRange = 3f;

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
        if (toolWaitCounter > 0)
        {
            toolWaitCounter -= Time.deltaTime;
            rb2d.linearVelocity = Vector2.zero;
        }
        else
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

        //logic for tool indicator to keep it at tool range and snap to grid
        toolIndicator.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        toolIndicator.position = new Vector3(toolIndicator.position.x, toolIndicator.position.y, 0f);

        if(Vector3.Distance(toolIndicator.position, transform.position) > toolRange)
        {
            Vector2 direction = toolIndicator.position - transform.position;
            direction = direction.normalized * toolRange;
            toolIndicator.position = transform.position + new Vector3(direction.x, direction.y, 0f);
        }

        //dont forget to add grid offset
        toolIndicator.position = new Vector3(Mathf.FloorToInt(toolIndicator.position.x) + .5f,
            Mathf.FloorToInt(toolIndicator.position.y) + .5f, 0f); //end of tool indicator logic
    }

    void UseTool()
    {
        GrowBlock block = null;

        block = GridController.instance.GetBlock(toolIndicator.position.x -.5f, toolIndicator.position.y -.5f);

        //block.PloughSoil();

        toolWaitCounter = toolWaitTime;

        if(block != null)
        {
            switch(currentTool)
            {
                case ToolType.plough:

                    block.PloughSoil();

                    anim.SetTrigger("usePlough");

                    break;

                case ToolType.wateringCan:

                    anim.SetTrigger("useWateringCan");

                    block.WaterSoil();

                    break;

                case ToolType.seeds:

                    block.PlantCrop();

                    break;

                case ToolType.basket:
                    
                    block.HarvestCrop();

                    break;
            }
        }
    }


}
