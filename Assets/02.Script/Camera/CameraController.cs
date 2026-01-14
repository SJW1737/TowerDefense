using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("Base Reference")]
    [SerializeField] private float baseAspect = 760f / 1280f;   //기본 해상도 비율
    [SerializeField] private float baseOrthoSize = 10f;         //기본 카메라 사이즈

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
        Fit();
    }

    void OnValidate()
    {
        if (cam == null)
            cam = GetComponent<Camera>();
        Fit();
    }

    void Fit()
    {
        float currentAspect = (float)Screen.width / Screen.height;

        if (currentAspect < baseAspect)
        {
            //더 세로로 긴 화면
            cam.orthographicSize = baseOrthoSize * (baseAspect / currentAspect);
        }
        else
        {
            //기준보다 넓은 화면
            cam.orthographicSize = baseOrthoSize;
        }
    }
}
