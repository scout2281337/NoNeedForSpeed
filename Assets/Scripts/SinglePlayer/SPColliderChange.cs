using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPColliderChange : MonoBehaviour
{
    [SerializeField]private CarScriptableObject[] carScriptableObjects;
    private BoxCollider BoxCollider;
    private void Start()
    {
        BoxCollider = GetComponent<BoxCollider>();
    }
    public void ChangeToPantiacCollider() 
    {
        BoxCollider.size = new Vector3(carScriptableObjects[0].SizeX, carScriptableObjects[0].SizeY, carScriptableObjects[0].SizeZ);
        BoxCollider.center = new Vector3(carScriptableObjects[0].CenterX, carScriptableObjects[0].CenterY, carScriptableObjects[0].CenterZ);

    }
    public void ChangeToMustangCollider()
    {
        BoxCollider.size = new Vector3(carScriptableObjects[1].SizeX, carScriptableObjects[1].SizeY, carScriptableObjects[1].SizeZ);
        BoxCollider.center = new Vector3(carScriptableObjects[1].CenterX, carScriptableObjects[1].CenterY, carScriptableObjects[1].CenterZ);

    }
}
