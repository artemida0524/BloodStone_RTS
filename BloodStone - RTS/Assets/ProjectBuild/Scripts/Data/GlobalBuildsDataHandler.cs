using Build;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GlobalData
{
    public class GlobalBuildsDataHandler
    {
        public static List<BuildBase> AllBuilds { get; } = new List<BuildBase>();

        public static List<T> GetBuilds<T>()
        {
            //return AllBuilds.Where(t => t is T).Cast<T>().ToList();
            return AllBuilds.OfType<T>().ToList();
        }

        public static void AddBuild(BuildBase build)
        {

            if (AllBuilds.Contains(build))
            {
                Debug.LogWarning("Already exists");
                return;
            }
            AllBuilds.Add(build);
        }

        public static void RemoveBuild(BuildBase build)
        {
            AllBuilds.Remove(build);
        }


    }
}

