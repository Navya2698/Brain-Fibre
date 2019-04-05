using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ManipulateFibre : MonoBehaviour
{
    public Region_Loader r;
    public Transform[] controllers;
    public VRTK_ControllerEvents[] controllerEvents;
    public Vector3[] start = { Vector3.zero, Vector3.zero };
    public Vector3[] current = { Vector3.zero, Vector3.zero };
    public bool[] active = { false, false, false };
    public Vector3 startPos;
    public Quaternion startRot;
    public Vector3 startScale;
    public Vector3 startOffset;

    void Update()
    {
        var centreWorld = (r.center) * (transform.localScale.x);
        var radiusWorld = (r.Diameter / 2) * (transform.localScale.x);
        if (active[0] && active[1])
        {
            float dist0 = Vector3.Distance(start[0], start[1]);
            float dist1 = Vector3.Distance(current[0], current[1]);

            if (Vector3.Distance(current[0], centreWorld) <= radiusWorld || 
                Vector3.Distance(current[1], centreWorld) <= radiusWorld)
            {
                Debug.Log("Diameter!!");
                Debug.Log(r.Diameter);
                Vector3 mid = (current[0] + current[1]) / 2f;
                transform.position = mid + startOffset * dist1 / dist0;
                transform.localScale = startScale * (dist1 / dist0);
            }
            else
            {
                Debug.Log("Not the Diameter!!");
                transform.localScale = startScale * (dist1 / dist0);
            }

        }
    }
    public void SavePosition()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        startScale = transform.localScale;
        startOffset = transform.position - ((current[0] + current[1]) / 2);
    }
}
