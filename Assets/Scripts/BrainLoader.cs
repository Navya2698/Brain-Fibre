using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BrainLoader : MonoBehaviour
{
    public int noOfRegions;
    public GameObject fibreObj;
    public Transform fibreParent; //Parent nodes transform
    public List<string> fileNames;
    public int index = 0;
    // Update is called once per frame
    void Start()
    {
        string filePath = Application.streamingAssetsPath + "/";
        foreach (string file in System.IO.Directory.GetFiles(filePath))
        {
            fileNames.Add(file.Replace(filePath, ""));
            index++;
        }
        for (int i = 0; i < 1; i++)
        {
            GameObject brainFibre = Instantiate(fibreObj, fibreParent, false);
            Region_Loader r = brainFibre.GetComponent<Region_Loader>();
            r.fileName = fileNames[i];
            noOfRegions++;
            brainFibre.name = "Fibre " + i + " : " + fileNames[i];
            i++;
        }
    }
}