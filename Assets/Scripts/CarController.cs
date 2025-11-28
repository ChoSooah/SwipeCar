using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float speed = 0; //속도를 저장할 변수
    float swipeLength = 0f;

    void Start()
    {
        Application.targetFrameRate = 60; //프레임 설정
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //마우스를 누른 순간
        {
            swipeLength = Input.mousePosition.x; //지금의 x좌표를 저장한다.
        }
        else if (Input.GetMouseButtonUp(0)) //마우스 버튼에서 손을 땐 순간
        {
            swipeLength = Input.mousePosition.x - swipeLength; //지금의 x좌표에 방금 전 저장한 x좌표를 빼 스와이프한 길이를 구한다.

            this.speed = swipeLength / 500.0f; //길이를 계산한 후 적용

            GetComponent<AudioSource>().Play(); //효과음 재생
        }

        transform.Translate(this.speed, 0, 0); //좌표를 변경해 이동한다.
        this.speed *= 0.98f; //스피드에 특정 숫자를 곱해 속도를 줄인다. 이 경우 절대로 0은 되지 않고 그에 한없이 가까운 수가 나오기 때문에 주의해야한다.
    }


}
