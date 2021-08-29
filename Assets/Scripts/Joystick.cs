using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler 
{
    [SerializeField] private RectTransform rectBg; // ���̽�ƽ ���
    [SerializeField] private RectTransform rectHandle; // ���̽�ƽ �� �ڵ�

    private float radius; // ���̽�ƽ�� ������
    private Vector3 bgInitialPos; // ���̽�ƽ�� ���� ��ġ (��ġ�� ���� ���̽�ƽ�� �̵��� �� -> ��ġ ���� �� ����ġ�� �̵�)

    [SerializeField] private GameObject player; // �÷��̾� GameObject
    private Rigidbody playerRigid; // �÷��̾� RigidBody
    private Animator playerAnimator; // �÷��̾� Animator
    [SerializeField] private float moveSpeed; // �÷��̾��� �̵��ӵ�

    private bool isTouch; // ���̽�ƽ ��ġ ���� (�÷��̾��� ������ ���� üũ)
    private Vector3 movePos; // �÷��̾��� �̵� ����
    private Vector3 rotatePos; // �÷��̾��� ȸ�� ����

    public void OnDrag(PointerEventData eventData)
    {

        Vector2 value = eventData.position - (Vector2)rectBg.position; // ��ġ ��ġ - ���̽�ƽ�� ��ġ (��ġ ��ġ�� 0, 0 ���� offset)

        // ���̽�ƽ �ڵ��� ��ġ ����
        value = Vector2.ClampMagnitude(value, radius); // �ڵ��� ���̽�ƽ ������ ����� �ʰ� ����
        rectHandle.localPosition = value;

        // �÷��̾ ������ ����
        value = value.normalized;
        movePos = new Vector3(value.x * moveSpeed * Time.deltaTime, 0f, value.y * moveSpeed * Time.deltaTime); // ���� ���⿡ ���� �̵� ��ġ ����
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
        rectBg.transform.position = eventData.position; // ��ġ�� ���۵� ��ǥ�� ���̽�ƽ ui�� ��ġ �̵�
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false; 
        rectHandle.localPosition = Vector3.zero; // �ڵ��� ��ġ�� ���̽�ƽ ����� ����� �̵�
        movePos = Vector3.zero; // movePos �ʱ�ȭ (�ڵ� ����ġ�� ������ movePos���� ������ ���� �ʵ���)
        rectBg.transform.position = bgInitialPos; // ���̽�ƽ ui ��ġ�� �̵��Ǿ��� ��� �ٽ� ����ġ��
    }

    void Start()
    {
        rotatePos = Vector3.zero; // rotatePos �ʱ�ȭ
        radius = rectBg.rect.width * 0.5f; // ���̽�ƽ ��� ������ ������
        bgInitialPos = rectBg.transform.position; // �����ͷ� ���õ� ��ġ�� ���� ��ġ�� ����

        playerRigid = player.GetComponent<Rigidbody>();
        playerAnimator = player.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isTouch)
        {
            player.transform.position += movePos; // Drag�� ���� ���� ���� �÷��̾� �̵�
            if (movePos != Vector3.zero)
                rotatePos = movePos; // movePos�� (0,0,0)�� ��� ��ġ�ص� ������ ���� (���� ȸ�� ������ ������ �� �ֵ���)
            playerRigid.rotation = Quaternion.LookRotation(rotatePos); // �����̴� ������ �ٶ󺸰Բ� Rigidbody ȸ��
            playerAnimator.SetBool("isWalk", movePos != Vector3.zero); // ������ �� �ִϸ��̼��� walk ���·�
        } else
            playerAnimator.SetBool("isWalk", false); // �������� ���� ���� idle
    }
}
