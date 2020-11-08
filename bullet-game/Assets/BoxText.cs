using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Entered Trigger box");
    }
}
