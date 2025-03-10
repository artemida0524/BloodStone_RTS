using Build;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalData
{
    public class GlobalBuildsGridDataHandler
    {
        private static Dictionary<Vector3Int, bool> buildGrid { get; } = new Dictionary<Vector3Int, bool>();

        public GlobalBuildsGridDataHandler()
        {
            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    buildGrid[new Vector3Int(i, 0, j)] = false;
                }
            }
        }

        public static void BuildInGrid(BuildBase build, Vector3Int point)
        {
            for (int height = 0; height < build.Height; height++)
            {
                for (int width = 0; width < build.Width; width++)
                {
                    buildGrid[new Vector3Int(width + point.x, 0, height + point.z)] = true;
                }
            }
        }

        public static bool CanBuildInGrid(BuildBase build, Vector3Int point)
        {
            for (int height = 0; height < build.Height; height++)
            {
                for (int width = 0; width < build.Width; width++)
                {
                    if (buildGrid[new Vector3Int(width + point.x, 0, height + point.z)])
                    {
                        Debug.Log("Cant build");
                        return false;
                    }
                }
            }
            return true;
        }
    }
}