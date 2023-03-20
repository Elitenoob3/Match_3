using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField]
    private GameObject[] rows;

    private Transform[,] board = new Transform[6,6];
    private bool[,] toDelete = new bool[6,6];

    private Transform t1, t2;

    [SerializeField]
    private GameObject Tile;

    private void Start()
    {
        Generate();

        t1 = null;
        t2 = null;
    }

    private void Generate() 
    {
        int i, j;

        for (i = 0; i < 6; i++)
        {
            Slots slots = rows[i].GetComponent<Slots>();

            for (j = 0; j < 6; j++)
            {
                board[i, j] = slots.slotContainer[j];

                Coords coords = board[i, j].gameObject.GetComponent<Coords>();
                coords.pos.x = i;
                coords.pos.y = j;
            }
        }

        for (i = 0; i < 6; i++)
        {
            TileTop();
            TileDrop();
        }
    }

    //Drop all tiles one level
    private void TileDrop()
    {
        int i, j;

        for (i = 5; i > 0; i--)
        {
            for (j = 0; j < 6; j++)
            {
                if (board[i, j].childCount == 0 && board[i - 1, j].childCount > 0)
                {
                    board[i - 1, j].GetChild(0).SetParent(board[i, j].transform);
                }
            }
        }
    }

    //Generate tiles on the top level
    private void TileTop()
    {
        int j;

        for (j = 0; j < 6; j++)
        {
            Instantiate(Tile, board[0, j]);
        }
    }

    public void TileToSwap(Transform tr)
    {
        if (!t1)
        {
            t1 = tr;
        }
        else if (t1 && t1.Equals(tr))
        {
            t1 = null;
        }
        else
        {
            t2 = tr;

            TileSwap();

            t1 = null;
            t2 = null;
        }
    }

    private void TileSwap()
    {
        Vector2 Pos1 = t1.parent.GetComponent<Coords>().pos;
        Vector2 Pos2 = t2.parent.GetComponent<Coords>().pos;

        if ((Pos1 - Pos2).magnitude == 1)
        {
            t1.SetParent(board[(int)Pos2.x, (int)Pos2.y]);
            t2.SetParent(board[(int)Pos1.x, (int)Pos1.y]);

            TileMarkDelete(Pos1);
            TileMarkDelete(Pos2);

            TileDelete();
        }
        else return;
    }

    private void TileMarkDelete(Vector2 pos)
    {
        int XCounter = 1;
        int YCounter = 1;

        int value = board[(int)pos.x, (int)pos.y].GetChild(0).GetComponent<Tile>().value;
        Debug.Log("Checking for" + value);

        int i;

        //Downwards
        for (i = (int)pos.x + 1; i < 6; i++)
        {
            if (board[i, (int)pos.y].GetChild(0).GetComponent<Tile>().value == value)
            {
                YCounter++;
            }
            else
            {
                break;
            } 
        }
        //Upwards
        for (i = (int)pos.x - 1; i >= 0; i--)
        {
            if (board[i, (int)pos.y].GetChild(0).GetComponent<Tile>().value == value)
            {
                YCounter++;
            }
            else
            {
                break;
            }
        }

        if (YCounter >= 3)
        {
            //Downwards
            for (i = (int)pos.x + 1; i < 6; i++)
            {
                if (board[i, (int)pos.y].GetChild(0).GetComponent<Tile>().value == value)
                {
                    toDelete[i, (int)pos.y] = true;
                    board[i, (int)pos.y].GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.black;
                }
                else
                {
                    break;
                }
            }
            //Upwards
            for (i = (int)pos.x - 1; i >= 0; i--)
            {
                if (board[i, (int)pos.y].GetChild(0).GetComponent<Tile>().value == value)
                {
                    toDelete[i, (int)pos.y] = true;
                    board[i, (int)pos.y].GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.black;
                }
                else
                {
                    break;
                }
            }

            toDelete[(int)pos.x, (int)pos.y] = true;
            board[(int)pos.x, (int)pos.y].GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.black;
        }
        
        //Right
        for (i = (int)pos.y + 1; i < 6; i++)
        {
            if (board[(int)pos.x, i].GetChild(0).GetComponent<Tile>().value == value)
            {
                XCounter++;
            }
            else
            {
                break;
            }
        }
        //Left
        for (i = (int)pos.y - 1; i >= 0; i--)
        {
            if (board[(int)pos.x, i].GetChild(0).GetComponent<Tile>().value == value)
            {
                XCounter++;
            }
            else
            {
                break;
            }
        }

        if (XCounter >= 3)
        {
            //Right
            for (i = (int)pos.y + 1; i < 6; i++)
            {
                if (board[(int)pos.x, i].GetChild(0).GetComponent<Tile>().value == value)
                {
                    toDelete[(int)pos.x, i] = true;
                    board[(int)pos.x, i].GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.black;
                }
                else
                {
                    break;
                }
            }
            //Left
            for (i = (int)pos.y - 1; i >= 0; i--)
            {
                if (board[(int)pos.x, i].GetChild(0).GetComponent<Tile>().value == value)
                {
                    toDelete[(int)pos.x, i] = true;
                    board[(int)pos.x, i].GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.black;
                }
                else
                {
                    break;
                }
            }

            toDelete[(int)pos.x, (int)pos.y] = true;
            board[(int)pos.x, (int)pos.y].GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.black;
        }
    }

    private void TileDelete()
    {
        int i, j;

        for (i = 0; i < 6; i++)
        {
            for (j = 0; j < 6; j++)
            {
                
            }
        }
    }
}
