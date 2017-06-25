using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI102
{
    public interface IRepository
    {

    }

    public class NHibernateRepository : IRepository
    {

    }


    public interface IEmailSender
    {
        int Port { get; }
    }

    public class SmtpEmailSender : IEmailSender
    {
        private int _port;
        public SmtpEmailSender(int port)
        {
            _port = port;
        }

        public int Port
        {
            get { return _port; }
        }
    }

    public class LoginController
    {
        private readonly IEmailSender _emailSender;
        public LoginController(IRepository repository, IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public IEmailSender EmailSender
        {
            get { return _emailSender; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var container = new DemoContainer();

            //registering dependecies
            container.Register<IRepository>(delegate
            {
                return new NHibernateRepository();
            });

            container.Configuration["email.sender.port"] = 1234;

            container.Register<IEmailSender>(delegate
            {
                return new SmtpEmailSender(container.GetConfiguration<int>("email.sender.port"));
            });

            container.Register<LoginController>(delegate
            {
                return new LoginController(
                    container.Create<IRepository>(),
                    container.Create<IEmailSender>());
            });

            //using the container
            Console.WriteLine(
                container.Create<LoginController>().EmailSender.Port
                );

            Console.ReadKey();
        }

    }
}