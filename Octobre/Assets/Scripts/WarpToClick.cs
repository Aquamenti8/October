using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WarpToClick : MonoBehaviour {

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
    void Start()
    {
        BlackUI = GameObject.Find("Black").GetComponent<BlackTransition>();

        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            GameObject.Find("ActionCollider").GetComponent<PickupCircle>().nearWarp = Enter.name;


            if (Input.GetMouseButtonDown(0))
            {
                GameObject.Find("ActionCollider").GetComponent<PickupCircle>().nearWarp = "";
                StartCoroutine("waitThenWarp");
            }

        }
        else if (GameObject.Find("ActionCollider").GetComponent<PickupCircle>().nearWarp == Enter.name)
        {
            GameObject.Find("ActionCollider").GetComponent<PickupCircle>().nearWarp = "";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("ActionCollider").GetComponent<PickupCircle>().nearWarp = "";
        }
    }
    IEnumerator waitThenWarp()
    {
        BlackUI.FadeBlack();
        yield return new WaitForSeconds(0.5f);

        GameObject.Find("Player").transform.position = new Vector2(destinationXX, destinationYX);
        //switch cinemachine
        if (GMto != null) GMto.CenterBGs(SolTo);
        GameObject.Find("ActionCollider").GetComponent<PickupCircle>().nearWarp = "";
        camTo.gameObject.SetActive(true);
        camFrom.gameObject.SetActive(false);
        Quit.SetActive(false);
        Enter.SetActive(true);
    }
}

