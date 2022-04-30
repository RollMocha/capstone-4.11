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

    public GameObject dragObject; // 드래그에 쓸 오브젝트
    // 드래그에 쓸 오브젝트의 경우 GameScene_UI에 미리 배치한 뒤 SetActive를 false로 설정할 것

    public GameObject slimePrefab; // 배치할 슬라임

    public bool isDragging = true; // 드래그 중인지 확인
    

    // Start is called before the first frame update
    void Start()
    {

        defultTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {

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

        if (dragObject == null)
        {
            return;
        }

        dragObject.SetActive(true);

        /*
        if (data.sprite == null)
        {
            return;
        }
        */

        /*
        dragContainer.dragObject.SetActive(true);
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
            dragObject.transform.position = eventData.position;
            
        }
    }

    // 드래그 끝날 때 실행
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag End");

        
        // 드래그 중인지 확인
        if (!isDragging)
        {
            Debug.Log("OnEndDrag : is not Dragging");
            return;
        }

        if (dragObject == null)
        {
            Debug.Log("OnEndDrag : is not Dragging");
            return;
        }

        dragObject.SetActive(false);
        isDragging = false;

        /*
        dragContainer.image.sprite = null;
        dragContainer.dragObject.SetActive(false);
        */

        // test
        //RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 카메라에서 나가는 선

        RaycastHit[] raycastHits = Physics.RaycastAll(ray); // 카메라에서 나가는 선에 부딪힌 오브젝트 모두 계산

        foreach (RaycastHit hit in raycastHits) // 부딪힌 물체들로 반복문 실행
        {
            string objectName = hit.collider.gameObject.name; // 테스트로 오브젝트 이름 출력
            //Debug.Log(objectName);

            if (hit.collider.tag != "Tile") // 부딪힌 물체가 타일인지 확인
            {
                // 타일이 아닐경우 다음 RaycastHit 확인
                Debug.Log("this is not tile");
                continue;
            }

            GameObject hitTile = hit.collider.gameObject; // 현재 이미지가 있는 타일

            // 가리킨 타일을 투명으로 변경
            // 이때 타일의 Materials의 RenderingMode를 변경할 필요가 있음
            /*
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
            */
            
            // 배치할 슬라임 정보가 설정되어 있는지 확인
            if (slimePrefab == null)
            {
                Debug.Log("Slime Prefab is not setting");
            }

            // 타워 생성
            Tile targetTile = hit.collider.gameObject.GetComponent<Tile>();
            SpawnTower(targetTile, slimePrefab);
            return;
        }
    }

    // 타워 생성
    public void SpawnTower(Tile tile, GameObject tower)
    {
        if (tile == null)
        {
            Debug.LogError("SpawnTowerError : tile Component error");
            return;
        }

        if (tower == null)
        {
            Debug.LogError("SpawnTowerError : tower is not setting");
            return;
        }

        if (tile.SlimeCheck())
        {
            Debug.LogWarning("SpawnTowerError : already tile have Slime");
            return;
        }

        tile.isSlime = true;
        Vector3 towerPosition = tile.GetPosition();

        Instantiate(slimePrefab, new Vector3(towerPosition.x, towerPosition.y + 3f,
            towerPosition.z), Quaternion.identity);
        Debug.Log("tower Set");
    }
}
