using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class WarpTo : MonoBehaviour {

    [SerializeField] private string newLevel;
    public float destinationXX = 0f;
    public float destinationYX = 0f;
    public CinemachineVirtualCamera camTo;
    public CinemachineVirtualCamera camFrom;
    public GameObject Quit;
    public GameObject Enter;
    public Parallaxing GMto;
    public GameObject SolTo;

    private BlackTransition BlackUI;

    // Use this for initialization
    void Start () {
        BlackUI = GameObject.Find("Black").GetComponent<BlackTransition>();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            BlackUI.FadeBlack();

            StartCoroutine("waitThenWarp");

            
        }
    }
    IEnumerator waitThenWarp()
    {

        yield return new WaitForSeconds(0.5f);
        GameObject.Find("Player").transform.position = new Vector2(destinationXX, destinationYX);
        //switch cinemachine
        if (GMto != null) GMto.CenterBGs(SolTo);
        camTo.gameObject.SetActive(true);
        camFrom.gameObject.SetActive(false);
        Quit.SetActive(false);
        Enter.SetActive(true);
    }
}
