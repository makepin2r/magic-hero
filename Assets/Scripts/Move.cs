using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    NavMeshAgent agent;
    public float speed = 3.0f;
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
        if (Input.GetKey(KeyCode.W)) // ��
        {
            animator.SetBool("isWalk", true);
            this.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.A)) // ����
        {
            animator.SetBool("isWalk", true);
            this.transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.S)) // ��
        {
            animator.SetBool("isWalk", true);
            this.transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.D)) // ������
        {
            animator.SetBool("isWalk", true);
            this.transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed);
        }
        else 
        {
            animator.SetBool("isWalk", false);
        }
    }
}
