using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    private Rigidbody enemyRb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Player 의 위치로부터 enemy 위치까지의 거리만큼 원하는 속도로 계속해서 힘을 주어 이동시킵니다.(플레이어 추적 및 이동)
        // 시간이 지남에 따라 값이 너무 커져서 힘이 너무 커지는 즉, 거리에 영향을 너무 많이 받지 않게끔 해줍니다.(normalized)
        // normalized 를 플레이어와 적 사이의 거리를 구한 Vector3 뒤에 입력해주면, 벡터의 크기를 정규화 합니다.
        // 이를 통해 적과 플레이어 간의 거리에 상관없이 항상 일정한 속도로 움직이게 됩니다.(방향성만 구하고 크기는 1로 고정)
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
