using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartTest : MonoBehaviour
{private void Start()
    {
        Debug.Log("게임 시작 테스트 → 웨이브 시작");
        WaveManager.Instance.StartGame();
    }
}
