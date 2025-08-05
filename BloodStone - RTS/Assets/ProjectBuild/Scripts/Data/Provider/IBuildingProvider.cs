using Game.Gameplay.Build;
using System;
using System.Collections.Generic;

namespace GlobalData
{
    public interface IBuildingProvider
    {
        IEnumerable<BuildBase> AllBuilds { get; }


        void Init();

        void AddBuild(BuildBase build);
        IEnumerable<T> GetBuilds<T>();
        IEnumerable<T> GetBuilds<T>(Func<T, bool> predicat);
        void RemoveBuild(BuildBase build);
    }
}