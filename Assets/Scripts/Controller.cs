using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;
public enum Devices { LeftController = 0, RightController = 1 }
public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public ManipulateFibre mnScript;
    public Devices device;
    VRTK_ControllerEvents controller;
    public bool active = false;
    void Start()
    {
        controller = GetComponent<VRTK_ControllerEvents>();
        controller.TriggerClicked += HandleTriggerClicked;
        controller.TriggerUnclicked += HandleTriggerUnclicked;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.triggerClicked)
        {
            mnScript.current[(int)device] = transform.position;
        }
    }
    void HandleTriggerClicked(object sender, ControllerInteractionEventArgs e)
    {
        mnScript.active[(int)device] = true;
        mnScript.start[(int)device] = transform.position;
        mnScript.current[(int)device] = transform.position;
        mnScript.SavePosition();
    }

    void HandleTriggerUnclicked(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("Trigger pressed");
        mnScript.active[(int)device] = false;
        mnScript.active[2] = false;
    }

}
