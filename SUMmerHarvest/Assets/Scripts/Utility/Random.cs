using System;
using System.Collections.Generic;
using UnityEngine;


    public static class RandomHelper
    {
        private static System.Random rng = new System.Random();

        public static void ShuffleList<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static Vector2 RandomVector2(Vector2 bottomLeft, Vector2 topRight)
        {
            float blx = bottomLeft.x;
            float bly = bottomLeft.y;
            float trx = topRight.x;
            float trY = topRight.y;
            return RandomVector2(blx, bly, trx, trY);
        }
        public static Vector2 RandomVector2(float bottomLeftX, float bottomLeftY, float topRightX, float topRightY)
        {
            float x = (float)rng.NextDouble() * (topRightX - bottomLeftX) + bottomLeftX;
            float y = (float)rng.NextDouble() * (topRightY - bottomLeftY) + bottomLeftY;
            return new Vector2(x, y);

        }
        public static Vector3 RandomVector3(Vector3 bottomLeftFront, Vector3 topRightBack)
        {
            return RandomVector3(bottomLeftFront.x, bottomLeftFront.y, bottomLeftFront.z, topRightBack.x, topRightBack.y, topRightBack.z);
        }
        public static Vector3 RandomVector3(float bottomLeftFrontX, float bottomLeftFrontY,float bottomLeftFrontZ,float topRightBackX,float topRightBackY,float topRightBackZ)
        {
            float x = (float)rng.NextDouble() * (topRightBackX - bottomLeftFrontX) + bottomLeftFrontX;
            float y = (float)rng.NextDouble() * (topRightBackY - bottomLeftFrontY) + bottomLeftFrontY;
            float z = (float)rng.NextDouble() * (topRightBackZ - bottomLeftFrontZ) + bottomLeftFrontZ;
            return new Vector3(x, y, z);
        }

    }

