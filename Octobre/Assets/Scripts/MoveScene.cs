using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    [SerializeField] private string newLevel;
    public float destinationXX = 0f;
    public float destinationYX = 0f;

    public SceneManage mySceneScript;

    public GameObject TheReminder;

    private void Start()
    {
        TheReminder = GameObject.Find("TheReminder");
        mySceneScript = GameObject.Find("TheReminder").GetComponent<SceneManage>();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DontDestroyOnLoad(TheReminder);
            mySceneScript.destinationX = destinationXX;
            mySceneScript.destinationY = destinationYX;
            SceneManager.LoadScene(newLevel);
            Destroy(this.gameObject);
 
        }
    }
}