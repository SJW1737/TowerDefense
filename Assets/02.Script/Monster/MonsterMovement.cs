using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class MonsterMovement : MonoBehaviour
{
    public event Action OnReachedEnd;

    private float moveSpeed;
    private float currentSpeed;

    private Queue<Node> pathQueue;
    private Coroutine slowCoroutine;

    private Pathfinder pathfinder;
    private GridManager gridManager;

    private void Awake()
    {
        pathfinder = Pathfinder.Instance;
        gridManager = GridManager.Instance;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
        currentSpeed = speed;
    }

    public void ApplySlow(float ratio, float duration)
    {
        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine);

        slowCoroutine = StartCoroutine(SlowRoutine(ratio, duration));
    }

    private IEnumerator SlowRoutine(float ratio, float duration)
    {
        currentSpeed = moveSpeed * (1f - ratio);
        yield return new WaitForSeconds(duration);
        currentSpeed = moveSpeed;
    }

    public void Setpath()
    {
        List<Node> path = pathfinder.FindPath(gridManager.startNode, gridManager.endNode);

        pathQueue = new Queue<Node>(path);
        pathQueue.Dequeue();    //첫 노드 제거(현위치)

        StartCoroutine(MovePath());
    }

    private IEnumerator MovePath()
    {
        while (pathQueue.Count > 0)
        {
            Node next = pathQueue.Dequeue();

            Vector3 targetpos = new Vector3(next.x + 0.5f, next.y + 0.5f, transform.position.z);    //노드 중앙 보정

            while (Vector3.Distance(transform.position, targetpos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetpos, currentSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = targetpos;
        }

        OnReachedEnd?.Invoke();
    }
}
