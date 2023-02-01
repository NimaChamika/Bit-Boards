using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitWiseOperations : MonoBehaviour
{
    private void Start()
    {


        //ComplementCheck();

        //LeftShiftCheck1();
        LeftShiftCheck2();

        //RightShiftCheck();
    }

    void ComplementCheck()
    {
        uint a = 0b_0000_1111_0000_1111_0000_1111_0000_1100;
        uint b = ~a;
        Debug.Log(Convert.ToString(b, toBase: 2));
    }

    //The << operator shifts its left-hand operand left by the number of bits defined by its right-hand operand
    void LeftShiftCheck1()
    {
        uint x = 0b_1100_1001_0000_0000_0000_0000_0001_0001;
        Debug.Log($"Before: {Convert.ToString(x, toBase: 2)}");

        uint y = x << 4;
        Debug.Log($"After:  {Convert.ToString(y, toBase: 2)}");
    }

    void LeftShiftCheck2()
    {
        int a = 1 << 3;
        int b = 1 << 5;
        Debug.Log(Convert.ToString(a, toBase: 2));

        int x = 0b000000000;
        x = x | a;
        x = x | b;

        Debug.Log(Convert.ToString(x, toBase: 2));
    }

    //The >> operator shifts its left-hand operand right by the number of bits defined by its right-hand operand.
    void RightShiftCheck()
    {
        uint x = 0b_1001;
        Debug.Log($"Before: {Convert.ToString(x, toBase: 2)}");

        uint y = x >> 2;
        Debug.Log($"After:  {Convert.ToString(y, toBase: 2)}");
    }

 
}
