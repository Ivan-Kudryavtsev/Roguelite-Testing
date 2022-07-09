using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionComparer : IComparer<Collider2D>
{
    private Vector2 compPosition;

    public void SetPosition(Vector2 newpos)
    {
        compPosition = newpos;
    }

    public int Compare(Collider2D x, Collider2D y) {
        return (int) (Vector2.Distance(x.transform.position,compPosition) - Vector2.Distance(y.transform.position, compPosition));
    }

    public int Compare(object x, object y)
    {
        throw new System.Exception("Wrong object type.");
        //return 0;
    }
}
