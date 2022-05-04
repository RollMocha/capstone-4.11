using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlimeDragEvent : DragEvent
{
    public Slime dragSlime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public new void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("new Drag!");

        if (dragObject == null)
        {
            return;
        }

        dragObject.SetActive(true);

        isDragging = true;

        // 카메라에서 나가는 선
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 카메라에서 나가는 선에 부딪힌 오브젝트 모두 계산
        RaycastHit[] raycastHits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in raycastHits) // 부딪힌 물체들로 반복문 실행
        {
            //gameObject hitTarget = hit.collider

            if (hit.collider.tag != "Slime")
            {
                Debug.Log("this is not Slime");
                continue;
            }

            dragSlime = hit.collider.GetComponent<Slime>();
        }
    }

    public new void OnDrag(PointerEventData eventData)
    {
            // Debug.Log("Drag");

            // 드래그 중일 때 옮기는 이미지를 마우스에 따라가도록 조정
        if (isDragging)
        {
            Vector3 dragSlimePosition = new Vector3(eventData.position.x, 5f, 
                eventData.position.y);

            dragSlime.gameObject.transform.position = dragSlimePosition;
        }


    }

    public new void OnEndDrag(PointerEventData eventData)
    {

    }
}
