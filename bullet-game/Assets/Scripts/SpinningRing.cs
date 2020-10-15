using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningRing : MonoBehaviour
{
    public enum direction { Right, Up, Forward};
    public float degreesPerFrame;
    public direction spinDirection;
    private Vector3 rotate;
    // Start is called before the first frame update
    void Start()
    {
        switch (spinDirection)
        {
            case direction.Right:
                rotate = Vector3.right;
                break;
            case direction.Forward:
                rotate = Vector3.forward;
                break;
            case direction.Up:
                rotate = Vector3.up;
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(rotate, degreesPerFrame);
    }
}
