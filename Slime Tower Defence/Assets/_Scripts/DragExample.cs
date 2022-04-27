using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragExample : MonoBehaviour, IDropHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Transform defultTransform;

    //public Image data;
    //public Sprite data2;
    //public DragContainer dragContainer;
    public GameObject gameObject;

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
        /*
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            string objectName = hit.collider.gameObject.name;
            Debug.Log(objectName);
        }
        */

    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        // throw new System.NotImplementedException();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Start");

        /*
        if (data.sprite == null)
        {
            return;
        }
        */

        /*
        dragContainer.gameObject.SetActive(true);
        dragContainer.image.sprite = data2;
        */

        isDragging = true;

        
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("Drag");

        if (isDragging)
        {
            //dragContainer.transform.position = eventData.position;
            gameObject.transform.position = eventData.position;
            
        }

        // test
        //RaycastHit hit;

        Vector3 cameraPosition = Camera.main.transform.position;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in raycastHits)
        {
            string objectName = hit.collider.gameObject.name;
            //Debug.Log(objectName);

            if (hit.collider.tag == "Tile")
            {
                GameObject hitTile = hit.collider.gameObject;
                Renderer renderer = hitTile.GetComponentInChildren<Renderer>();

                if (renderer == null)
                {
                    Debug.Log("renderer null");
                }

                MeshRenderer mesh = hitTile.GetComponent<MeshRenderer>();

                if (mesh == null)
                {
                    Debug.Log("mesh null");
                }

                

                Material material = renderer.material;
                Color color = material.color;

                Debug.Log(color.a);

                color.a = 0.1f;
                material.color = color;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag End");

        
        if (isDragging)
        {
            /*
            if (dragContainer.image.sprite != null)
            {
                //data.sprite = dragContainer.image.sprite;
            }
            else
            {
                //data.sprite = null;
            }
            */

            //dragObject.transform.position = defultTransform.position;
        }

        isDragging = false;

        /*
        dragContainer.image.sprite = null;
        dragContainer.gameObject.SetActive(false);
        */
        
    }
}
