using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Assets._Project.Develop.Runtime.Gameplay.SequenceGenerator
{
    public class SequenceGenerator<T>
    {
        public List<T> GenerateOf(List<T> valuesList, int length)
        {
            if (length < 1)
                throw new ArgumentOutOfRangeException(nameof(length), "Длина последовательности должна быть больше 0");

            List<T> sequence = new List<T>();

            for (int i = 0; i < length; i++)
            {
                T value = valuesList[Random.Range(0, valuesList.Count)];
                sequence.Add(value);
            }

            return sequence;
        }
    }
}
