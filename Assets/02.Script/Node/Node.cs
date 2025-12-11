using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int x;
    public int y;
    public bool isBlocked;

    public int gCost;
    public int hCost;
    public int fCost => gCost + hCost;

    public Node parent;

    public Node(int x, int y)
    {
        this.x = x;
        this.y = y;
        isBlocked = true;
    }
}