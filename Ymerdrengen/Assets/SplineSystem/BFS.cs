using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BFS {

    List<Node> openList;
    List<Node> closedList;

    public Stack<BezierSpline> findNearestFinalDestination(BezierSpline startSpline)
    {
        openList = new List<Node>();
        closedList = new List<Node>();

        openList.Add(new Node(startSpline.index, null, startSpline));

        while (openList.Count > 0)
        {

            Node currentNode = openList[0];

            Debug.Log(currentNode.spline.endPoint);
            if (currentNode.spline.endPoint)
                return backTrackPath(currentNode);

            for (int i = 0; i < currentNode.spline.nextSplines.Count; i++)
            {
                Node frontNode = new Node(currentNode.spline.nextSplines[i].index, currentNode, currentNode.spline.nextSplines[i]);
                addNodetoOpenList(frontNode);
            }

            for (int i = 0; i < currentNode.spline.previusSplines.Count; i++)
            {
                Node frontNode = new Node(currentNode.spline.previusSplines[i].index, currentNode, currentNode.spline.previusSplines[i]);
                addNodetoOpenList(frontNode);
            }

            openList.RemoveAt(0);
            closedList.Add(currentNode);
        }

        Debug.LogError("No path could be found check if path has a final point");
        return null;
    }

    private void addNodetoOpenList(Node frontNode)
    {
        
        if (!closedList.Contains(frontNode))
        {
            if (!openList.Contains(frontNode))
            {
                openList.Add(frontNode);
            }

        }
    }

    private Stack<BezierSpline> backTrackPath(Node currentNode)
    {
        Stack<BezierSpline> path = new Stack<BezierSpline>();
        while(currentNode.parent != null)
        {
            path.Push(currentNode.spline);
            currentNode = currentNode.parent;
        }
        path.Push(currentNode.spline);
        return path;
    }
}

public class Node
{
    
    public int index;
    public Node parent;
    public BezierSpline spline;

    public Node(int index, Node parent, BezierSpline spline)
    {
        this.index = index;
        this.parent = parent;
        this.spline = spline;
    }

    public override bool Equals(System.Object obj)
    {
        // If parameter is null return false.
        if (obj == null)
        {
            return false;
        }

        // If parameter cannot be cast to Point return false.
        Node n = obj as Node;
        if ((System.Object)n == null)
        {
            return false;
        }

        // Return true if the fields match:
        return (index == n.index);
    }

    public bool Equals(Node n)
    {
        // If parameter is null return false:
        if ((object)n == null)
        {
            return false;
        }

        // Return true if the fields match:
        return (index == n.index);
    }

    public override int GetHashCode()
    {
        return index;
    }



}
