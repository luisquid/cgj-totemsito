using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignMaterialProperties : MonoBehaviour
{
    MeshRenderer [] msh;

    // Start is called before the first frame update
    void Start()
    {
        msh = GetComponentsInChildren<MeshRenderer>();  

        for(int i = 0; i < msh.Length - 1; i++)
        {
            msh[i].material.SetFloat("Vector1_9ABEAAF1", i * msh.Length);
        }
    }


}
