using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HowToPlay ������Ʈ Ȱ��ȭ/ ��Ȱ��ȭ
public class SetActiveHTP : MonoBehaviour
{
    public GameObject HowToPlayobj;
    public GameObject Titleobj;

    public void ClickHTPButton()//HowToPlayButton Ŭ����
    {
        HowToPlayobj.SetActive(true);
        //HowToPlayDiscribe������Ʈ Ȱ��ȭ
        Titleobj.SetActive(false);
    }

    public void ClickHTP()//HowToPlayDescribe Ŭ����
    {
        HowToPlayobj.SetActive(false);
        //HowToPlayDiscribe������Ʈ ��Ȱ��ȭ
        Titleobj.SetActive(true);
    }
}
