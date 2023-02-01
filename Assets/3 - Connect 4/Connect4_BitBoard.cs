using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect4_BitBoard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Connect4Board board = new Connect4Board();

        Run(board);
    }

    void Run(Connect4Board board)
    {

        System.Random rnd = new System.Random();

        board.Reset();
        List<int> validMoves;

        int x = 0;

        while (true)
        {
            validMoves = board.GetValidMovesLst();

            if(validMoves.Count == 0)
            {
                break;
            }

            int nextColumn = rnd.Next(0, validMoves.Count);
            Debug.Log(nextColumn);

            if (x%2==0) board.FillX(validMoves[nextColumn]);
            else board.FillO(validMoves[nextColumn]);

            board.Print();
            board.PrintState();

            x++;
        }


    }
}

/*
6 13 20 27 34 41 48 55 62
5 12 19 26 33 40 47 54 61     
4 11 18 25 32 39 46 53 60
3 10 17 24 31 38 45 52 59
2  9 16 23 30 37 44 51 58
1  8 15 22 29 36 43 50 57
0  7 14 21 28 35 42 49 56 63

*/



class Connect4Board
{
    private int rows = 7;
    private int cols = 9;


    private ulong xPlayer = 0b0000000_0000000_0000000_0000000_0000000_0000000_0000000_0000000_0000000;
    private ulong oPlayer = 0b0000000_0000000_0000000_0000000_0000000_0000000_0000000_0000000_0000000;
    //private ulong oPlayer = 0b000000_000000_000000_000000_000000_000000_000000_000000_000000;

    int[] height = new int[] {0,7,14,21,28,35,42,49,56};


    public ulong MaskOf(int col)
    {
        ulong move = (ulong)1 << height[col];
        height[col]++;

        return move;

    }

    public void PlayX(int idx)
    {
        ulong mask = MaskOf(idx);
        xPlayer |= mask;
    }

    public void PlayO(int idx)
    {
        ulong mask = MaskOf(idx);
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
        xPlayer = 0b0000000_0000000_0000000_0000000_0000000_0000000_0000000_0000000_0000000;
        oPlayer = 0b0000000_0000000_0000000_0000000_0000000_0000000_0000000_0000000_0000000;
    }

    public void Print()
    {
        
        string boardState = "";

        for (int r = 0; r < rows-1; r++)
        {
            string row = "|";
            for (int c = 0; c < cols; c++)
            {
                // compute mask
                ulong mask = (ulong)1 << r + c * rows;

                // values for player x
                if ((xPlayer & mask) != 0)
                {
                    row += "-X-|";
                    continue;
                }

                // values for player o
                if ((oPlayer & mask) != 0)
                {
                    row += "-O-|";
                    continue;
                }

                // empty
                row += "---|";
            }
            boardState = "|---|---|---|---|---|---|---|---|---|\n" + boardState;
            boardState = $"{row}\n" + boardState;
        }
        boardState = "|---|---|---|---|---|---|---|---|---|\n" + boardState;
        Debug.Log(boardState);
    }

    public void PrintState()
    {
        if (IsWin(xPlayer)) Debug.Log("X WIN");
        else if (IsWin(oPlayer)) Debug.Log("O WIN");
        else Debug.Log("IN PROGRESS");
    }

    bool IsWin(ulong bitboard)
    {
        if ((bitboard & (bitboard >> 6) & (bitboard >> 12) & (bitboard >> 18)) != 0) return true; // diagonal \
        if ((bitboard & (bitboard >> 8) & (bitboard >> 16) & (bitboard >> 24)) != 0) return true; // diagonal /
        if ((bitboard & (bitboard >> 7) & (bitboard >> 14) & (bitboard >> 21)) != 0) return true; // horizontal
        if ((bitboard & (bitboard >> 1) & (bitboard >> 2) & (bitboard >> 3)) != 0) return true; // vertical
        return false;
    }

    public List<int> GetValidMovesLst()
    {
        List<int> moves = new List<int>();
        long TOP = 0b1000000_1000000_1000000_1000000_1000000_1000000_1000000;
        for (int col = 0; col < cols; col++)
        {
            if ((TOP & (1L << height[col])) == 0) moves.Add(col);
        }
        return moves;
    }

}
