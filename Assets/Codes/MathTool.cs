using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathTool{

    /// <summary>
    /// 获取指定坐标点为中心的极坐标 
    /// </summary>
    /// <param name="point">极坐标原点</param>
    /// <param name="position">世界直角坐标</param>
    /// <returns>Vector3:(经度，纬度，半径)</returns>
    public static Vector3 GetPolarCoordinates(Vector3 point,Vector3 position)
    {
        Vector3 relePos = position - point;
        Vector3 polarPos
            = new Vector3(
                Mathf.Atan(relePos.y/relePos.x),                  //φ：经度
                Mathf.Acos(relePos.z / relePos.magnitude),        //θ：纬度
                relePos.magnitude                                 //r：半径
            );
        return polarPos;
    }
    public static Vector3 GetPolarCoordinates(Vector3 position)
    {
        return GetPolarCoordinates(Vector3.zero,position);
    }
    /// <summary>
    /// 通过极坐标坐标点获取直角坐标 
    /// </summary>
    /// <param name="point">极坐标原点</param>
    /// <param name="polarPos">极坐标</param>
    /// <returns>Vector3</returns>
    public static Vector3 GetRectangularCoordinates(Vector3 point, Vector3 polarPos)
    {
        Vector3 position
            = new Vector3(
                polarPos.z * Mathf.Sin(polarPos.y) * Mathf.Cos(polarPos.x),
                polarPos.z * Mathf.Sin(polarPos.y) * Mathf.Sin(polarPos.x),       
                polarPos.z*Mathf.Cos(polarPos.y)                               
            );
        return position+ point;
    }
    public static Vector3 GetRectangularCoordinates(Vector3 position)
    {
        return GetRectangularCoordinates(Vector3.zero, position);
    }
}
