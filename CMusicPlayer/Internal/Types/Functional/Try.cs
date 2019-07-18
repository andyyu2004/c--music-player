using System;
using System.Threading.Tasks;

namespace CMusicPlayer.Internal.Types.Functional
{
    /// <summary>
    ///     Try&lt;T&gt; is isomorphic to Either&lt;Exception, T&gt;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    ///
    ///
    #nullable disable
    public class Try<T> // : Either<Exception, T> 
    {
        private readonly Exception ex;

        public readonly bool IsError;
        private readonly T val;

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
        #nullable enable

        public bool IsSuccess => !IsError;

//        public T Unwrap()
//        {
//            if (IsError) throw new Exception("Unwrap of failed Try<T>");
//            return val;
//        }

        public TU Match<TU>(Func<Exception, TU> fl, Func<T, TU> fr)
        {
            return IsError ? fl(ex) : fr(val);
        }

        public TU Fold<TU>(Func<Exception, TU> fl, Func<T, TU> fr)
        {
            return Match(fl, fr);
        }

        public void Match(Action<Exception> fl, Action<T> fr)
        {
            if (IsError) fl(ex);
            else fr(val);
        }

        public Try<TU> Bind<TU>(Func<T, Try<TU>> f)
        {
            return IsError ? new Try<TU>(ex) : f(val);
        }

        public void Bind(Action<T> f)
        {
            if (IsSuccess) f(val);
        }

        public Task<Try<TU>> BindAsync<TU>(Func<T, Task<Try<TU>>> f)
        {
            return IsError ? Task.FromResult(new Try<TU>(ex)) : f(val);
        }

        public static implicit operator Try<T>(T val)
        {
            return new Try<T>(val);
        }

        public static implicit operator Try<T>(Exception ex)
        {
            return new Try<T>(ex);
        }

        public static Try<T> Just(T val)
        {
            return new Try<T>(val);
        }
    }
}