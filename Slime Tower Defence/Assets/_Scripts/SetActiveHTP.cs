using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HowToPlay 오브젝트 활성화/ 비활성화
public class SetActiveHTP : MonoBehaviour
{
    public GameObject HowToPlayobj;
    public GameObject Titleobj;

    public void ClickHTPButton()//HowToPlayButton 클릭시
    {
        HowToPlayobj.SetActive(true);
        //HowToPlayDiscribe오브젝트 활성화
        Titleobj.SetActive(false);
    }

    public void ClickHTP()//HowToPlayDescribe 클릭시
    {
        HowToPlayobj.SetActive(false);
        //HowToPlayDiscribe오브젝트 비활성화
        Titleobj.SetActive(true);
    }
}
