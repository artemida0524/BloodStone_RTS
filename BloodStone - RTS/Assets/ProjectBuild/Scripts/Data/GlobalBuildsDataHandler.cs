using Build;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GlobalData
{
    public class GlobalBuildsDataHandler
    {
        public  List<BuildBase> AllBuilds { get; } = new List<BuildBase>();

        public  BuildGridData GlobalBuildsGridData { get; private set; } = new BuildGridData();


        public GlobalBuildsDataHandler()
        {
            BuildUtility.OnBuildEnable += BuildUtility_OnBuildEnable;
            BuildUtility.OnBuildDisableOrDestroy += BuildUtility_OnBuildDisableOrDestroy;
        }

        private void BuildUtility_OnBuildDisableOrDestroy(BuildBase @base)
        {
            RemoveBuild(@base);
        }

        private void BuildUtility_OnBuildEnable(BuildBase obj)
        {
            AddBuild(obj);
        }

        public  List<T> GetBuilds<T>()
        {
            return AllBuilds.OfType<T>().ToList();
        }

        public  List<T> GetBuilds<T>(Func<T, bool> predicat)
        {
            return AllBuilds.OfType<T>().Where(predicat).ToList();
        }

        public  void AddBuild(BuildBase build)
        {
            if (AllBuilds.Contains(build))
            {
                Debug.LogWarning("Already exists" + " " + build.name);
                return;
            }
            AllBuilds.Add(build);
            BuildInGrid(build, Vector3Int.FloorToInt(build.transform.position));
        }

        public  void RemoveBuild(BuildBase build)
        {
            AllBuilds.Remove(build);

            RemoveBuildInGrid(build, Vector3Int.FloorToInt(build.transform.position));

        }

        public  void BuildInGrid(BuildBase build, Vector3Int point)
        {
            GlobalBuildsGridData.BuildInGrid(build, point);
        }

        public  void RemoveBuildInGrid(BuildBase build, Vector3Int point)
        {
            GlobalBuildsGridData.RemoveInGrid(build, point);
        }

    }
}

