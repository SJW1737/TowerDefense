using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTest : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaveFlowTest());
    }

    private IEnumerator WaveFlowTest()
    {
        Debug.Log("=== Wave Test Start ===");

        // Wave 1 시작
        WaveManager.Instance.StartFirstWave();
        Debug.Log("Wave 1 시작");

        // Wave 1 몬스터 전부 죽을 때까지 대기
        yield return WaitUntilAllMonsterDead();

        Debug.Log("Wave 1 종료");

        // 약간 딜레이
        yield return new WaitForSeconds(3f);

        // Wave 2 시작
        WaveManager.Instance.StartNextWave();
        Debug.Log("Wave 2 시작");

        yield return WaitUntilAllMonsterDead();

        Debug.Log("Wave 2 종료");
        Debug.Log("=== Wave Test End ===");
    }

    private IEnumerator WaitUntilAllMonsterDead()
    {
        while (MonsterPoolManager.Instance.AliveMonsterCount > 0)
        {
            yield return null;
        }
    }
}
