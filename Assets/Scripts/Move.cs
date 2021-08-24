using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    NavMeshAgent agent;
    public float speed = 3.0f;

    private void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
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
            this.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.A)) // ����
        {
            this.transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * speed);
        }
        
        if (Input.GetKey(KeyCode.S)) // ��
        {
            this.transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * speed);
        }
        
        if (Input.GetKey(KeyCode.D)) // ������
        {
            this.transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed);
        }
    }
}
