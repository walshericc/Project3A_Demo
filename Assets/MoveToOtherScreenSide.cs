using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToOtherScreenSide : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var positionInScreenSpace = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        if (positionInScreenSpace.x > Screen.width)
            positionInScreenSpace.x = 0;
        else if (positionInScreenSpace.x < 0)
            positionInScreenSpace.x = Screen.width;

        if (positionInScreenSpace.y > Screen.height)
            positionInScreenSpace.y = 0;
        else if (positionInScreenSpace.y < 0)
            positionInScreenSpace.y = Screen.height;

        transform.position = Camera.main.ScreenToWorldPoint(positionInScreenSpace);
    }
}
