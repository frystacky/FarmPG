using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [Header("Referance to other componets")]
    public Transform minPoint;  //min point of our grid
    public Transform maxPoint;  //max point of our grid
    public GrowBlock baseGridBlock; //the base copy of the grow block

    [Header("List of Lists of grid objects")]
    public List<BlockRow> blockRows = new List<BlockRow>();

    [Header("Layer to stop grid blocks from spawning")]
    public LayerMask gridBlocker;

    private Vector2Int gridSize; //used to figure out the size of the grid

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

                blockRows[y].blocks.Add(newblock);

                if(Physics2D.OverlapBox(newblock.transform.position, new Vector2(.9f, .9f), 0f, gridBlocker))
                {
                    newblock.sr.sprite = null;
                    newblock.preventUse = true;
                }
            }
        }

        baseGridBlock.gameObject.SetActive(false);
    }
}

[System.Serializable]
public class BlockRow
{
    public List<GrowBlock> blocks = new List<GrowBlock>();
}
