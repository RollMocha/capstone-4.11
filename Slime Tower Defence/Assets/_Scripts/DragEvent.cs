using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragEvent : MonoBehaviour, IDropHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //Transform defultTransform;

    //public Image data;
    //public Sprite data2;
    //public DragContainer dragContainer;

    public GameObject dragObject; // 드래그에 쓸 오브젝트
    // 드래그에 쓸 오브젝트의 경우 GameScene_UI에 미리 배치한 뒤 SetActive를 false로 설정할 것

    public Slime slimePrefab; // 배치할 슬라임

    public bool isDragging = false; // 드래그 중인지 확인
    public bool isFruit = false; // 열매인지 확인
    public bool isPromote = false; // 상위 슬라임인지 확인


    // Start is called before the first frame update
    void Start()
    {

        //defultTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 드래그 이후 드롭 이벤트 때 사용
    public void OnDrop(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
    }

    // 드래그 시작 시 사용
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragObject == null)
        {
            return;
        }

        dragObject.SetActive(true);

        isDragging = true;
    }

    // 드래그 중 프레임 단위로 호출
    public void OnDrag(PointerEventData eventData)
    {

        // 드래그 중일 때 옮기는 이미지를 마우스에 따라가도록 조정
        if (isDragging)
        {
            dragObject.transform.position = eventData.position;

        }
    }

    // 드래그 끝날 때 실행
    public void OnEndDrag(PointerEventData eventData)
    {

        // 드래그 중인지 확인
        if (!isDragging)
        {
            Debug.Log("OnEndDrag : is not Dragging");
            return;
        }

        // 드래그 오브젝트가 설정되어 있는지 확인
        if (dragObject == null)
        {
            Debug.Log("OnEndDrag : is not Dragging");
            return;
        }

        dragObject.SetActive(false); // 미리 배치한 dragObject 활성화
        isDragging = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 카메라에서 나가는 선

        RaycastHit[] raycastHits = Physics.RaycastAll(ray); // 카메라에서 나가는 선에 부딪힌 오브젝트 모두 계산

        foreach (RaycastHit hit in raycastHits) // 부딪힌 물체들로 반복문 실행
        {
            if (hit.collider.tag != "Tile") // 부딪힌 물체가 타일인지 확인
            {
                // 타일이 아닐경우 다음 RaycastHit 확인
                continue;
            }


            // 가리킨 타일을 투명으로 변경
            // 이때 타일의 Materials의 RenderingMode를 변경할 필요가 있음
            /*
            GameObject hitTile = hit.collider.gameObject; // 현재 이미지가 있는 타일

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

            // raycast에 부딫힌 오브젝트의 Tile 정보 가져옴
            Tile targetTile = hit.collider.gameObject.GetComponent<Tile>();

            // 슬라임 생성 및 확인
            if (isFruit)
            {
                ChangeDefultSlime(targetTile, slimePrefab);
            }
            else if (isPromote)
            {
                ChangeFruitSlime(targetTile, slimePrefab);
            }
            else
            {
                SpawnDefultSlime(targetTile, slimePrefab);
            }

            return;
        }
    }

    // 기본 슬라임 배치
    public void SpawnDefultSlime(Tile tile, Slime slimePrefab)
    {
        // 슬라임 정보가 있는지 확인
        if (slimePrefab == null)
        {
            return;
        }

        // 배치할 타일에 타워가 있는지 확인
        if (tile.CheckSlime())
        {
            Debug.LogWarning("Tile.SetSlime : already tile have tower");
            return;
        }

        // 슬라임을 생성
        Slime attachSlime = Instantiate(slimePrefab, tile.towerPosition, Quaternion.identity);
        Debug.Log("tower Set");

        // 슬라임 정보 체크
        if (attachSlime == null)
        {
            return;
        }

        // 타일에 설치한 타워 정보 전달
        tile.SetSlime(attachSlime);
        PromoteSlimeSpawnManager.promoteSlimeSpawnManager.AddSlimeAtList(attachSlime);
    }

    // 기존 슬라임 변경
    public void ChangeDefultSlime(Tile tile, Slime slimePrefab)
    {

        // 배치할 슬라임 정보가 있는지 확인
        if (slimePrefab == null)
        {
            Debug.LogError("Tile.ChangeSlime : slime is not setting");
            return;
        }

        Slime pastSlime = tile.GetSlime();

        // 배치할 타일에 타워가 있는지 확인
        if (pastSlime == null)
        {
            Debug.LogWarning("Tile.ChangeSlime : no have target");
            return;
        }

        // 타워가 기본이 맞는지 확인
        if (pastSlime.state != SlimeState.DEFAULT)
        {
            Debug.LogWarning("Tile.ChangeSlime : slime is not default");
            return;
        }

        tile.DestroySlime();

        Slime attachSlime = Instantiate(slimePrefab, tile.towerPosition, Quaternion.identity);
        Debug.Log("tower Set");

        // 슬라임 정보 체크
        if (attachSlime == null)
        {
            return;
        }

        // 타일에 설치한 타워 정보 전달
        tile.SetSlime(attachSlime);
        PromoteSlimeSpawnManager.promoteSlimeSpawnManager.AddSlimeAtList(attachSlime);
    }

    // 열매 슬라임 변경
    public void ChangeFruitSlime(Tile tile, Slime slimePrefab)
    {

        // 배치할 슬라임 정보가 있는지 확인
        if (slimePrefab == null)
        {
            Debug.LogError("Tile.ChangeSlime : slime is not setting");
            return;
        }

        int checkNum = PromoteSlimeSpawnManager.promoteSlimeSpawnManager.CheckPromoteSlime(slimePrefab.state);

        if (checkNum != 1)
        {
            Debug.LogError("PromoteSlimeSpawnManager Error");
            Debug.Log(checkNum);
            return;
        }

        // 배치할 타일에 타워가 있는지 확인
        if (tile.CheckSlime())
        {
            Debug.LogWarning("Tile.SetSlime : already tile have tower");
            return;
        }

        Slime attachSlime = Instantiate(slimePrefab, tile.towerPosition, Quaternion.identity);
        Debug.Log("tower Set");

        // 슬라임 정보 체크
        if (attachSlime == null)
        {
            Debug.LogError("Slime is not Attaching");
            return;
        }

        // 타일에 설치한 타워 정보 전달
        tile.SetSlime(attachSlime);
        PromoteSlimeSpawnManager.promoteSlimeSpawnManager.AddSlimeAtList(attachSlime);
    }

}