using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;      // 적 관련 프리펩 변수
    public GameObject powerupPrefab;    // powerUp 아이템 프리펩 변수
    public int enemyCount;      // 적의 수를 카운트해줄 변수
    public int waveNumber = 1;  // 웨이브 개념의 변수로 난이도 상승을 위한 변수입니다.

    private float spawnRange = 9.0f;

    // Start is called before the first frame update
    void Start()
    {
        // 시작하면 적과 아이템을 랜덤하게 스폰 시킵니다.
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        // FindObjectsOfType 를 통해 모든 Enemy 오브젝트를 찾습니다.
        enemyCount = FindObjectsOfType<Enemy>().Length;

        // 적의 수가 0이 되면 새로운 적을 처음에 한 명에서 한 명씩 증가시키면서 스폰합니다.(아이템은 하나씩만 스폰)
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // 랜덤 스폰 코드
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    // 랜덤 적 스폰 좌표 커스텀 메서드
    // 내부에서만 사용할 함수이기에 private 접근자 사용
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);  // 랜덤 스폰 X 0~9
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);  // 랜덤 스폰 Z 0~9

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ); // 랜덤 스폰 좌표
        return randomPos;
    }
}
