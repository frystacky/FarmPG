using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GridController : MonoBehaviour
{
    public static GridController instance;

    [Header("Referance to other componets")]
    public Transform minPoint;  //min point of our grid
    public Transform maxPoint;  //max point of our grid
    public GrowBlock baseGridBlock; //the base copy of the grow block

    [Header("List of Lists of grid objects")]
    public List<BlockRow> blockRows = new List<BlockRow>();

    [Header("Layer to stop grid blocks from spawning")]
    public LayerMask gridBlocker;

    private Vector2Int gridSize; //used to figure out the size of the grid

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateGrid()
    {
        minPoint.position = new Vector3(Mathf.Round(minPoint.position.x), Mathf.Round(minPoint.position.y), 0f);
        maxPoint.position = new Vector3(Mathf.Round(maxPoint.position.x), Mathf.Round(maxPoint.position.y), 0f);

        Vector3 startPoint = minPoint.position + new Vector3(.5f, .5f, 0f); //off set because grow block will be in center of 4 grid spots

        //Instantiate(baseGridBlock, startPoint, Quaternion.identity);

        //gets the size of the Grid max - min for x and y
        gridSize = new Vector2Int(Mathf.RoundToInt(maxPoint.position.x - minPoint.position.x),
            Mathf.RoundToInt(maxPoint.position.y - minPoint.position.y));

        for (int y = 0; y < gridSize.y; y++)
        {
            blockRows.Add(new BlockRow());

            for (int x = 0; x < gridSize.x; x++)
            {
                GrowBlock newblock = Instantiate(baseGridBlock, startPoint + new Vector3(x, y, 0f), Quaternion.identity);

                newblock.transform.SetParent(transform);
                newblock.sr.sprite = null;

                newblock.SetGridPosition(x, y);

                blockRows[y].blocks.Add(newblock);

                if(Physics2D.OverlapBox(newblock.transform.position, new Vector2(.9f, .9f), 0f, gridBlocker))
                {
                    newblock.sr.sprite = null;
                    newblock.preventUse = true;
                }

                if(GridInfo.instance.hasGrid == true)
                {
                    BlockInfo storeBlock = GridInfo.instance.theGrid[y].blocks[x];

                    newblock.currentStage = storeBlock.currentStage;
                    newblock.isWatered = storeBlock.isWatered;

                    newblock.SetSoilSprite();
                    newblock.UpdateCropSprite();
                }
            }
        }

        if(GridInfo.instance.hasGrid == false)
        {
            GridInfo.instance.CreateGrid();
        }

        baseGridBlock.gameObject.SetActive(false);
    }

    //x and y of the world that we want to get info about
    public GrowBlock GetBlock(float x, float y)
    {
        //round to int since grind values are on whole numbers
        x = Mathf.RoundToInt(x); 
        y = Mathf.RoundToInt(y);

        //relative to the start point of the grid
        x -= minPoint.position.x;
        y -= minPoint.position.y;

        //convert to int so we can check in our list
        int intX = Mathf.RoundToInt(x);
        int intY = Mathf.RoundToInt(y);

        if(intX < gridSize.x && intY < gridSize.y)
        {
            return blockRows[intY].blocks[intX];
        }

        return null;
    }

}

[System.Serializable]
public class BlockRow
{
    public List<GrowBlock> blocks = new List<GrowBlock>();
}
