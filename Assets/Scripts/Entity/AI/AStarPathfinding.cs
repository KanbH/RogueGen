using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    [SerializeField] private NodeManager nodeManager;
    private Node[,] _nodes; // Reference to the grid of nodes (NodeManager array)

    public List<Node> FindPath(Vector2 startWorldPosition, Vector2 targetWorldPosition)
    {
        CheckNodesNotNull();
        // Convert world positions to grid coordinates
        Vector2Int startNodePos = WorldToGrid(startWorldPosition);
        Vector2Int targetNodePos = WorldToGrid(targetWorldPosition);
        Node startNode = _nodes[startNodePos.x, startNodePos.y];
        Node targetNode = _nodes[targetNodePos.x, targetNodePos.y];

        if (startNode.IsWall || targetNode.IsWall)
        {
            return null; // Invalid start or target
        }

        // Initialize open and closed lists
        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();
        openList.Add(startNode);

        // Track the costs of the nodes
        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
        Dictionary<Node, float> gCost = new Dictionary<Node, float>();
        Dictionary<Node, float> fCost = new Dictionary<Node, float>();

        gCost[startNode] = 0;
        fCost[startNode] = Heuristic(startNode, targetNode);

        while (openList.Count > 0)
        {
            Node currentNode = GetLowestFCostNode(openList, fCost);
            if (currentNode == targetNode)
            {
                // Path found, construct and return it
                return ReconstructPath(cameFrom, targetNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (closedList.Contains(neighbor) || neighbor.IsWall)
                {
                    continue; // Skip if already evaluated or is a wall
                }

                // Calculate the G cost for the neighbor
                float tentativeGCost = gCost[currentNode] + GetDistance(currentNode, neighbor);

                if (!openList.Contains(neighbor))
                {
                    openList.Add(neighbor); // New node to evaluate
                }
                else if (tentativeGCost >= gCost[neighbor])
                {
                    continue; // No need to update path to this neighbor
                }

                // Record the best path so far to this node
                cameFrom[neighbor] = currentNode;
                gCost[neighbor] = tentativeGCost;
                fCost[neighbor] = gCost[neighbor] + Heuristic(neighbor, targetNode);
            }
        }

        // No path found
        return null;
    }

    // Reconstruct path by backtracking from target
    private List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node currentNode)
    {
        List<Node> path = new List<Node>();
        while (cameFrom.ContainsKey(currentNode))
        {
            path.Add(currentNode);
            currentNode = cameFrom[currentNode];
        }
        path.Reverse(); // Reverse to get the path from start to target
        return path;
    }

    // Get neighbors with diagonal movement check
    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        int x = node.gridPositionX;
        int y = node.gridPositionY;

        // Check all 8 possible directions (cardinal + diagonals)
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue; // Skip the current node itself

                int checkX = x + dx;
                int checkY = y + dy;

                if (IsWithinBounds(checkX, checkY))
                {
                    Node neighbor = _nodes[checkX, checkY];

                    // Check for diagonal corner rules
                    if (Mathf.Abs(dx) == 1 && Mathf.Abs(dy) == 1)
                    {
                        // Ensure both adjacent tiles are walkable for diagonal move
                        if (_nodes[x + dx, y].IsWall || _nodes[x, y + dy].IsWall)
                        {
                            continue; // Skip diagonal if adjacent walls block movement
                        }
                    }

                    neighbors.Add(neighbor);
                }
            }
        }

        return neighbors;
    }

    // Convert world space position to grid coordinates
    private Vector2Int WorldToGrid(Vector2 worldPos)
    {
        // Assuming grid is placed at (0,0) in world space
        return new Vector2Int(Mathf.FloorToInt(worldPos.x), Mathf.FloorToInt(worldPos.y));
    }

    // Get the heuristic distance (Euclidean distance with diagonal movement)
    private float Heuristic(Node a, Node b)
    {
        int dx = Mathf.Abs(a.gridPositionX - b.gridPositionX);
        int dy = Mathf.Abs(a.gridPositionY - b.gridPositionY);
        return Mathf.Sqrt(dx * dx + dy * dy); // Diagonal distance
    }

    // Get the distance between two nodes (1 for straight, 1.4 for diagonal)
    private float GetDistance(Node a, Node b)
    {
        int dx = Mathf.Abs(a.gridPositionX - b.gridPositionX);
        int dy = Mathf.Abs(a.gridPositionY - b.gridPositionY);
        return (dx == 1 && dy == 1) ? 1.4f : 1.0f; // Diagonal = 1.4, straight = 1
    }

    // Get the node with the lowest F cost
    private Node GetLowestFCostNode(List<Node> openList, Dictionary<Node, float> fCost)
    {
        Node lowestFCostNode = openList[0];
        foreach (Node node in openList)
        {
            if (fCost[node] < fCost[lowestFCostNode])
            {
                lowestFCostNode = node;
            }
        }
        return lowestFCostNode;
    }

    // Check if coordinates are within grid bounds
    private bool IsWithinBounds(int x, int y)
    {
        return x >= 0 && x < _nodes.GetLength(0) && y >= 0 && y < _nodes.GetLength(1);
    }

    private void CheckNodesNotNull()
    {
        if (_nodes == null)
        {
            _nodes = nodeManager.GetNodes();
        }
    }
}
