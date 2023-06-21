using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float cameraSpeed = 5.0f;
    private bool once = true;
    private bool doneMove = false;
    private float speed = 0f;
    private float angle = 0;
    [HideInInspector] public bool moveCam = false;
    public GameObject bowCameraTo;
    public GameObject thirdPersonCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!doneMove)
        {
            moveToStump();
        }
    }

    void moveToStump()
    {
        if (once)
        {
            angle = Quaternion.Angle(transform.rotation, bowCameraTo.transform.rotation);
            once = false;
        }
        if (moveCam)    //This is to move the camera to stump when searched
        {
            thirdPersonCam.SetActive(false);
            if (Vector3.Distance(transform.position, bowCameraTo.transform.position) >= 0.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, bowCameraTo.transform.position, cameraSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, bowCameraTo.transform.rotation, Time.deltaTime);
            }
            else
            {
                //transform.rotation = Quaternion.Lerp(transform.rotation, bowCameraTo.transform.rotation, 1);

                //Debug.Log("Done");
                //moveCam = false;
                doneMove = true;
            }

        }
        else
        {
            thirdPersonCam.SetActive(true);
        }
    }
}