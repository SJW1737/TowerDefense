using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondTest : MonoBehaviour
{
    [SerializeField] private int addAmount = 100;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SaveManager.Instance.AddDiamond(addAmount);
            Debug.Log($"[DEBUG] Diamond +{addAmount}");
        }
    }
}
