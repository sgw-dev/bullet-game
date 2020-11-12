using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isSideView;
    bool flipControl;
    public bool isSideRight;

    [SerializeField] Border border;

    [SerializeField] float speed = 10;
    [SerializeField] float accelerationTime = .1f;
    [SerializeField] float maxTiltAngle;

    Vector3 directionalInput;
    Vector3 velocity;
    Vector3 velocitySmoothing;
    Vector3 position;

    Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        flipControl = isSideView;
        position = transform.position;
    }

    public void Update()
    {
        GetInput();
        TiltPlayer();
    }

    void GetInput()
    {
        //not implented yet
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            flipControl = isSideView;
        }

        if (isSideView)
        {
            directionalInput = new Vector3(0, Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));
            if (isSideRight)
                directionalInput.z *= -1;
        }
        else
        {
            directionalInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }
        //print(directionalInput);
    }

    public void FixedUpdate()
    {
        CalculateVelocity();
    }

    void CalculateVelocity()
    {
        Vector3 targetVelocity = directionalInput * speed;
        velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref velocitySmoothing, accelerationTime);

        Vector3 oldPos = position;
        position += velocity * Time.deltaTime;
        //***Spencer Edit
        /*if (!border.InBorder(position))
        {
            velocity = Vector3.zero;
            position = oldPos;
        }*/
        if (!border.InX(position))
        {
            velocity = new Vector3(0.0f, velocity.y, velocity.z);
            position = new Vector3(oldPos.x, position.y, position.z);
        }
        if (!border.InY(position))
        {
            velocity = new Vector3(velocity.x, 0.0f, velocity.z);
            position = new Vector3(position.x, oldPos.y, position.z);
        }
        if (!border.InZ(position))
        {
            velocity = new Vector3(velocity.x, velocity.y, 0.0f);
            position = new Vector3(position.x, position.y, oldPos.z);
        }
        //****End Spencer Edit

        rb.velocity = velocity;
    }

    void TiltPlayer()
    {
        float precent = velocity.x / speed;
        float tiltAmount = precent * maxTiltAngle;
        transform.rotation = Quaternion.Euler(-90 - tiltAmount, -90, 90);
    }
}
