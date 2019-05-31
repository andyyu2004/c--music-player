using System;
using System.Threading.Tasks;

namespace CMusicPlayer.Internal.Types.Functional
{
    #nullable disable
    /// <summary>
    /// Try&lt;T&gt; is isomorphic to Either&lt;Exception, T&gt; 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Try<T> // : Either<Exception, T> 
    {
        private readonly T val;
        private readonly Exception ex;

        public readonly bool IsError;
        public bool IsSuccess => !IsError;

        public Try(Exception ex)
        {
            this.ex = ex;
            IsError = true;
        }

        public Try(T val)
        {
            this.val = val;
            IsError = false;
        }

//        public T Unwrap()
//        {
//            if (IsError) throw new Exception("Unwrap of failed Try<T>");
//            return val;
//        }

        public TU Match<TU>(Func<Exception, TU> fl, Func<T, TU> fr)
            => IsError ? fl(ex) : fr(val);

        public TU Fold<TU>(Func<Exception, TU> fl, Func<T, TU> fr) => Match(fl, fr);

        public void Match(Action<Exception> fl, Action<T> fr)
        {
            if (IsError) fl(ex);
            else fr(val);
        }

        public Try<TU> Bind<TU>(Func<T, Try<TU>> f)
            => IsError ? new Try<TU>(ex) : f(val);

        public void Bind(Action<T> f)
        {
            if (IsSuccess) f(val);
        }

        public Task<Try<TU>> BindAsync<TU>(Func<T, Task<Try<TU>>> f)
            => IsError ? Task.FromResult(new Try<TU>(ex)) : f(val);

        public static implicit operator Try<T>(T val) => new Try<T>(val);
        public static implicit operator Try<T>(Exception ex) => new Try<T>(ex);

        public static Try<T> Just(T val) => new Try<T>(val);
    }
}
