using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.WSA;

public class MonsterMovement : MonoBehaviour
{
    private float moveSpeed;

    private Queue<Node> pathQueue;

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
                transform.position = Vector3.MoveTowards(transform.position, targetpos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = targetpos;
        }

        OnArrived();
    }

    private void OnArrived()
    {
        Debug.Log("몬스터 도착 및 공격");
        Destroy(gameObject);
    }
}
