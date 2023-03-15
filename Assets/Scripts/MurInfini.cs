using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurInfini : MonoBehaviour
{
    Renderer[] renderers;
    bool isWrappingX = false;
    bool isWrappingY = false;
    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    

    // Update is called once per frame
    void Update()
    {
        ScreenWrap();
    }

    bool CheckRenderers()
    {
        /*foreach (var renderer in renderers)
        {
            if (renderer.isVisible)
            {
                return true;
            }
        }
        return false;*/
        if (transform.position.x > -26.8 && transform.position.x < 26.8 && transform.position.y > -14.5 && transform.position.y < 10.8)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ScreenWrap()
    {
        var isVisible = CheckRenderers();
        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }
        if (isWrappingX && isWrappingY)
        {
            return;
        }
        var cam = Camera.main;
        var viewportPosition = cam.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;
        if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;
            isWrappingX = true;
        }
        if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            if(transform.position.y < 10)
            {
                newPosition.y = -newPosition.y;
            }
            else
            {
                newPosition.y = -newPosition.y+5;
            }
            isWrappingY = true;
        }
        transform.position = newPosition;
    }
}
