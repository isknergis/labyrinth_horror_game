using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastActions : MonoBehaviour
{
   
    private Camera camera;
    private Ray ray;
    private RaycastHit hit;

    private Targetable currentTargetable;

    private Collectable currentCollactable;


    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider.TryGetComponent(out Targetable targetable))
            {
                currentTargetable = targetable;
                currentTargetable.ToggleHighlight(true);
                if(currentTargetable.TryGetComponent(out Collectable collectable))

                {
                    currentCollactable = collectable;
                }
            }
            else if(currentTargetable)
            {
                currentTargetable.ToggleHighlight(false);
                currentTargetable = null;
                if(currentCollactable)
                {
                    currentCollactable=null;
                }
            }
        }
     
        if(Input.GetMouseButtonDown(0))
        { 
            if(currentCollactable)
            {
                currentCollactable.Collect();
                currentCollactable = null;
            }
        }
    }
}
