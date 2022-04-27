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

    public GameObject gameObject; // 드래그에 쓸 오브젝트

    public bool isDragging = true; // 드래그 중인지 확인
    

    // Start is called before the first frame update
    void Start()
    {

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

    // 드래그 이후 드롭 이벤트 때 사용
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        // throw new System.NotImplementedException();
    }

    // 드래그 시작 시 사용
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

    // 드래그 중 프레임 단위로 호출
    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("Drag");

        // 드래그 중일 때 옮기는 이미지를 마우스에 따라가도록 조정
        if (isDragging)
        {
            //dragContainer.transform.position = eventData.position;
            gameObject.transform.position = eventData.position;
            
        }

        // test
        //RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 카메라에서 나가는 선

        RaycastHit[] raycastHits = Physics.RaycastAll(ray); // 카메라에서 나가는 선에 부딪힌 오브젝트 모두 계산

        foreach (RaycastHit hit in raycastHits) // 부딪힌 물체들로 반복문 실행
        {
            string objectName = hit.collider.gameObject.name; // 테스트로 오브젝트 이름 출력
            //Debug.Log(objectName);

            if (hit.collider.tag == "Tile") // 부딪힌 물체가 타일인지 확인
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

    // 드래그 끝날 때 실행
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
