using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ ��ȯ�� ���� �ʿ� ��ġ�� ������ ������ �����ϴ� �Ŵ��� Ŭ����
public class PromoteSlimeSpawnManager : MonoBehaviour
{
    Dictionary<Tile, Slime> slimeAndTile; // �ʻ� �ִ� �����Ӱ� �������� ��ġ�� Ÿ��

    static public PromoteSlimeSpawnManager promoteSlimeSpawnManager;

    private void Awake()
    {
        promoteSlimeSpawnManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        slimeAndTile = new Dictionary<Tile, Slime>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ��ġ�� ���� �����ӿ� �ʿ��� ������ ���� 
    SlimeState CheckMaterialSlime_1(SlimeState promoteSlimeState)
    {
        switch (promoteSlimeState) // ��ȯ�� �����ӿ� �´� �Լ� ����
        {
            case SlimeState.VINE: // ���� ������ : ���� �����Ӱ� ���� ������
                return SlimeState.ICE;
            case SlimeState.WATER: // �� ������ : �� �����Ӱ� ���� ������
                return SlimeState.ICE;
            case SlimeState.WIND: // �ٶ� ������ : �� �����Ӱ� ���� ������
                return SlimeState.FIRE;
            default: // ������ ���� ���� �������� ���� ���
                Debug.Log("no information from PromoreSime");
                return SlimeState.DEFAULT;
        }
    }

    // ��ġ�� ���� �����ӿ� �ʿ��� ������ ���� 
    SlimeState CheckMaterialSlime_2(SlimeState promoteSlimeState)
    {
        switch (promoteSlimeState) // ��ȯ�� �����ӿ� �´� �Լ� ����
        {
            case SlimeState.VINE: // ���� ������ : ���� �����Ӱ� ���� ������
                return SlimeState.THUNDER;
            case SlimeState.WATER: // �� ������ : �� �����Ӱ� ���� ������
                return SlimeState.FIRE;
            case SlimeState.WIND: // �ٶ� ������ : �� �����Ӱ� ���� ������
                return SlimeState.THUNDER;
            default: // ������ ���� ���� �������� ���� ���
                Debug.Log("no information from PromoreSime");
                return SlimeState.DEFAULT;
        }
    }

    // �⺻ ������ ��ġ
    public void SpawnDefultSlime(Tile tile, Slime slimePrefab)
    {
        // ������ ������ �ִ��� Ȯ��
        if (slimePrefab == null || tile == null)
        {
            Debug.LogWarning("tile and slimePrefab error");
            return;
        }

        // ��ġ�� Ÿ�Ͽ� Ÿ���� �ִ��� Ȯ��
        if (tile.isSlime)
        {
            Debug.LogWarning("already tile have tower");
            return;
        }

        tile.isSlime = true; // Ÿ���� ������ üũ �� ����

        // �������� ���� �� ����Ʈ�� �߰�
        Slime attachSlime = Instantiate(slimePrefab, tile.towerPosition, Quaternion.identity);
        slimeAndTile.Add(tile, attachSlime);
    }

    // ���� ������ ��ġ
    public void ChangeDefultSlime(Tile tile, Slime slimePrefab)
    {

        // ��ġ�� ������ ������ �ִ��� Ȯ��
        if (slimePrefab == null || tile == null)
        {
            Debug.LogWarning("tile and slimePrefab error");
            return;
        }

        // ��ġ�� Ÿ�Ͽ� Ÿ���� �ִ��� Ȯ��
        if (!tile.isSlime)
        {
            Debug.LogWarning("no have target slime");
            return;
        }

        // �ٲ� �⺻ �������� �ִ��� Ȯ��
        Slime pastSlime;
        if (!slimeAndTile.TryGetValue(tile, out pastSlime))
        {
            Debug.Log("this tile not in List");
            return;
        }

        // Ÿ���� �⺻�� �´��� Ȯ��
        if (pastSlime.state != SlimeState.DEFAULT)
        {
            Debug.Log("slime is not default");
            return;
        }

        // ������ �⺻ �������� ����
        slimeAndTile.Remove(tile);
        Destroy(pastSlime.gameObject);

        // �������� ���� �� ����Ʈ�� �߰�
        Slime attachSlime = Instantiate(slimePrefab, tile.towerPosition, Quaternion.identity);
        slimeAndTile.Add(tile, attachSlime);
    }

    // ���� ������ ����
    public void ChangeFruitSlime(Tile tile, Slime slimePrefab)
    {
        // ��ġ�� ������ ������ �ִ��� Ȯ��
        if (slimePrefab == null || tile == null)
        {
            Debug.LogWarning("tile and slimePrefab error");
            return;
        }

        // ���� ������ ��ġ�� ���� �ʿ��� ���� ������ ������
        SlimeState materialSlimeState_1 = CheckMaterialSlime_1(slimePrefab.state);
        SlimeState materialSlimeState_2 = CheckMaterialSlime_2(slimePrefab.state);
        Slime materialSlime_1 = null; // ���� ������ 1
        Slime materialSlime_2 = null; // ���� ������ 2
        Tile materialSlimeTile_1 = null; // ���� �������� Ÿ��
        Tile materialSlimeTile_2 = null; // ���� �������� Ÿ��

        // ���� �������� ��ġ�� �� �ش� Ÿ�Ͽ� ��� �������� ���� ��츦 Ȯ��
        // ��� �������� ���� ��� ���� �� ����� ���̱� ������ isSlime üũ�� �ʿ� ����.
        bool tileHaveMaterialSlime = false;

        // ���� ������ ��ġ�� ���� �ʿ��� ��� ������ ã��
        foreach (KeyValuePair<Tile, Slime> pair in slimeAndTile)
        {
            // ù ��° ��� ������ ã��
            if (pair.Value.state == materialSlimeState_1)
            {
                materialSlime_1 = pair.Value;
                materialSlimeTile_1 = pair.Key;

                if (materialSlimeTile_1 == tile)
                {
                    // ��ġ�� ��ġ�� ��� �������� �ִ� ���
                    tileHaveMaterialSlime = true;
                }
                continue;
            }

            // �� ��° ��� ������ ã��
            if (pair.Value.state == materialSlimeState_2)
            {
                materialSlime_2 = pair.Value;
                materialSlimeTile_2 = pair.Key;

                if (materialSlimeTile_2 == tile)
                {
                    // ��ġ�� ��ġ�� ��� �������� �ִ� ���
                    tileHaveMaterialSlime = true;
                }
                continue;
            }
        }

        // �ʿ��� ��� �������� ã�� ���ϴ� ���
        if (materialSlime_1 == null || materialSlime_2 == null)
        {
            Debug.LogWarning("not have material slime");
            return;
        }

        // ��ġ�� ��ġ�� ��� ������ �̿��� �������� �ִ� ���
        if (tile.isSlime)
        {
            if (!tileHaveMaterialSlime)
            {
                Debug.LogWarning("already tile have tower");
                return;
            }
        }

        // ������ ��� ������ ����
        materialSlimeTile_1.isSlime = false;
        slimeAndTile.Remove(materialSlimeTile_1);
        Destroy(materialSlime_1.gameObject);

        materialSlimeTile_2.isSlime = false;
        slimeAndTile.Remove(materialSlimeTile_2);
        Destroy(materialSlime_2.gameObject);

        // �������� ���� �� ����Ʈ�� �߰�
        Slime attachSlime = Instantiate(slimePrefab, tile.towerPosition, Quaternion.identity);
        slimeAndTile.Add(tile, attachSlime);
        tile.isSlime = true;
    }
}
