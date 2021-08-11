using System;
using System.Collections.Generic;
using System.Linq;
using Commons;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Octree<T>
{
    /* Level3(空間を8*8*8=512分割する)Octree */
    float x, y, z;
    float w, h, d;

    private bool debugMode = true;
    public List<T>[] linearOctree = new List<T>[512];

    public Octree(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = x / 8;  //widthUnitSize
        this.h = y / 8;  //heightUnitSize
        this.d = z / 8;  //depthUnitSize

        for (int i = 0; i < 512; i++)
        {
            this.linearOctree[i] = new List<T>();
        }
    }

    public void Insert(float x, float y, float z, T content)
    {
        int mo = XyzToMortonOrder(30.0f, 15.0f, 1.0f);
        this.linearOctree[mo].Add(content);
    }

    private int BitSeparate3D(int n)
    {
        int s = n;
        s = (s | s << 8) & 0x0000f00f;
        s = (s | s << 4) & 0x000c30c3;
        s = (s | s << 2) & 0x00249249;
        return s;
    }

    public int XyzToMortonOrder(float x, float y, float z)
    {
        /*
            y        z
            ^       /
            |  +--------+
            | / 6    7 /|
            |/ 2   3  /7|
            +--------+  |         
            | 2   3  | 5+
            |        | /
            | 0   1  |/
            +--------+--------> x
            MORTON ORDER 
        */
        // int[] point = { (int)Math.Floor(x / this.w), (int)Math.Floor(y / this.h), (int)Math.Floor(z / this.d) };
        int[] point = { (int)Math.Floor(x / this.w), (int)Math.Floor(y / this.h), (int)Math.Floor(z / this.d) };
        // 座標からbit演算で空間を特定する
        int o = BitSeparate3D(point[0]) | BitSeparate3D(point[1]) << 1 | BitSeparate3D(point[2]) << 2;
        return o;
    }
}

