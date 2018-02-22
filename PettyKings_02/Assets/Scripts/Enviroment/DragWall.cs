using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragWall : MonoBehaviour
{

    RaycastHit hit;
    Ray ray;

    bool isDraggable;
    // Use this for initialization
    void Start () {
        isDraggable = true;
	}


    void OnMouseDrag()
    {
        if (isDraggable)
        {
            //cast ray
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f) && hit.collider.tag != "Wall")
            {
                Instantiate(this.gameObject, hit.transform.position, this.gameObject.transform.rotation);
            }
            Debug.Log("Draggin");
        }
    }

   
}
