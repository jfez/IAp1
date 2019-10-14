using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public const int DIAGONAL = 14;
    public const int VERT_HOR = 10;
    public Transform seeker, target;
	Grid grid;

	void Awake() {
		grid = GetComponent<Grid> ();
	}

	void Update() {
		FindPath (seeker.position, target.position);
	}

	//MAIN ALGORITHM
    void FindPath(Vector3 startPos, Vector3 targetPos) {
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);

        //print (startNode.gridX + ", " + startNode.gridY);
        //print (targetNode.gridX + ", " + targetNode.gridY);

		
        //Create structures to contain the evaluated (closed) and non evaluated (open) nodes
        List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();

		//Add the startNode to the openSet in order to start the loop with the first node
        openSet.Add(startNode);

		while (openSet.Count > 0) {     //we do the loop while remains non evaluated nodes
			
            //We pick the node in openSet with the lowest fCost (with a list is extremely expensive, therefore, we will optimize it with a heap)
            Node node = openSet[0];
			for (int i = 1; i < openSet.Count; i ++) {
				if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost) {
					if (openSet[i].hCost < node.hCost)
						node = openSet[i];
				}
			}

			//Remove that node from the openSet and add to the closedSet because we are not going to use it anymore (we will employ its neighbours)
            openSet.Remove(node);
			closedSet.Add(node);

			
            //We have found the path and we rebuild it
            if (node == targetNode) {
				RetracePath(startNode,targetNode);
				return;
			}

			//We are going to see each neighbour
            foreach (Node neighbour in grid.GetNeighbours(node)) {
				//The neighbour is in an obstacle or has been evaluated 
                if (!neighbour.walkable || closedSet.Contains(neighbour)) {
					continue;
				}

				//Calculate if the way to reach that neighbour node is the most optimized and add it to the openSet if it was not there
                //If we have found a most optimized way to that node, we change its parent to change the path
                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = node;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
	}

	void RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		
        //Get the correct order of the path (start --> end)
        path.Reverse();

		grid.path = path;
        /*foreach (Node n in path){
            print (n.gridX + ", " + n.gridY);
            
        }*/

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
