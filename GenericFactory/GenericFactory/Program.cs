using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Rhino.Mocks;

namespace GenericFactory
{
    class Program
    {

        private static IMyInterface mock;

        static void Main(string[] args)
        {
            var fac=new MyFactory();
            IMyInterface myObject1=fac.Create<MyClass1>();
            myObject1.Execute();

            IMyInterface myObject2 = fac.Create<MyClass2>();
            myObject2.Execute();
            
            IMyInterface myObject3 = fac.Create<MyMock>();
            myObject3.Execute();

            Debug.WriteLine("End");
            // fac.Create<MyWrongClass>(); // das ist bei programmieren verboten!!!!
           

            // einzelnd nutzen -> container
            var builder = new ContainerBuilder();
            builder.RegisterType<MyClass1>().As<IMyInterface>();
            var container = builder.Build();
            var xxx=container.Resolve<IMyInterface>(new NamedParameter("Dependency1",42), new NamedParameter("Dependency2", "42"));
            xxx.Execute();

            Console.ReadKey();
        }
    }

    public abstract class MyBase : IMyInterface
    {
        protected int Dependency1;
        protected string Dependency2;
        public MyBase(int dependency1, string dependency2)
        {
            this.Dependency1 = dependency1;
            this.Dependency2 = dependency2;
        }

        public virtual void Execute()
        {
            Console.WriteLine("abstact");
        }
    }

    public interface IMyInterface
    {
        void Execute();
    }

    public class MyFactory
    {
        private readonly int helpInt;
        private readonly string helpString;
        public MyFactory()
        {
            // hier dann constructor injection zB
            helpInt = 1;
            helpString = "hallo lösung";
        }

        public IMyInterface Create<T>() where T : MyBase, IMyInterface 
        {
            // var wrong = (T)Activator.CreateInstance<T>(); das gibt ne exception
            return  (T)Activator.CreateInstance(typeof(T), helpInt, helpString);
        }
    }

    public class MyClass1 : MyBase, IMyInterface
    {
        // der wird erzeungen
        public MyClass1(int Dependency1, string Dependency2) : base(Dependency1, Dependency2)
        {
        }

        public void Execute()
        {
            Console.WriteLine("Class1:  " + base.Dependency1);
        }
    }

    public class MyClass2 : MyBase
    {

        public MyClass2(int dependency1, string dependency2) : base(dependency1, dependency2)
        {
        }

        public override void Execute()
        {
            Console.WriteLine("Class2:  " + base.Dependency1);
        }
    }

    public class MyWrongClass
    {
    }

    public class MyMock : MyBase, IMyInterface
    {
        public MyMock(int dependency1, string dependency2) : base(dependency1, dependency2)
        {
        }
        
        public void Execute()
        {
            Console.WriteLine("MyMock");
        }
    }

}
