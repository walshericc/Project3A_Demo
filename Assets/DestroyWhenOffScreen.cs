using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenOffScreen : MonoBehaviour
{
    void Update()
    {
        var positionInScreenSpace = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        if (positionInScreenSpace.x > Screen.width || positionInScreenSpace.x < 0)
            Destroy(gameObject);

        if (positionInScreenSpace.y > Screen.height || positionInScreenSpace.y < 0)
            Destroy(gameObject);
    }
}
