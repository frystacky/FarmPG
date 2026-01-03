using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrowBlock : MonoBehaviour
{
    //keeps track of our growing stages for plants
    public enum GrowthStage
    {
        barren,
        ploughed,
        planted,
        growing1,
        growing2,
        ripe
    }

    public GrowthStage currentStage;

    [Header("Referance to other componets")]
    public SpriteRenderer sr;
    public Sprite soilTilled;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    //used to move the grow block to the next stage
    public void AdvanceStage()
    {
        currentStage = currentStage + 1;

        //wraps the enum back to the start by checking the size
        if((int)currentStage >= Enum.GetValues(typeof(GrowthStage)).Length)
        {
            currentStage = GrowthStage.barren;
        }
    }

    public void SetSoilSprite()
    {
        if (currentStage == GrowthStage.barren)
        {
            sr.sprite = null;
        }
        else 
        {
            sr.sprite = soilTilled;
        }
    }

    public void PloughSoil()
    {
        if(currentStage == GrowthStage.barren)
        {
            currentStage = GrowthStage.ploughed;

            SetSoilSprite();
        }
    }

}
