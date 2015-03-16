using System;
using Funq;
using ServiceStack;

namespace ToDoBackend
{
    public class AppHost : AppHostBase
    {
        public AppHost() 
            : base("ToDoBackend", typeof (ToDoService).Assembly)
        { }

        public override void Configure(Container container)
        {
            var corsPlugin = new CorsFeature(
                allowedMethods: "GET, POST, PUT, DELETE, PATCH, OPTIONS");
            Plugins.Add(corsPlugin);
            container.RegisterAs<StaticHashSetToDoRepo,IToDoRepo>();
            var binding = Environment.GetEnvironmentVariable("todobackend_binding");
            if (!binding.IsNullOrEmpty())
            {
                SetConfig(new HostConfig { WebHostUrl = binding });
            }
        }
    }
}