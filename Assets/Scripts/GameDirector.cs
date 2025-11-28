using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameDirector : MonoBehaviour
{
    [SerializeField] private GameObject car; //find대신 시리얼라이즈필드로 인스펙터창에서 직접 오브젝트를 설정하게 한다. 시리얼라이즈필드를 사용하면 퍼블릭보다 보안성이 좋아진다.
    [SerializeField] private GameObject flag;
    [SerializeField] private GameObject distance;

    void Start()
    {

    }

    void Update()
    {
        float length = this.flag.transform.position.x - this.car.transform.position.x; //깃발의 x좌표에서 차량의 x좌표를 뺀다.
        this.distance.GetComponent<TextMeshProUGUI>().text = "Distance: " + length.ToString("F2") + "m"; //컴포넌트를 가져와 UI의 텍스트 내용을 변경한다.
    }
}
