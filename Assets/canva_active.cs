using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canva_active : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject drone;
    public GameObject vertical_joystick_canvas;
    public GameObject horizontal_joystick_canvas;
    void Start()
    {
        vertical_joystick_canvas.SetActive(false);
        horizontal_joystick_canvas.SetActive(false);
        drone.SetActive(false);
    }

    // Update is called once per frame
    public void start_drone(){
        vertical_joystick_canvas.SetActive(true);
        horizontal_joystick_canvas.SetActive(true);
        drone.SetActive(true);
    }
}
