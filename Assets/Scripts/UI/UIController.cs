using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;    //create a singleton

    [Header("Referance to other componets/objects")]
    public GameObject[] toolbarActivatorIcons;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchTool(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchTool(int selected)
    {
        foreach (GameObject icon in toolbarActivatorIcons)
        {
            icon.SetActive(false);
        }

        toolbarActivatorIcons[selected].SetActive(true);
    }
    
}
