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

    [Header("BlockImage Config")]
    public SpriteRenderer sr;
    public Sprite soilTilled, soilWatered;

    [Header("CropImage Config")]
    public SpriteRenderer cropSR;
    public Sprite cropPlanted, cropGrowing1, cropGrowing2, cropRipe;

    [Header("Block config")]
    public bool isWatered;
    public bool preventUse;

    private Vector2Int gridPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            AdvanceCrop();
        }
#endif

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
            if (isWatered == true)
            {
                sr.sprite = soilWatered;
            }
            else
            {
                sr.sprite = soilTilled;
            }
        }

        UpdateGridInfo();

    }

    public void PloughSoil()
    {
        if(currentStage == GrowthStage.barren && preventUse == false)
        {
            currentStage = GrowthStage.ploughed;

            SetSoilSprite();
        }
    }

    public void WaterSoil()
    {
        if (preventUse == false)
        {
            isWatered = true;

            SetSoilSprite();
        }
    }

    public void PlantCrop()
    {
        if (currentStage == GrowthStage.ploughed && isWatered == true && preventUse == false)
        {
            currentStage = GrowthStage.planted;

            UpdateCropSprite();
        }
    }

    public void UpdateCropSprite()
    {
        switch (currentStage)
        {
            case GrowthStage.planted:

                cropSR.sprite = cropPlanted;

                break;

            case GrowthStage.growing1:

                cropSR.sprite = cropGrowing1;

                break;

            case GrowthStage.growing2:

                cropSR.sprite = cropGrowing2;

                break;

            case GrowthStage.ripe:

                cropSR.sprite = cropRipe;

                break;
        }

        UpdateGridInfo();

    }

    public void AdvanceCrop()
    {
        if (isWatered == true && preventUse == false)
        {
            if (currentStage == GrowthStage.planted || currentStage == GrowthStage.growing1 || currentStage == GrowthStage.growing2)
            {
                currentStage++;

                isWatered = false;
                SetSoilSprite();
                UpdateCropSprite();
            }
        }
    }

    public void HarvestCrop()
    {
        if(currentStage == GrowthStage.ripe && preventUse == false)
        {
            currentStage = GrowthStage.ploughed;
            
            SetSoilSprite();
            cropSR.sprite = null;
        }
    }

    public void SetGridPosition(int x, int y)
    {
        gridPosition = new Vector2Int(x, y);
    }


    public void UpdateGridInfo()
    {
        GridInfo.instance.UpdateInfo(this, gridPosition.x, gridPosition.y);
    }
}
