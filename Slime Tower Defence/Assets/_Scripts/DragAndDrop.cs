using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDrag()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Start");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag End");
    }
}
