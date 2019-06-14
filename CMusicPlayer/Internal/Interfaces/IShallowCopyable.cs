using System.CodeDom;

namespace CMusicPlayer.Internal.Interfaces
{
    internal interface IShallowCopyable
    {
        IShallowCopyable ShallowCopy();
    }
}
