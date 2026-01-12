using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;    //create a singleton

    [Header("Referance to other componets/objects")]
    public GameObject[] toolbarActivatorIcons;
    public TMP_Text timeText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

    //formats time displaying on UI
    public void UpdateTimeText(float curTime)
    {
        if (curTime < 12)
        {
            timeText.text = Mathf.FloorToInt(curTime) + "AM";
        }
        else if (curTime < 13)
        {
            timeText.text = "12PM";
        }
        else if (curTime < 24)
        {
            timeText.text = Mathf.FloorToInt(curTime - 12) + "PM";
        }
        else if (curTime < 25)
        {
            timeText.text = "12AM";
        }
        else
        {
            timeText.text = Mathf.FloorToInt(curTime - 24) + "AM";
        }
    } 
}
