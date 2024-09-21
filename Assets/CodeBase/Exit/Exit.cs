using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public bool IsAllKeysCollected = false;

    public void SetWin()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
        IsAllKeysCollected = true;
    }
}
