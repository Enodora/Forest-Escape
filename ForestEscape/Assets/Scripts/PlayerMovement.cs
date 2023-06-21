using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Joystick joystick;
    public GameObject initialSpawn;
    public GameObject walkUntil;
    public GameObject walkUntilLog;
    public float speed = 6f;
    public float autoSpeed = 0.5f;

    public bool testing = false;

    private Vector3 velocity;
    private Animator anim;
    private bool autoEnd = false;
    [HideInInspector] public bool toLog = false;
    [HideInInspector] public bool move = false;
    [HideInInspector] public bool tp = false;
    [SerializeField] private float gravity = 5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();

        if (!testing)
        {
            transform.position = initialSpawn.transform.position;
        }
    }

    void Update()
    {
        if (!testing)
        {
            if (Vector3.Distance(transform.position, walkUntil.transform.position) >= 1f && !autoEnd)
            {
                anim.Play("player_right");
                transform.position = Vector3.MoveTowards(transform.position, walkUntil.transform.position, autoSpeed * Time.deltaTime);
            }
            else if (!autoEnd)
            {
                autoEnd = true;
            }
            else if (toLog && autoEnd && Vector3.Distance(transform.position, walkUntilLog.transform.position) >= 0.1f && !move)
            {
                anim.Play("player_back");
                transform.position = Vector3.MoveTowards(transform.position, walkUntilLog.transform.position, autoSpeed * Time.deltaTime);
            }
            else if (!move && Vector3.Distance(transform.position, walkUntilLog.transform.position) <= 0.1f)
            {
                anim.Play("player_front");
            }
            playerController();
        }

        if (testing)
        {
            playerController();
        }
    }

    /// <summary>
    /// ジョイスティックのY軸の値からX軸の値を引いてプレイヤーの向いている方向を設定する
    /// </summary>
    /// <param name="horizontal">ジョイスティックのX軸の値</param>
    /// <param name="vertical">ジョイスティックのY軸の値</param>
    private void playerFacing(float horizontal, float vertical)
    {
        if (horizontal > 0 && vertical > 0) //Quadrant I
        {
            if (vertical - horizontal > 0)
            {
                anim.Play("player_back");
            }
            else
            {
                anim.Play("player_right");
            }
        }
        else if (horizontal > 0 && vertical < 0) //Quadrant IV
        {
            if (-vertical - horizontal > 0)
            {
                anim.Play("player_front");
            }
            else
            {
                anim.Play("player_right");
            }
        }
        else if (horizontal < 0 && vertical > 0) //Quadrant II
        {
            if (vertical - -horizontal > 0)
            {
                anim.Play("player_back");
            }
            else
            {
                anim.Play("player_left");
            }
        }
        else if (horizontal < 0 && vertical < 0) //Quadrant III
        {
            if (-vertical - -horizontal > 0)
            {
                anim.Play("player_front");
            }
            else
            {
                anim.Play("player_left");
            }
        }
    }

    public void playerController()
    {
        if ((move && !tp) || testing)
        {
            float horizontal = joystick.Horizontal;
            float vertical = joystick.Vertical;
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            //Debug.Log("horizontal: " + horizontal);
            //Debug.Log("vertical: " + vertical);

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

                playerFacing(horizontal, vertical);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * ((speed * Mathf.Abs(horizontal) + speed * Mathf.Abs(vertical)) / 2) * Time.deltaTime);
            }

            if (controller.isGrounded)
            {
                velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            }
            velocity.y += Physics.gravity.y * Time.deltaTime;
            controller.Move(velocity * gravity * Time.deltaTime);
        }
    }
}
