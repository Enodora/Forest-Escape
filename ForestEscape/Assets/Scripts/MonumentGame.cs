using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentGame : MonoBehaviour
{
    private GameObject[] childMonu = new GameObject[4];
    private int count = 0;
    private bool outside = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            childMonu[i] = this.gameObject.transform.parent.transform.parent.transform.parent.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (outside)
            {
                if (count < 3)
                {
                    outside = false;
                    childMonu[count].SetActive(false);
                    Debug.Log("From: " + childMonu[count].name);
                    count++;
                    childMonu[count].SetActive(true);
                    Debug.Log("To: " + childMonu[count].name);
                }
                else
                {
                    outside = false;
                    childMonu[count].SetActive(false);
                    Debug.Log("From: " + childMonu[count].name);
                    count = 0;
                    childMonu[count].SetActive(true);
                    Debug.Log("To: " + childMonu[count].name);
                }
            }
            
            //Debug.Log(this.gameObject.transform.parent.transform.parent.transform.parent.name);
        }
    }

    void OnTriggerExit(Collider col)
    {
        outside = true;
    }
}
