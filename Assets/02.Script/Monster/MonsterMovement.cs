using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.WSA;

public class MonsterMovement : MonoBehaviour
{
    public float speed = 1f;

    private Queue<Node> pathQueue;
    private List<Node> path;

    Pathfinder pathfinder;

    public void Setpath()
    {
        pathQueue = new Queue<Node>(path);

        path = pathfinder.FindPath(path[0], path[path.Count - 1]);
    }

}
