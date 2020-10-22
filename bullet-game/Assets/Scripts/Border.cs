using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] Vector3 topLeftBorder, botRightBorder;
    [SerializeField] bool displayGizmos;

    public bool InBorder(Vector3 pos)
    {
        bool inx = pos.x > topLeftBorder.x && pos.x < botRightBorder.x;
        bool iny = pos.y < topLeftBorder.y && pos.y > botRightBorder.y;
        bool inz = pos.z < topLeftBorder.z && pos.z > botRightBorder.z;
        return inx && iny && inz;
    }

    public void OnDrawGizmos()
    {
        if (displayGizmos)
            return;

        Vector3 tl = topLeftBorder;
        Vector3 br = botRightBorder;
        float size = .15f;

        Vector3[] v = new Vector3[]
        {
            tl,
            new Vector3(br.x, tl.y, tl.z),
            new Vector3(tl.x, br.y, tl.z),
            new Vector3(br.x, br.y, tl.z),
            br,
            new Vector3(tl.x, br.y, br.z),
            new Vector3(br.x, tl.y, br.z),
            new Vector3(tl.x, tl.y, br.z)
        };

        Gizmos.color = Color.magenta;

        Gizmos.DrawLine(v[0], v[1]);
        Gizmos.DrawLine(v[0], v[2]);
        Gizmos.DrawLine(v[2], v[3]); 
        Gizmos.DrawLine(v[3], v[1]);

        Gizmos.DrawLine(v[4], v[5]);
        Gizmos.DrawLine(v[4], v[6]);
        Gizmos.DrawLine(v[6], v[7]);
        Gizmos.DrawLine(v[7], v[5]);

        Gizmos.DrawLine(v[0], v[7]);
        Gizmos.DrawLine(v[1], v[6]);
        Gizmos.DrawLine(v[2], v[5]);
        Gizmos.DrawLine(v[3], v[4]);


        Gizmos.DrawSphere(v[3], size);
        Gizmos.DrawSphere(v[1], size);
        Gizmos.DrawSphere(v[2], size);

        Gizmos.DrawSphere(v[7], size);
        Gizmos.DrawSphere(v[5], size);
        Gizmos.DrawSphere(v[6], size);

        Gizmos.color = Color.yellow;

        Gizmos.DrawSphere(v[0], size * 2);
        Gizmos.DrawSphere(v[4], size * 2);
    }
}
