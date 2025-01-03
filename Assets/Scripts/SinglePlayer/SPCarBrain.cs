using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPCarBrain : MonoBehaviour
{
    public GameObject[] headlights;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            TurnOnLights();
        }    
    }
    private void TurnOnLights() 
    {
        foreach (var light in headlights)
        {
            light.SetActive(!light.activeSelf);
        }
    
    }
}
