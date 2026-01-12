using Unity.VisualScripting;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    public float currnetTime;
    public float dayStart, dayEnd;
    public float timeSpeed = .25f;
    public int currentDay = 1;

    private bool timeActive;

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
        currnetTime = dayStart;
        timeActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeActive)
        {
            currnetTime += Time.deltaTime * timeSpeed;

            if (currnetTime > dayEnd)
            {
                currnetTime = dayEnd;
                EndDay();
            }

            if (UIController.instance != null)
            {
                UIController.instance.UpdateTimeText(currnetTime);
            }
        }
    }

    public void EndDay()
    {
        timeActive = false;
        currentDay++;

        StartDay();
    }
    
    public void StartDay()
    {
        timeActive = true;
        currnetTime = dayStart;

        GridInfo.instance.GrowCrop();
    }
}
