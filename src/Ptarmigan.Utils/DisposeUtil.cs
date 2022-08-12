using System;

namespace Ptarmigan.Utils
{
    public static class DisposeUtil
    {

        public static Disposer Disposer(Action action)
            => new Disposer(action);

        public static Disposer Disposer(Action beforeAction, Action afterAction)
        {
            beforeAction();
            return new Disposer(afterAction);
        }
    }
}