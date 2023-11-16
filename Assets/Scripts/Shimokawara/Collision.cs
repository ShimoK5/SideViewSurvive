
using UnityEngine;

public static class Collision 
{
    public static bool BoundingBox(Vector3 a_pos, Vector3 b_pos,float a_x_size,float a_y_size, float b_x_size,float b_y_size)
    {
        if (a_pos.y + a_y_size / 2 > b_pos.y - b_y_size / 2 &&
            a_pos.y - a_y_size / 2 < b_pos.y + b_y_size / 2 &&
            a_pos.x + a_x_size / 2 > b_pos.x - b_x_size / 2 &&
            a_pos.x - a_x_size / 2 < b_pos.x + b_x_size / 2)
            return true;
        return false;
    }
}
