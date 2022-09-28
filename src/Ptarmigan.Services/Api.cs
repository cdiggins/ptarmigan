﻿using System.Collections.Generic;
using Domo;

namespace Ptarmigan.Services
{
    public class ServiceRegisteredEvent<T> : IEvent
        where T : IService
    {
        public T Service { get; }

        public ServiceRegisteredEvent(T service)
            => Service = service;
    }

    public class RepositoryRegisteredEvent<T> : IEvent
        where T : IRepository
    {
        public T Repository { get; }

        public RepositoryRegisteredEvent(T repository)
            => Repository = repository;
    }


    public sealed class Api : IApi
    {
        private List<IService> _services = new List<IService>();
        private List<IRepository> _repositories = new List<IRepository>();

        public IEnumerable<IService> GetServices() => _services;
        public IEnumerable<IRepository> GetRepositories() => _repositories;
        
        public IEventBus EventBus { get; } = new EventBus();

        public void AddService<T>(T service) where T: IService
        {
            _services.Add(service);
            EventBus.Publish(new ServiceRegisteredEvent<T>(service));
        }

        public void AddRepository<T>(T repository) where T: IRepository
        {
            _repositories.Add(repository);
            EventBus.AddRepositoryAsPublisher(repository);
            EventBus.Publish(new RepositoryRegisteredEvent<T>(repository));
        }
    }
}
