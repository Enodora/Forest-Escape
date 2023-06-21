using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SisterMovement : MonoBehaviour
{
    public GameObject self;
    public GameObject initialSpawn;
    public GameObject walkUntil;
    public GameObject walkUntilDisappear;
    public float speed = 1.0f;
    public PlayerMovement player;

    private Animator anim;
    private bool endFirstMove = false;
    [HideInInspector]public bool speak = false;
    [HideInInspector] public bool disappear = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        player = player.GetComponent<PlayerMovement>();

        transform.position = initialSpawn.transform.position;
        anim.Play("sister_right");
    }

    // Update is called once per frame
    void Update()
    {
        firstMovement();
        secondMovement();
    }

    void firstMovement()
    {
        if (Vector3.Distance(transform.position, walkUntil.transform.position) >= 1f && !endFirstMove)
        {
            anim.Play("sister_right");
            transform.position = Vector3.MoveTowards(transform.position, walkUntil.transform.position, speed * Time.deltaTime);
        }
        else if(!endFirstMove)
        {
            endFirstMove = true;
            anim.Play("sister_left");
            speak = true;
        }
    }

    void secondMovement()
    {
        if (disappear)
        {
            if (Vector3.Distance(transform.position, walkUntilDisappear.transform.position) >= 1f)
            {
                anim.Play("sister_right");
                transform.position = Vector3.MoveTowards(transform.position, walkUntilDisappear.transform.position, speed * Time.deltaTime);
            }
            else
            {
                self.SetActive(false);
                player.move = true;
            }
        }
    }
}
