using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTT_BitBoard_1 : MonoBehaviour
{


    void Start()
    {
        Board board = new Board();

        Run(board, new int[] { 0, 1,2}, new int[] { 3, 5, 8 });
    }

    void Run(Board board,int[] xs,int[] os)
    {
        board.Reset();
        board.FillX(xs);
        board.FillO(os);
        board.PrintState();
        board.Print();

    }
}

/*
| ---| ---| ---|
| 0  | 1  | 2  |
| ---| ---| ---|
| 3  | 4  | 5  |
| ---| ---| ---|
|  6 |  7 | 8  |
| ---| ---| ---|

*/

class Board
{
    private int rows = 3;
    private int cols = 3;

    private int xPlayer = 0b000000000;
    private int oPlayer = 0b000000000;
    private int fullBoard = 0b111111111;

    private int[] winConditions = new int[] {
        0b111000000,
        0b000111000,
        0b000000111,
        0b100100100,
        0b010010010,
        0b001001001,
        0b100010001,
        0b001010100
    };

    public int MaskOf(int idx)
    {
        return 1 << idx;
    }

    public void PlayX(int idx)
    {
        int mask = MaskOf(idx);
        xPlayer |= mask;
    }

    public void PlayO(int idx)
    {
        int mask = MaskOf(idx);
        oPlayer |= mask;
    }

    public void FillX(params int[] idxs)
    {
        foreach (int idx in idxs)
        {
            PlayX(idx);
        }
    }

    public void FillO(params int[] idxs)
    {
        foreach (int idx in idxs)
        {
            PlayO(idx);
        }
    }

    public void Reset()
    {
        xPlayer = 0b000000000;
        oPlayer = 0b000000000;
    }

    public void Print()
    {
        for (int y = 0; y < rows; y++)
        {
            string row = "|";
            for (int x = 0; x < cols; x++)
            {
                // compute mask
                int mask = MaskOf(x + y * cols);

                Debug.Log(x + y * cols);
                Debug.Log($"Binary: {Convert.ToString(mask, toBase: 2)}");

                // values for player x
                if ((xPlayer & mask) != 0)
                {
                    row += " X |";
                    continue;
                }

                // values for player o
                if ((oPlayer & mask) != 0)
                {
                    row += " O |";
                    continue;
                }

                // empty
                row += "   |";
            }
            Console.WriteLine("|---|---|---|");
            Console.WriteLine(row);
        }
        Console.WriteLine("|---|---|---|");
        Console.WriteLine("");
    }

    public void PrintState()
    {
        foreach (int condition in winConditions)
        {
            // check if x wins
            if ((xPlayer & condition) == condition)
            {
                Console.WriteLine("X Won");
                return;
            }

            // check if o wins
            if ((oPlayer & condition) == condition)
            {
                Console.WriteLine("O Won");
                return;
            }
        }

        // check draws
        if ((xPlayer | oPlayer) == fullBoard)
        {
            Console.WriteLine("Draw");
            return;
        }

        // game in progress
        Console.WriteLine("In progress");
    }
}
