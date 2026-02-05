using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterMovement : MonoBehaviour
{
    public event Action OnReachedEnd;

    private float moveSpeed;
    private float currentSpeed;
    private float slowMultiplier = 1f;

    private Queue<Node> pathQueue;

    private Coroutine slowCoroutine;
    private Coroutine frozenCoroutine;
    private bool isFrozen;

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
        if (!gameObject.activeInHierarchy)
            return;

        if (isFrozen)
            return;

        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine);

        slowCoroutine = StartCoroutine(SlowRoutine(ratio, duration));
    }

    public void ApplyFrozen(float duration)
    {
        if (frozenCoroutine != null)
            StopCoroutine(frozenCoroutine);

        frozenCoroutine = StartCoroutine(FrozenRoutine(duration));
    }

    private IEnumerator SlowRoutine(float ratio, float duration)
    {
        slowMultiplier = 1f - ratio;
        yield return new WaitForSeconds(duration);
        slowMultiplier = 1f;
    }

    private IEnumerator FrozenRoutine(float duration)
    {
        isFrozen = true;
        yield return new WaitForSeconds(duration);
        isFrozen = false;
    }

    float GetFinalSpeed()
    {
        if (isFrozen) return 0f;
        return moveSpeed * slowMultiplier;
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
                transform.position = Vector3.MoveTowards(transform.position, targetpos, GetFinalSpeed() * Time.deltaTime);
                yield return null;
            }

            transform.position = targetpos;
        }

        OnReachedEnd?.Invoke();
    }

    public void ResetStatus()
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
            slowCoroutine = null;
        }

        if (frozenCoroutine != null)
        {
            StopCoroutine(frozenCoroutine);
            frozenCoroutine = null;
        }

        StopAllCoroutines();

        isFrozen = false;
        slowMultiplier = 1f;
    }

    public void ResetMovement()
    {
        ResetStatus();
        currentSpeed = moveSpeed;
    }
}
