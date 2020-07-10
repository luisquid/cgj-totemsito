using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorIfGuide : MonoBehaviour
{
    public void ChangeColor()
    {
        GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.cyan);
    }
}
