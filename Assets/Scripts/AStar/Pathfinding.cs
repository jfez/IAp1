using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinding : MonoBehaviour
{
    public const int DIAGONAL = 14;
    public const int VERT_HOR = 10;
	Grid grid;

	void Awake() {
		grid = GetComponent<Grid> ();
	}
	
	//MAIN ALGORITHM
	public void FindPath(PathRequest request, Action<PathResult> callback) {

		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;
		
		Node startNode = grid.NodeFromWorldPoint(request.pathStart);
		Node targetNode = grid.NodeFromWorldPoint(request.pathEnd);
		
		
		if (startNode.walkable && targetNode.walkable) {
			//Create structures to contain the evaluated (closed) and non evaluated (open) nodes
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			//Add the startNode to the openSet in order to start the loop with the first node
			openSet.Add(startNode);
			
			while (openSet.Count > 0) {
				//We pick the node in openSet with the lowest fCost (with a list is extremely expensive, therefore, we will optimize it with a heap)
				//Remove that node from the openSet and add to the closedSet because we are not going to use it anymore (we will employ its neighbours)
				Node currentNode = openSet.RemoveFirst();
				closedSet.Add(currentNode);
				
				//We have found the path and we rebuild it
				if (currentNode == targetNode) {
					pathSuccess = true;
					break;
				}
				
				//We are going to see each neighbour
				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
					//The neighbour is in an obstacle or has been evaluated 
					if (!neighbour.walkable || closedSet.Contains(neighbour)) {
						continue;
					}
					
					//Calculate if the way to reach that neighbour node is the most optimized and add it to the openSet if it was not there
                	//If we have found a most optimized way to that node, we change its parent to change the path
					int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour) + neighbour.movementPenalty;
					if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
						neighbour.gCost = newMovementCostToNeighbour;
						neighbour.hCost = GetDistance(neighbour, targetNode);
						neighbour.parent = currentNode;
						
						if (!openSet.Contains(neighbour))
							openSet.Add(neighbour);
						else
							openSet.UpdateItem(neighbour);
					}
				}
			}
		}
		if (pathSuccess) {
			waypoints = RetracePath(startNode,targetNode);
            pathSuccess = waypoints.Length > 0;
		}
        callback(new PathResult(waypoints, pathSuccess, request.callback));
		
	}
	
	Vector3[] RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}

		grid.path = path;
		
		Vector3[] waypoints = SimplifyPath(path);
		//Get the correct order of the path (start --> end)
		Array.Reverse(waypoints);
		return waypoints;

		

	}

	//Symplify the path by directions (horizontal, vertical and diaonal)
	Vector3[] SimplifyPath(List<Node> path) {
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;
		
		for (int i = 1; i < path.Count; i ++) {
			//Check if the path is changing its direction
			Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX, path[i-1].gridY - path[i].gridY);
			if (directionNew != directionOld) {
				waypoints.Add(path[i].worldPosition);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray();
	}

	int GetDistance(Node nodeA, Node nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		//Equation to calculate the shortest distance
        if (dstX > dstY)
			return DIAGONAL*dstY + VERT_HOR* (dstX-dstY);
		return DIAGONAL*dstX + VERT_HOR * (dstY-dstX);
	}
}
