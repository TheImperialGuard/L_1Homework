using System;

namespace Assets._Project.Develop.Runtime.Utilities.Reactive
{
    public interface IReadOnlyVariable<T> where T : IEquatable<T>
    {
        T Value { get; }

        IDisposable Subscribe(Action<T, T> action);
    }
}