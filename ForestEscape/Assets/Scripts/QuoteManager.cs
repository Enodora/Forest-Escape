using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuoteManager : MonoBehaviour
{
    public GameObject quote;
    public Text message;
    public PlayerMovement player;
    public SisterMovement sis;
    public float textDelaySpeed = 1.0f;

    private string sister1 = "あら、疲れたの？アナタはあの切り株で休んでて";
    private string sister2 = "お母さんたちには内緒で、この森へ妖精を探しに来てるんだから先を急がなきゃなのに。。。";
    private string sister3 = "それに最近は失踪者が多いって話よ";
    private string sister4 = "私はこの先を下見してくるけど、";
    private string sister5 = "アナタはここを離れないでね";
    private int quoteNum = 1;
    private bool nextQuote = true;


    // Start is called before the first frame update
    void Start()
    {
        quote.SetActive(false);
        message.text = "";

        message = message.GetComponent<Text>();
        sis = sis.GetComponent<SisterMovement>();
        player = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.testing)
        {
            if (sis.speak)
            {
                sayQuote(quoteNum);
            }
        }
    }

    void sayQuote(int num)
    {
        string displayQuote = "";
        switch (num)
        {
            case 1:
                displayQuote = sister1;
                //Debug.Log("sister1: " + num);
                break;
            case 2:
                displayQuote = sister2;
                //Debug.Log("sister2: " + num);
                break;
            case 3:
                displayQuote = sister3;
                //Debug.Log("sister3: " + num);
                break;
            case 4:
                displayQuote = sister4;
                //Debug.Log("sister4: " + num);
                break;
            case 5:
                displayQuote = sister5;
                //Debug.Log("sister5: " + num);
                break;
            default:
                quote.SetActive(false);
                sis.disappear = true;
                break;
        }
            

        if (nextQuote)
        {
            quote.SetActive(true);
            message.text= displayQuote;
            nextQuote = false;
        }

        if(num >= 2)
        {
            player.toLog = true;
        }
    }

    public void PressQuote()
    {
        nextQuote = true;
        quoteNum++;
    }

}
