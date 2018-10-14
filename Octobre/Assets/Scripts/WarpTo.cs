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

    // Use this for initialization
    void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            GameObject.Find("Player").transform.position = new Vector2 (destinationXX, destinationYX);
            //switch cinemachine
            camTo.gameObject.SetActive(true) ;
            camFrom.gameObject.SetActive(false);
            Quit.SetActive(false);
            Enter.SetActive(true);

            GMto.CenterBGs(SolTo);
        }
    }
}
