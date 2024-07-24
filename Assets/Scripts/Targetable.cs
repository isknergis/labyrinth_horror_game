using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Targetable : MonoBehaviour
{
    public GameObject InfoObject;

    public void ToggleHighlight(bool status)
    {
        InfoObject.SetActive(status);   
    }

}
