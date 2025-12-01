using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;

public class ArrowController : MonoBehaviour
{
    bool isRotaringAngle = true; // 화살표가 회전(마우스를 따라 각도 변화) 중인지 체크

    [SerializeField] float angle = 0f; // 현재 화살표의 각도 저장
    public float Angle => angle; // 위의 변수를 외부에서 참조하기 위해 만든 public 변수, 외부에서 값 수정이 불가능함

    [SerializeField] private GameDirector gameDirector; // Reset 이벤트를 받기 위해 GameDirector 스크립트 참조
    public event EventHandler OnArrowStop; // 화살표가 멈췄을 때 SliderController에게 알림(연결)

    void Start()
    {
        gameDirector.OnReset += HandleReset; // GameDirector에서 리셋이 되면 이 스크립트도 초기화(연결)
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭 시 화살표 각도 고정
        {
            isRotaringAngle = false; // 화살표 회전 정지
            OnArrowStop?.Invoke(this, EventArgs.Empty); // 이벤트를 호출
        }

        if (isRotaringAngle) // 마우스를 따라 회전
        {
            ArrowAxis(); // 궤도(화살표) 회전
        }
    }

    void ArrowAxis()
    {
        Vector3 mouseworldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스의 월드 좌표를 구함
        mouseworldPos.z = 0; // z축 초기화
        Vector2 mouseAxis = mouseworldPos - transform.position; // 마우스의 방향을 구함

        angle = Mathf.Atan2(mouseAxis.y, mouseAxis.x) * Mathf.Rad2Deg; // 구한 방향을 각도로 변환

        if (angle >= -1 && angle <= 91) // -1도 ~ 91도 사이로만 회전하도록 각도 제한(화살표가 땅으로 각도를 조작하지 못함)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void HandleReset(object GameDirector, EventArgs e) // 화살표 위치 및 상태 초기화
    {
        isRotaringAngle = true; // 다시 회전 가능 상태로 복구
        angle = 0f; // 각도 초기화
    }
}