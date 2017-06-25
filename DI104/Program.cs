using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI104
{
    public interface IDal
    {
        void Save(IPerson person);
    }

    public class Dal : IDal
    {
        public void Save(IPerson person)
        {
            Console.WriteLine("Save Person");
        }
    }

    public interface IPerson
    {
        void Save();   
    }

    public class Person : IPerson
    {
        private readonly IDal _dal;

        public Person(IDal dal)
        {
            _dal = dal;
        }

        public void Save()
        {
            _dal.Save(this);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .AsImplementedInterfaces();

            var container = builder.Build();

            var p = container.Resolve<IPerson>();

            p.Save();

            Console.ReadKey();

        }
    }
}
