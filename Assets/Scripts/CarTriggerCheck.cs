using UnityEngine;

public class CarTriggerCheck : MonoBehaviour
{
    // Inspector에서 설정
    [SerializeField] int score = 0; // 자동차가 깃발에 닿으면 점수 +100, 바닥에 닿으면 +0
    public int Score => score; // 점수 UI 표시

    [SerializeField] private GameDirector gameDirector;   // 점수 관리, GameDirector 스크립트 참조

    void Update()
    {
        CheckOutCamera(); // 매 프레임마다 자동차가 카메라 밖으로 벗어났는지 체크
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground")) // 충돌한 오브젝트가 "Ground"라면 (땅 충돌)
        {
            Debug.Log("땅"); // 디버그 로그에 "땅" 출력
            gameDirector.Reset(); // 자동차 리셋
        }
        else if (collision.collider.CompareTag("Flag")) // 충돌한 오브젝트가 "Flag" 태그라면 (깃발 충돌)
        {
            Debug.Log("깃발"); // 디버그 로그에 "깃발" 출력
            score += 100; // 깃발에 닿으면 점수 +100
            gameDirector.Reset(); // 자동차 리셋
        }
    }

    void CheckOutCamera()
    {
        Vector2 viewPos = Camera.main.WorldToViewportPoint(transform.position);  // 자동차의 월드 좌표를 [0,1]로 변환
        bool isOut = viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1; // 화면을 벗어났는지 체크(상하좌우 체크)

        if (isOut) // 자동차가 화면을 벗어났다면
        {
            Debug.Log("화면 벗어남"); // 디버그 로그에 "화면 벗어남" 출력
            gameDirector.Reset(); // 자동차 리셋
        }
    }
}
