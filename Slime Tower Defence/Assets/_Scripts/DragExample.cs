using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragExample : MonoBehaviour, IDropHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Transform defultTransform;

    public Image data;
    public Sprite data2;
    public DragContainer dragContainer;
    public bool isDragging = true;

    public static DragExample dragExample;
    

    // Start is called before the first frame update
    void Start()
    {
        dragExample = this;

        defultTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        // throw new System.NotImplementedException();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Start");

        Vector2 touchPosition = eventData.position;

        Vector3 touchPositionInWorld = Camera.main.ScreenToWorldPoint(
            new Vector3()
            );

        if (data.sprite == null)
        {
            return;
        }
        
        dragContainer.gameObject.SetActive(true);
        dragContainer.image.sprite = data2;
        isDragging = true;

        
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("Drag");

        if (isDragging)
        {
            dragContainer.transform.position = eventData.position;
            
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag End");

        
        if (isDragging)
        {
            if (dragContainer.image.sprite != null)
            {
                //data.sprite = dragContainer.image.sprite;
            }
            else
            {
                //data.sprite = null;
            }

            this.transform.position = defultTransform.position;
        }

        isDragging = false;
        dragContainer.image.sprite = null;
        dragContainer.gameObject.SetActive(false);
        
    }
}
