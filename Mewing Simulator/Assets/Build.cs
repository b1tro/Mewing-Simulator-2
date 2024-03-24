using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class Build : MonoBehaviour
{
    [FormerlySerializedAs("gameManager")] [SerializeField] public TileMovement tileMovement;
    
    private void OnMouseDown()
    {
        tileMovement.isCursorEmpty = false;
    }
    
    private void OnMouseUp()
    {
        tileMovement.isCursorEmpty = false;
        CancelInvoke(nameof(OnMouseDrag));
    }
    
    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }
}
