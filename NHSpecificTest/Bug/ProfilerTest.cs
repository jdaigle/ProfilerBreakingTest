using System;
using NUnit.Framework;
using NHibernate.Criterion;

namespace NHibernate.Test.NHSpecificTest.Bug
{
    [TestFixture]
    public class ProfilerTest : BugTestCase
    {
        protected override void OnSetUp()
        {
            base.OnSetUp();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        }

        protected override void OnTearDown()
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Stop();
            base.OnTearDown();
        }

        [Test]
        public void should_not_throw_exception_with_profiler()
        {
            using (ISession session = this.OpenSession())
            {
                session.CreateCriteria<Bar>().Add(Restrictions.Eq("FooReference", session.Load<Foo>(Guid.NewGuid()))).List();
            }
        }

        [Test]
        public void should_not_throw_exception_without_profiler()
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Stop();
            using (ISession session = this.OpenSession())
            {
                session.CreateCriteria<Bar>().Add(Restrictions.Eq("FooReference", session.Load<Foo>(Guid.NewGuid()))).List();
            }
        }
    }
}
