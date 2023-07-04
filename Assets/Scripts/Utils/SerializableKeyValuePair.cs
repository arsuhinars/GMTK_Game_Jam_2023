using System;

namespace GMTK_2023.Utils
{
    [Serializable]
    public struct SerializableKeyValuePair<K, V>
    {
        public K key;
        public V value;
    }
}
