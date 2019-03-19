using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainToScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var positionInScreenSpace = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        positionInScreenSpace.x = Mathf.Clamp(positionInScreenSpace.x, 0, Screen.width);
        positionInScreenSpace.y = Mathf.Clamp(positionInScreenSpace.y, 0, Screen.height);

        transform.position = Camera.main.ScreenToWorldPoint(positionInScreenSpace);
    }
}
