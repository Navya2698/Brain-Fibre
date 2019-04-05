using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class Region_Loader : MonoBehaviour
{
    public string fileName;
    public string visit;
    public GameObject lineObj;
    public Transform lineParent;
    public Region r;
    public List<GameObject> lines;
    public Vector3 min = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
    public Vector3 max = new Vector3(-Mathf.Infinity, -Mathf.Infinity, -Mathf.Infinity);
    public float Diameter;
    public Vector3 center;
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Loading Region: " + fileName);
            r = readFile();
            instantiateObjects(r);
        }
    }

    Region readFile()
    {
        Debug.Log(Application.streamingAssetsPath);
        string filePath = Application.streamingAssetsPath + "/" + fileName;
        return JsonUtility.FromJson<Region>(File.ReadAllText(filePath));
    }

    void instantiateObjects(Region r)
    {
        if(lineParent == null)
        {
            var line = new GameObject("Lines");
            line.transform.SetParent(transform);
            line.transform.localPosition = Vector3.zero;
            line.transform.localRotation = Quaternion.identity;
            line.transform.localScale = Vector3.one;
            lineParent = line.transform;
        }
        lines = instantiateLines(r);
    }
    List<GameObject> instantiateLines(Region r)
    {
        int k = 0;
        List<GameObject> lineList = new List<GameObject>();
        for(int i = 0; i <(int)r.lineLengths.Length; i++)
        {
            GameObject line = Instantiate(lineObj, lineParent, false);
            var lr = line.GetComponent<LineRenderer>();
            //lr.widthMultiplier = 10;
            var points = new Vector3[((int)r.lineLengths[i])];
            for(int j = (int)r.lineOffsets[i]; 
                j <(int)r.lineLengths[i]+ (int)r.lineOffsets[i]; j++)
            {
                points[k].x = r.x[j];
                points[k].y = r.y[j];
                points[k].z = r.z[j];
                k++;
                min.x = (min.x > r.x[j]) ? r.x[j] : min.x;
                min.y = (min.y > r.y[j]) ? r.y[j] : min.y;
                min.z = (min.z > r.z[j]) ? r.z[j] : min.z;
                max.x = (max.x < r.x[j]) ? r.x[j] : max.x;
                max.y = (max.y < r.y[j]) ? r.y[j] : max.y;
                max.z = (max.z < r.z[j]) ? r.z[j] : max.z;
            }
            line.name = " L: " + r.lineOffsets[i] + " - " + (r.lineOffsets[i] + r.lineLengths[i]-1);
            lr.positionCount = points.Length;
            lr.SetPositions(points);
            lineList.Add(line);
            k = 0;
        }
        Diameter = Vector3.Distance(min, max);
        center = new Vector3((min.x + max.x) / 2, (min.y + max.y) / 2, (min.z + max.z) / 2); //todo the point in the cluster, average of every point
        return lineList;
    }

}

