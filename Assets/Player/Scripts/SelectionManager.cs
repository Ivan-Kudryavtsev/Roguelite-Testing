using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Transform selection;
    [SerializeField] private LayerMask selectableLayerMask;
    // Update is called once per frame
    void Update()
    {   
        //Cast a ray from the mouse position through the Z axis
        //First object that is on the selectable layer hit will be tagged and selected
        Vector2 raycastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero, Mathf.Infinity, selectableLayerMask);
        if (hit.collider != null)
        {
            selection = hit.transform;
            //TODO highlight selected objects
            //this is actually quite involved thanks to shaders
            //i will be doing this some other time
        } else
        {
            selection = null;
        }
    }

    //Returns currently selected transform.
    public Transform GetSelection()
    {
        return selection;
    }
}
