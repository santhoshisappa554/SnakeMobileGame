using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileScript : MonoBehaviour
{
    protected Vector2Int gridPos;

    public Vector2Int GridPos { get { return gridPos; } }

    public void MoveToGrid(int x, int y)
    {
        float xf = x;
        float yf = y;
        transform.position = new Vector3(GridManager.Instance.StartPoint.x + (xf / GridManager.Instance.GridSize), GridManager.Instance.StartPoint.y + (yf / GridManager.Instance.GridSize),transform.position.z);
        gridPos = new Vector2Int(x, y);
    
    
    }
    public void MovetoTile(TileScript tile)
    {
        MoveToGrid(tile.gridPos.x, tile.gridPos.y);
    }


}
