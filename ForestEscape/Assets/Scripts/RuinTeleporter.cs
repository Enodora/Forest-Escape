using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuinTeleporter : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement playerMove;
    public GameObject toTp;
    public GameObject tpFade;
    public Image sr;
    public float fadeTime = 1.0f;

    private float color = 0.0f;
    private bool once = true;
    private bool tpTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = playerMove.GetComponent<PlayerMovement>();
        sr = sr.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tpTrigger)
        {
            tpFade.SetActive(true);
            if (color <= fadeTime && once)
            {
                sr.color = new Color(1, 1, 1, color);
                color += Time.deltaTime / fadeTime;
            }
            else if (color > fadeTime && once)
            {
                player.transform.position = toTp.transform.position;
                once = false;
            }

            if (color >= 0 && !once)
            {
                sr.color = new Color(1, 1, 1, color);
                color -= Time.deltaTime / fadeTime;
            }
            else if (!once)
            {
                playerMove.tp = false;
                tpFade.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        playerMove.tp = true;
        tpTrigger = true;
    }
}
