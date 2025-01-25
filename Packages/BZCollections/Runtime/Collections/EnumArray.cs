using System;
using System.Collections.Generic;
using UnityEngine;

namespace BZ.Core.Collections
{
    [Serializable]
    public class EnumArray<E> where E : Enum
    {
        public static Type EnumType => typeof(E);
        public static System.Array ArrayEnum => System.Enum.GetValues(EnumType);
    }

    [Serializable]
    public class EnumArray<T, E> : EnumArray<E> where E : Enum
    {
        [SerializeField]
        protected T[] _enumArray;

        public int Count => _enumArray.Length;

        public EnumArray()
        {
            _enumArray = new T[ArrayEnum.Length];
        }

        public T this[E characterIndex]
        {
            get => Get(characterIndex);
            set => Set(characterIndex, value);
        }

        public T this[int index]
        {
            get => Get(index);
            set => Set(index, value);
        }

        public T Get(E enumIndex)
        {
            object index = Convert.ChangeType(enumIndex, enumIndex.GetTypeCode());
            return _enumArray[(int)index];
        }

        public T Get(int index)
        {
            return _enumArray[index];
        }

        public void Set(E enumIndex, T value)
        {
            object index = Convert.ChangeType(enumIndex, enumIndex.GetTypeCode());
            _enumArray[(int)index] = value;
        }

        public void Set(int index, T value)
        {
            _enumArray[index] = value;
        }

        /// <summary>
        /// Determines whether an element is in the EnumArray<T, E>
        /// </summary>
        /// <param name="value">
        /// The object to locate in the EnumArray<T, E>. The value can be null for reference types.
        /// </param>
        /// <returns>
        /// true if value is found in the EnumArray<T, E> otherwise, false.
        /// </returns>
        public bool Contains(T value)
        {
            int enumCount = Count;
            for (int i = 0; i < enumCount; i++)
            {
                if (_enumArray[i].Equals(value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Searches for the specified object and returns the first occurrence index in the EnumArray<T, E>.
        /// </summary>
        /// <param name="value">
        /// The object to locate in the EnumArray<T, E>. The value can be null for reference types.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of value in the EnumArray<T, E>, if found; otherwise, -1.
        /// </returns>
        public int IndexOf(T value)
        {
            int enumCount = Count;
            for (int i = 0; i < enumCount; i++)
            {
                if (_enumArray[i].Equals(value))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Searches for the specified object and returns the first occurrence E(Enum) value in the EnumArray<T, E>.
        /// </summary>
        /// <param name="value">
        /// The object to locate in the EnumArray<T, E>. The value can be null for reference types.
        /// </param>
        /// <returns>
        /// The E(Enum) value of the first occurrence of value in the EnumArray<T, E>, if found.
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// The value is not found in the EnumArray<T, E>.
        /// </exception>
        public E Find(T value)
        {
            int enumCount = Count;
            for (int i = 0; i < enumCount; i++)
            {
                if (_enumArray[i].Equals(value))
                {
                    return (E)Enum.ToObject(EnumType, i);
                }
            }

            throw new KeyNotFoundException($"The value {value} has no entry in the EnumArray");
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_enumArray).GetEnumerator();
        }
    }
}