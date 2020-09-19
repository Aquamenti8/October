using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryAppear : MonoBehaviour {

    // Use this for initialization

    //Quand on est a une distance inferieure a 2 de cleo la bibliotheque apparrait (coroutine lente)
    //Quand on est sup ca disparait
    // APPARAIT = lib3, lib 5 , lib 6, lib 1 disparait lib 2
    public SpriteRenderer lib1;
    public SpriteRenderer lib2;
    public SpriteRenderer lib3;
    public SpriteRenderer lib5;
    public SpriteRenderer lib6;
    public GameObject Cleo;
    public GameObject Player;

    bool AppearLaunch;
    bool DispearLaunch;
    float alpha = 1;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector2.Distance(Cleo.transform.position, Player.transform.position) < 1 && !AppearLaunch)
        {
            StopCoroutine("Dispear");
            StartCoroutine("Appear");
            AppearLaunch = true;
            DispearLaunch = false;
        }

        if (Vector2.Distance(Cleo.transform.position, Player.transform.position) > 1 && !DispearLaunch)
        {
            StopCoroutine("Appear");
            StartCoroutine("Dispear");
            AppearLaunch = false;
            DispearLaunch = true;
        }

    }

    IEnumerator Appear()
    {
        //Dimiue l'alpha
        while (alpha > 0)
        {
            alpha -= 2*Time.deltaTime;
            lib1.color = new Color(1, 1, 1, 1-alpha);
            lib3.color = new Color(1, 1, 1, 1 - alpha);
            lib5.color = new Color(1, 1, 1, 1 - alpha);
            lib6.color = new Color(1, 1, 1, 1 - alpha);
            lib2.color = new Color(1, 1, 1, alpha);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        AppearLaunch = false;
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator Dispear()
    {
        //Dimiue l'alpha
        while (alpha < 1)
        {
            alpha += 2 * Time.deltaTime;
            lib1.color = new Color(1, 1, 1, 1 - alpha);
            lib3.color = new Color(1, 1, 1, 1 - alpha);
            lib5.color = new Color(1, 1, 1, 1 - alpha);
            lib6.color = new Color(1, 1, 1, 1 - alpha);
            lib2.color = new Color(1, 1, 1, alpha);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        AppearLaunch = false;
        yield return new WaitForSeconds(0.1f);
    }
}
