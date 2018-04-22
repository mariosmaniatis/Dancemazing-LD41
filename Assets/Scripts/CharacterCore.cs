using UnityEngine;
using System.Collections;

public class CharacterCore : MonoBehaviour
{

    public float speed;
    private float speed_fixed;
    private Rigidbody rigid;
    private Vector3 moveDirection;
    public GameObject key;
    public GameObject crosshair;
    public bool crouch;
    public bool run;
    public float sound_making;
    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        speed_fixed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (key.GetComponent<Dancefloor>().is_dancing == false)
        {
            crosshair.SetActive(true);
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            sound_making = gameObject.GetComponent<Rigidbody>().velocity.magnitude;

            //moveDirection = (horizontal * transform.right + vertical * transform.forward).normalized;
            moveDirection = new Vector3(horizontal, 0f, vertical);
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
            moveDirection.y = 0.0f;
            if (Input.GetKeyDown(KeyCode.C))
            {
                Crouch();
            }
        } else
        {
            moveDirection = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            crosshair.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        
        Move();
        if (crouch)
        {
            speed = speed_fixed / 3;
            gameObject.GetComponent<CapsuleCollider>().height = 1;
        } else
        {
            speed = speed_fixed;
            gameObject.GetComponent<CapsuleCollider>().height = 2;
        }
    }

    void Move()
    {
        Vector3 yVelFix = new Vector3(0, rigid.velocity.y, 0);
        rigid.velocity = moveDirection * speed * Time.deltaTime;
        rigid.velocity += yVelFix;
    }

    void Crouch()
    {
        if(crouch)
        {
            crouch = false;
        } else
        {
            crouch = true;
        }
    }
}
