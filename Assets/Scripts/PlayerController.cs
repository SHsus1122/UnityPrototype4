using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerUpStrength = 10f;    // powerUp 아이템 획득시 충돌 반발력 변수

    public GameObject powerupIndicator;     // powerUp 상태 확인용 게임 오브젝트
    public float speed = 5.0f;
    public bool hasPowerUp;                 // powerUp 상태 확인용 변수

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        // 플레이어(구체)가 바라보는 방향이 아닌 카메라가 바라보는 방향으로 이동하기 위해서 하는 작업
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical"); // 조작계 설정

        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed); // 조작계 속도 및 방향 설정

        // powerUp 상태 표시용 오브젝트의 위치를 player위치에 계속해서 고정시키기 위한 코드입니다.
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    // 트리거 이벤트로 사용하는 함수입니다.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);    // powerUp 상태 오브젝트를 표시합니다.
            // 아이템을 획득 했다면 이곳으로 넘어와서 코루틴을 시작해줍니다.
            // 호출한 객체(OnTriggerEnter)가 실행된 코루틴(PowerupCountdownRoutine)의 소유권을 가지게 됩니다.
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    // 코루틴 함수인 WaitForSecondes()와 IEnumerator를 사용해서 카운트를 시작합니다.
    IEnumerator PowerupCountdownRoutine()
    {
        // yield 키워드를 사용해서 프로세스의 동작을 시작합니다.
        // 아래의 코드는 7초 뒤에 그 이후의 코드를 작동하게 합니다.
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerupIndicator.gameObject.SetActive(false);       // powerUp 상태 오브젝트를 숨깁니다.
    }

    // 물리 옵션을 활용할 때 주로 사용하는 함수입니다.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody  enemgyRirgidbody = collision.gameObject.GetComponent<Rigidbody>();
            // 적이 플레이어와 충돌해서 밀려나간 방향
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            // 충돌 이후 반대 방향으로 적을 10 의 힘만큼 즉시 밀어냅니다.
            enemgyRirgidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);

            // 확인용 디버그 코드 : 충돌한 대상의 오브젝트 이름과 자신의 powerup 상태를 디버깅문으로 확인
            Debug.Log("Collided with: " + collision.gameObject.name + " with poswerup set to " + hasPowerUp);
        }
    }
}
