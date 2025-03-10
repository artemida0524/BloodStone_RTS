using System;


namespace Bar
{
    public interface IResourceBar : IDisposable
    {
        string Name { get; }
        int MaxCount { get; }
        int Count { get; }

        Action OnDataChange { get; set; }

        void OnChange(int count);
    }
}