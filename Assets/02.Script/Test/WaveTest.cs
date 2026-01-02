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
        //Wave 1 시작
        WaveManager.Instance.StartFirstWave();
        Debug.Log("Wave 1 시작");

        yield return WaitUntilAllMonsterDead();

        Debug.Log("Wave 1 종료");

        //딜레이
        yield return new WaitForSeconds(3f);

        //Wave 2 시작
        WaveManager.Instance.StartNextWave();
        Debug.Log("Wave 2 시작");

        yield return WaitUntilAllMonsterDead();

        Debug.Log("Wave 2 종료");

        yield return new WaitForSeconds(3f);

        // Wave 3 시작
        WaveManager.Instance.StartNextWave();
        Debug.Log("Wave 3 시작");

        yield return WaitUntilAllMonsterDead();
        Debug.Log("Wave 3 종료");

    }

    private IEnumerator WaitUntilAllMonsterDead()
    {
        while (MonsterPoolManager.Instance.AliveMonsterCount > 0)
        {
            yield return null;
        }
    }
}
