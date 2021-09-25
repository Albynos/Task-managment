using AutoMapper;

namespace Application
{
    public interface IMapFor<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}