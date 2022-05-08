using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlimeDragEvent : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 오브젝트에 마우스 다운 되었을 경우 실행
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("SlimeDragEvent Mouse Down!");

            //카메라 위치
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                            Input.mousePosition.y, -Camera.main.transform.position.z));

            Debug.Log("point : " + point);

            //this.gameObject.transform
        }
    }

    // 오브젝트에 마우스 업 되었을 경우 실행
    private void OnMouseUp()
    {
        
    }
}
