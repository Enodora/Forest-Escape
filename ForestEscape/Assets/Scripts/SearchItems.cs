using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SearchItems : MonoBehaviour
{
    public MenuButtonManager mbm;
    public CameraManager cam;

    private string flowerThis = "";
    private int yellowNum = 0;
    private int flowerNum = 0;
    private string flowerRightS = "";
    private string flowerLeftS = "";
    private string flowerUpS = "";
    private string flowerDownS = "";
    private bool flowerYellow = false;
    private bool enterFlower = false;

    private bool enterBow = false;

    private GameObject currentFlower;
    private GameObject flowerRight;
    private GameObject flowerLeft;
    private GameObject flowerUp;
    private GameObject flowerDown;

    // Start is called before the first frame update
    void Start()
    {
        mbm = GameObject.Find("ButtonManager").GetComponent<MenuButtonManager>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraManager>();

        InvokeRepeating("Search", 0.001f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        if (enterFlower && mbm.search)
        {
            FlowerTrigger();
        }else if (enterBow && mbm.search)
        {
            BowTrigger();
        }

        //---------------------------------------------------------------------------------
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, 100) && Physics.Raycast(ray, out raycastHit))
        {
            //Debug.Log("a");
            if (raycastHit.collider.CompareTag("Apple"))
            {
                Debug.Log("Apple");
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        
        if (col.tag == "Flower")
        {
            currentFlower = col.gameObject;
            flowerThis = col.gameObject.name;
            flowerNum = int.Parse(flowerThis);
            enterFlower = true;
        }else if (col.tag == "Bow")
        {
            enterBow = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Flower")
        {
            enterFlower = false;
        }else if (col.tag == "Bow")
        {
            enterBow = false;
        }
    }

    void Search()
    {
        mbm.search = false;
    }

    void FlowerTrigger()
    {
        if (!flowerYellow)
        {
            flowerRightS = "" + (flowerNum + 1);
            flowerLeftS = "" + (flowerNum - 1);
            flowerUpS = "" + (flowerNum + 10);
            flowerDownS = "" + (flowerNum - 10);

            changeFlower(currentFlower);
            try { flowerRight = GameObject.Find(flowerRightS); changeFlower(flowerRight); } catch (Exception e) { Debug.Log("Error 1: "+ e); }
            try { flowerLeft = GameObject.Find(flowerLeftS); changeFlower(flowerLeft); } catch (Exception e) { Debug.Log("Error 2: "+ e); }
            try { flowerUp = GameObject.Find(flowerUpS); changeFlower(flowerUp); } catch (Exception e) { Debug.Log("Error 3: "+ e); }
            try { flowerDown = GameObject.Find(flowerDownS); changeFlower(flowerDown); } catch (Exception e) { Debug.Log("Error 4: "+ e); }

            mbm.search = false;

            Transform parent = GameObject.Find("Flowers").transform;
            yellowNum = 0;
            foreach (Transform child in parent)
            {
                if (child.gameObject.transform.Find("flower04").gameObject.activeSelf)
                {
                    yellowNum++;
                }
            }
            if (yellowNum == 16)
            {
                flowerYellow = true;
                Debug.Log("done");
            }
        }
        
        /**
         * 30 31 32 33
         * 20 21 22 23
         * 10 11 12 13
         * 00 01 02 03
         */
    }

    private void changeFlower(GameObject flower)
    {
        if (flower.gameObject.transform.Find("flower03").gameObject.activeSelf)
        {
            flower.gameObject.transform.Find("flower03").gameObject.SetActive(false);
            flower.gameObject.transform.Find("flower04").gameObject.SetActive(true);
        }
        else if (flower.gameObject.transform.Find("flower04").gameObject.activeSelf)
        {
            flower.gameObject.transform.Find("flower03").gameObject.SetActive(true);
            flower.gameObject.transform.Find("flower04").gameObject.SetActive(false);
        }
    }

    void BowTrigger()
    {
        cam.moveCam = true;
    }
}
