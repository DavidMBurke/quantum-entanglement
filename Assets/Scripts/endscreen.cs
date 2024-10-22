using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endscreen : MonoBehaviour
{

    public GameObject endscreenPrefab;
    public GameObject door1;
    public GameObject door2;
    public DoorController door1Controller;
    public DoorController door2Controller;

    public void checkOpen()
    {
        print("checkOpen()");
        print(door1Controller.IsOpen);
        print(door2Controller.IsOpen);
        if (door1Controller.IsOpen && door2Controller.IsOpen)
        {
            endscreenPrefab.SetActive(true);
        }
    }



}
