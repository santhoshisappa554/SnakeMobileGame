using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] SnakeTile snakeTilePrefab;

    private List<SnakeTile> snakePieces;
    public List<SnakeTile> SnakePieces { get { return SnakePieces; } }

   
    public void Intializesnake()
    {
        snakePieces = new List<SnakeTile>();
        transform.position = GridManager.Instance.GetRandomTile(5).transform.position;
        SnakeTile snaketile = Instantiate(snakeTilePrefab, transform.position, Quaternion.identity);
        snaketile.transform.parent = transform;
        SnakePieces.Add(snaketile);
       
    }
    private void AppleEaten()
    {
        AddPiece();
    }
    void AddPiece()
    {
        SnakeTile snakeTile = Instantiate(snakeTilePrefab, transform.position, Quaternion.identity);
        snakeTile.MovetoTile(snakePieces[snakePieces.Count - 1]);
        switch (PlayerInput.dir)
        {
            case PlayerInput.Direction.up:
                snakeTile.MoveDown();
                break;
            case PlayerInput.Direction.down:
                snakeTile.MoveUp();
                break;
            case PlayerInput.Direction.left:
                snakeTile.MoveRight();
                break;
            case PlayerInput.Direction.right:
                snakeTile.MoveLeft();
                break;
            default:
                break;
        }
        
        
        snakeTile.MoveLeft();
        snakePieces.Add(snakeTile);
    }
    public void Tick()
    {
        Move();   
    }

    private void Move()
    {
        for(int i = snakePieces.Count - 1; i >= 0; i--)
        {
            if (i == 0)
            {
                switch (PlayerInput.dir)
                {
                    case PlayerInput.Direction.up:
                        snakePieces[i].MoveUp();
                        break;
                    case PlayerInput.Direction.down:
                        snakePieces[i].MoveDown();
                        break;
                    case PlayerInput.Direction.right:
                        snakePieces[i].MoveRight();
                        break;
                    case PlayerInput.Direction.left:
                        snakePieces[i].MoveLeft();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                snakePieces[i].MovetoTile(snakePieces[i]);
            }
        }
    }
    public SnakeTile GetHeadTile()
    {
        return snakePieces[0];
    }

}
