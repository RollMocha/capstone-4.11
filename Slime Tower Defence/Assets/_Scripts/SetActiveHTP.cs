using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HowToPlay 오브젝트 활성화/ 비활성화
public class SetActiveHTP : MonoBehaviour
{
    public GameObject HowToPlayobj;

    public void ClickHTPButton()//HowToPlayButton 클릭시
    {
        HowToPlayobj.SetActive(true); 
        //HowToPlayDiscribe오브젝트 활성화
    }

    public void ClickHTP()//HowToPlayDescribe 클릭시
    {
        HowToPlayobj.SetActive(false);
        //HowToPlayDiscribe오브젝트 비활성화
    }
}
