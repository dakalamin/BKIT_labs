using System;
using System.Collections.Generic;
using System.Text;

namespace Containers
{
    public class Matrix3D<T>
    {
        int maxX, maxY, maxZ;
        T nullElement;

        Dictionary<string, T> matrix = new Dictionary<string, T>();

        public Matrix3D(int x, int y, int z, T nullElementParam)
        {
            maxX = x;
            maxY = y;
            maxZ = z;
            nullElement = nullElementParam;
        }

        public T this[int x, int y, int z]
        {
            get
            {
                CheckBounds(x, y, z);
                string key = DictKey(x, y, z);

                if (this.matrix.ContainsKey(key))
                {
                    return this.matrix[key];
                }
                else
                {
                    return this.nullElement;
                }
            }
            set
            {
                CheckBounds(x, y, z);
                string key = DictKey(x, y, z);

                this.matrix.Add(key, value);
            }
        }

        void CheckBounds(int x, int y, int z)
        {
            if (x < 0 || x >= this.maxX) throw new IndexOutOfRangeException($"x={x} is out of boundary");
            if (y < 0 || y >= this.maxY) throw new IndexOutOfRangeException($"y={y} is out of boundary");
            if (z < 0 || z >= this.maxZ) throw new IndexOutOfRangeException($"z={z} is out of boundary");
        }

        string DictKey(int x, int y, int z)
        {
            return $"{x}_{y}_{z}";
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();

            b.Append(">> moving along Z axis\n\n");
            for (int k = 0; k < this.maxZ; k++)
            {
                b.Append($"Z = {k}\n");
                for (int j = 0; j < this.maxY; j++)
                {
                    b.Append("[");
                    for (int i = 0; i < this.maxX; i++)
                    {
                        if (i > 0) b.Append("\t");

                        T element = this[i, j, k];
                        if (element != null) b.Append(element);
                        else b.Append("-\t");
                    }
                    b.Append("\t]\n");
                }
                b.Append("\n");
            }
            return b.ToString();
        }
    }
}