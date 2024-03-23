using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Build : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;
    
    private void OnMouseDown()
    {
        gameManager.isCursorEmpty = false;
    }
    
    private void OnMouseUp()
    {
        gameManager.isCursorEmpty = false;
        CancelInvoke(nameof(OnMouseDrag));
    }
    
    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }
}
