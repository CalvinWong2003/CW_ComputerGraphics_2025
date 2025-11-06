using Unity.VisualScripting;
using UnityEngine;

public class OutCode
{
    internal bool up;
    internal bool down;
    internal bool left;
    internal bool right;

    public OutCode(Vector2 v)
    {
        up = v.y > 1;
        down = v.y < -1;
        left = v.x < -1;
        right = v.x > 1;
    }
    public OutCode() 
    {
        up = false;
        down = false;
        left = false;
        right = false;
    }
    public OutCode(OutCode oc)
    {
        up = oc.up;
        down = oc.down;
        left = oc.left;
        right = oc.right;
    }
    public OutCode(bool up, bool down, bool left, bool right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    public static OutCode operator +(OutCode o1, OutCode o2)
    {
        return new OutCode(o1.up || o2.up, o1.down || o2.down, o1.left || o2.left, o1.right || o2.right);
    }
    public static OutCode operator *(OutCode o1, OutCode o2)
    {
        return new OutCode(o1.up && o2.up, o1.down && o2.down, o1.left && o2.left, o1.right && o2.right);
    }
    public static bool operator ==(OutCode o1, OutCode o2)
    {
        return (o1.up == o2.up) && (o1.down == o2.down) && (o1.left == o2.left) && (o1.right == o2.right);
    }
    public static bool operator !=(OutCode o1, OutCode o2)
    {
        return !(o1 == o2) ;
    }
    //Logical Operator OR (+), Operator AND (*), Operator == (if two OutCodes are equal), Operator != (if two OutCodes are not equal),
    //display(), print(), ToString()
}
