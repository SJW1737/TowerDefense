using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFitToGrid : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float padding = 0.5f;

    private void Reset()
    {
        cam = GetComponent<Camera>();
    }

    public void FitToBounds(Bounds b)
    {
        if (cam == null) cam = GetComponent<Camera>();
        if (cam == null) cam = Camera.main;
        if (cam == null) return;

        cam.orthographic = true;

        float aspect = cam.pixelRect.width / cam.pixelRect.height;

        Vector3 p = cam.transform.position;
        cam.transform.position = new Vector3(b.center.x, b.center.y, p.z);

        float sizeByHeight = b.extents.y;
        float sizeByWidth = b.extents.x / aspect;

        cam.orthographicSize = Mathf.Max(sizeByHeight, sizeByWidth) + padding;

        Debug.Log($"[Fit] Screen={Screen.width}x{Screen.height}, pixelRect={cam.pixelRect.width}x{cam.pixelRect.height}, aspect={aspect}");
    }

    public void Fit(int gridWidth, int gridHeight)
    {
        if (cam == null) cam = Camera.main;
        if (cam == null) return;

        cam.orthographic = true;

        Vector3 p = cam.transform.position;
        cam.transform.position = new Vector3(gridWidth * 0.5f, gridHeight * 0.5f, p.z);

        float aspect = cam.aspect;
        if (aspect <= 0.0001f) aspect = 1f;

        float sizeByHeight = gridHeight * 0.5f;
        float sizeByWidth = (gridWidth * 0.5f) / aspect;

        cam.orthographicSize = Mathf.Max(sizeByHeight, sizeByWidth) + padding;
    }
}
