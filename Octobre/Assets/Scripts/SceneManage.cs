using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManage : MonoBehaviour {

    public static SceneManage SceneM;
    public GameObject player;
    public float destinationX;
    public float destinationY;

	// Use this for initialization
	void Start () {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene aScene, LoadSceneMode aMode) {
        Debug.Log("Load! x=" + destinationX + " Y=" + destinationY);
        player = GameObject.Find("Player");
        player.transform.position = new Vector2(destinationX, destinationY);
    }
    

    // Update is called once per frame
    void Update () {
		
	}
}
