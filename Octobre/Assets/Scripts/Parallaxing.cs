using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {


    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;

    public Transform cam;
    private Vector3 previousCamPos;

    private float parallax;
    private float backgroundTargetPosX;

    // Use this for initialization
    void Start () {

        previousCamPos = cam.position;
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1; //ex:i=3, parallaxScales= -2.34
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (cam.gameObject.activeSelf)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
                bool bound = false;
                for (int j = 0; j < GameObject.FindGameObjectsWithTag("Boundary").Length; j++)
                {
                    Vector3 ViewPos = Camera.main.WorldToViewportPoint(GameObject.FindGameObjectsWithTag("Boundary")[j].transform.position);
                    if (ViewPos.x < 1 && ViewPos.x > 0 && ViewPos.y < 1 && ViewPos.y > 0) // si l'objet est dans la ligne de mire
                    {
                        Debug.Log("I see a Boundary");
                        bound = true;
                    }
                }

                if (!bound)
                {
                    backgroundTargetPosX = backgrounds[i].position.x + parallax;


                    Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

                    backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
                }
            }
            previousCamPos = cam.position;
        }
    }


    public void CenterBGs(GameObject sol)
    {
        //recup la taille de sol, min et max
        //ajuster la place des background, si le backgroundMin est inferieur a solMin, recupere la difference, place BGpos de sorte que BGmin = sol Min

        float solPos = sol.transform.position.x;
        float solMin = solPos - sol.GetComponent<SpriteRenderer>().bounds.size.x/2;
        float solMax = solPos + sol.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position = new Vector3(GameObject.Find("Player").transform.position.x, backgrounds[i].position.y, backgrounds[i].position.z);

            float bgPos = backgrounds[i].position.x;
            float bgMin = bgPos - backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.x / 2;
            float bgMax = bgPos + backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.x / 2;

            if (bgMin < solMin)
            {
                backgrounds[i].position = new Vector3(backgrounds[i].position.x + (solMin - bgMin), backgrounds[i].position.y, backgrounds[i].position.z); 
            }
            else if(bgMax > solMax)
            {
                backgrounds[i].position = new Vector3(backgrounds[i].position.x - (bgMax - solMax), backgrounds[i].position.y, backgrounds[i].position.z);
            }
            
        }
    }
}
