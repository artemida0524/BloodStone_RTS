using BloodStone.Gameplay.Build;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GlobalData
{
    public class GlobalBuildsDataHandler : IBuildingProvider, IBuildGridData
    {
        private List<BuildBase> _allBuilds = new List<BuildBase>();
        public IEnumerable<BuildBase> AllBuilds => _allBuilds;
        public BuildGridData GlobalBuildsGridData { get; private set; } = new BuildGridData();


        public void Init()
        {
            BuildUtility.OnBuildEnable += OnBuildEnableHandler;
            BuildUtility.OnBuildDisableOrDestroy += OnBuildDisableOrDestroyHandler;
        }

        private void OnBuildDisableOrDestroyHandler(BuildBase build)
        {
            RemoveBuild(build);
        }

        private void OnBuildEnableHandler(BuildBase build)
        {
            AddBuild(build);
        }

        public IEnumerable<T> GetBuilds<T>()
        {
            return AllBuilds.OfType<T>();
        }

        public IEnumerable<T> GetBuilds<T>(Func<T, bool> predicat)
        {
            return AllBuilds.OfType<T>().Where(predicat);
        }

        public void AddBuild(BuildBase build)
        {
            if (AllBuilds.Contains(build))
            {
                Debug.LogWarning("Already exists" + " " + build.name);
                return;
            }
            _allBuilds.Add(build);
            BuildInGrid(build, Vector3Int.FloorToInt(build.transform.position));
        }

        public void RemoveBuild(BuildBase build)
        {
            _allBuilds.Remove(build);

            RemoveBuildInGrid(build, Vector3Int.FloorToInt(build.transform.position));
        }

        private void BuildInGrid(BuildBase build, Vector3Int point)
        {
            GlobalBuildsGridData.BuildInGrid(build, point);
        }

        private void RemoveBuildInGrid(BuildBase build, Vector3Int point)
        {
            GlobalBuildsGridData.RemoveInGrid(build, point);
        }

    }
}

