using System;

namespace DI101
{
    public interface IService
    {
        void Serve();
    }

    public class Service : IService
    {
        public void Serve()
        {
            Console.WriteLine("Service Invoked");
        }
    }

    public class Client
    {
        private readonly IService _service;

        public Client(IService service)
        {
            _service = service;
        }

        public void Start()
        {
            Console.WriteLine("Service Started");
            this._service.Serve();
            //To Do: Some Stuff
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var c = new Client(new Service());
            c.Start();

            Console.ReadKey();
        }
    }
}
