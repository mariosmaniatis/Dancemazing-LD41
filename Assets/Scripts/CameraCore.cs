using UnityEngine;
using System.Collections;

public class CameraCore : MonoBehaviour
{

    private float minX = -60f;
    private float maxX = 60f;
    private float minY = -360f;
    private float maxY = 360f;
    private float rotationY;
    private float rotationX;
    private bool is_dancing;
    public GameObject key;
    public GameObject monster;

    [Range(0.0f, 40.0f)]
    public float sensitivity = 15f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!is_dancing)
        {
            rotationY += Input.GetAxis("Mouse X") * sensitivity;
            rotationX += Input.GetAxis("Mouse Y") * sensitivity;

            rotationX = Mathf.Clamp(rotationX, minX, maxX);

            //transform.localEulerAngles = new Vector3(0, rotationY, 0);
            Camera.main.transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);
        } else
        {
            Camera.main.transform.LookAt(monster.transform);
        }
    }

    private void FixedUpdate()
    {

        is_dancing = key.GetComponent<Dancefloor>().is_dancing;
    }
}
