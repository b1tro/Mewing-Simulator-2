using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

[System.Serializable]
public class Build : MonoBehaviour
{
    [FormerlySerializedAs("spriteBuilding")] [SerializeField]
    public List<Vector3Int> cellPosBuilding;

    public Building cellBuilding;
}
