using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mewing : MonoBehaviour
{
    private void OnMouseEnter()
    {
<<<<<<< Updated upstream
        transform.localScale += new Vector3(1, 1, 1);
        //HUI 2
=======
        transform.localScale = new Vector2(1, 1);
        //HUI
>>>>>>> Stashed changes
    }
}
