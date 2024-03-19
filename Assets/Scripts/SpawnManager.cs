using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9.0f;

    // Start is called before the first frame update
    void Start()
    {
        // 랜덤 스폰 코드
        Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 랜덤 적 스폰 커스텀 메서드
    // 내부에서만 사용할 함수이기에 private 접근자 사용
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);  // 랜덤 스폰 X 0~9
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);  // 랜덤 스폰 Z 0~9

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ); // 랜덤 스폰 좌표
        return randomPos;
    }
}
