using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CMusicPlayer.Internal.Types.DataStructures
{
    /**
     * Returns null if key doesn't exist instead of throwing exception. Violates LSP but probably worth the syntax benefits just for this project
     */
    internal class NDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TValue : class
    {
        public new TValue? this[TKey key]
        {
            get => ContainsKey(key) ? base[key] : null;
            set
            {
                if (value != null) base[key] = value;
            }
        }
    }

}