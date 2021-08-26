using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    // Input Axis값을 받을 전역변수
    float hAxis;
    float vAxis;
    Vector3 moveVec;

    public float speed = 3.0f;
    
    NavMeshAgent agent;
    private Animator animator;

    private void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        // edit -> project settings -> input manager 에서 관리하고 있는 값

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;

        animator.SetBool("isWalk", moveVec != Vector3.zero);

        transform.LookAt(transform.position + moveVec); // 나아가는 방향으로 회전
    }
}
