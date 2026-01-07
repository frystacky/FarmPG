using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaSwitcher : MonoBehaviour
{
    [Header("Config for AreaSwitcher")]
    public string sceneToLoad;  //the scene to load to
    public Transform loadPoint; //the point/location a player will load from
    public string transitionName;   //set this on both AreaSwitcher objects to load from point A to B and back to A

    private string playerPrefsKey = "Transition";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey(playerPrefsKey)) 
        {
            if (PlayerPrefs.GetString(playerPrefsKey) == transitionName)
            {
                PlayerController.instance.transform.position = loadPoint.position;
            }
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene(sceneToLoad);

            PlayerPrefs.SetString(playerPrefsKey, transitionName);
        }
    }
}
