using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierholzerAlgorithm
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Returns the index of a given key in the dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary values.</typeparam>
        /// <param name="dictionary">The dictionary to search.</param>
        /// <param name="key">The key to find the index for.</param>
        /// <returns>The index of the key in the dictionary.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the key is not found in the dictionary.</exception>
        public static int IndexOf<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            int index = 0;
            foreach (var k in dictionary.Keys)
            {
                if (EqualityComparer<TKey>.Default.Equals(k, key))
                    return index;
                index++;
            }

            return -1;
        }
    }
}
