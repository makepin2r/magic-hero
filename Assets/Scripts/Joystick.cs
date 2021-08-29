using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler 
{
    [SerializeField] private RectTransform rectBg; // 조이스틱 배경
    [SerializeField] private RectTransform rectHandle; // 조이스틱 내 핸들

    private float radius; // 조이스틱의 반지름
    private Vector3 bgInitialPos; // 조이스틱의 기준 위치 (터치로 인해 조이스틱이 이동된 후 -> 터치 끝낼 때 원위치로 이동)

    [SerializeField] private GameObject player; // 플레이어 GameObject
    private Rigidbody playerRigid; // 플레이어 RigidBody
    private Animator playerAnimator; // 플레이어 Animator
    [SerializeField] private float moveSpeed; // 플레이어의 이동속도

    private bool isTouch; // 조이스틱 터치 여부 (플레이어의 움직임 시점 체크)
    private Vector3 movePos; // 플레이어의 이동 방향
    private Vector3 rotatePos; // 플레이어의 회전 방향

    public void OnDrag(PointerEventData eventData)
    {

        Vector2 value = eventData.position - (Vector2)rectBg.position; // 터치 위치 - 조이스틱의 위치 (터치 위치의 0, 0 기준 offset)

        // 조이스틱 핸들의 위치 변경
        value = Vector2.ClampMagnitude(value, radius); // 핸들이 조이스틱 범위를 벗어나지 않게 조정
        rectHandle.localPosition = value;

        // 플레이어가 움직일 방향
        value = value.normalized;
        movePos = new Vector3(value.x * moveSpeed * Time.deltaTime, 0f, value.y * moveSpeed * Time.deltaTime); // 계산된 방향에 따른 이동 수치 대입
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
        rectBg.transform.position = eventData.position; // 터치가 시작된 좌표로 조이스틱 ui의 위치 이동
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false; 
        rectHandle.localPosition = Vector3.zero; // 핸들의 위치를 조이스틱 배경의 가운데로 이동
        movePos = Vector3.zero; // movePos 초기화 (핸들 재터치시 이전의 movePos값에 영향을 받지 않도록)
        rectBg.transform.position = bgInitialPos; // 조이스틱 ui 위치가 이동되었을 경우 다시 원위치로
    }

    void Start()
    {
        rotatePos = Vector3.zero; // rotatePos 초기화
        radius = rectBg.rect.width * 0.5f; // 조이스틱 배경 면적의 반지름
        bgInitialPos = rectBg.transform.position; // 에디터로 세팅된 위치를 기준 위치로 지정

        playerRigid = player.GetComponent<Rigidbody>();
        playerAnimator = player.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isTouch)
        {
            player.transform.position += movePos; // Drag시 계산된 값을 더해 플레이어 이동
            if (movePos != Vector3.zero)
                rotatePos = movePos; // movePos가 (0,0,0)일 경우 터치해도 이전값 유지 (기존 회전 방향을 유지할 수 있도록)
            playerRigid.rotation = Quaternion.LookRotation(rotatePos); // 움직이는 방향을 바라보게끔 Rigidbody 회전
            playerAnimator.SetBool("isWalk", movePos != Vector3.zero); // 움직일 때 애니메이션을 walk 상태로
        } else
            playerAnimator.SetBool("isWalk", false); // 움직이지 않을 때는 idle
    }
}
