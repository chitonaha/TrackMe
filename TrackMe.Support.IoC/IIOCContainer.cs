using System;

namespace TrackMe.Support.IoC
{
    public interface IIOCContainer
    {
        TService Resolve<TService>();

        object Resolve(Type type);

        void RegisterType(Type type);
    }
}
