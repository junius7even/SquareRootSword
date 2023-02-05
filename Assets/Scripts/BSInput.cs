using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BSInputType { PLAYERATTACK, ENDTURN, NONE }
public class BSInput
{
    public BSInputType type = BSInputType.NONE;
    public int cardValue = 0;
    public Operator op = Operator.Plus;
    public DragIndicator dragIndicator;
    
    public BSInput(BSInputType a_type, int a_cardValue, Operator a_op)
    {
        type = a_type;
        cardValue = a_cardValue;
        op = a_op;
    }
    public BSInput() {}
};