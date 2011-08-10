using System;
using NHibernate.Cfg;
using NHibernate.Criterion;

namespace ProfilerBreakingTest
{
    public class Foo
    {
        public virtual Guid Id { get; set; }
        public virtual string FooData { get; set; }
    }

    public class Bar
    {
        public virtual Guid Id { get; set; }
        public virtual Foo FooReference { get; set; }
        public virtual string BarData { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            var config = new Configuration();
            config.AddAssembly("ProfilerBreakingTest");
            var sessionFactory = config.Configure().BuildSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                session.CreateCriteria<Bar>().Add(Restrictions.Eq("FooReference", session.Load<Foo>(Guid.NewGuid()))).List();
            }

            Console.WriteLine("Exiting Without Error");
        }
    }
}
