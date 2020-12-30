using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMover : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z <= end.position.z)
        {
            transform.position = start.position;
            transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
        else
        {
            transform.Translate(new Vector3(0,0,-speed*Time.deltaTime));
        }
    }
}
