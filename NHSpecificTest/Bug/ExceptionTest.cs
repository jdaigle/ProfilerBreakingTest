using System;
using NHibernate.Criterion;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.Bug
{
    [TestFixture]
    public class ExceptionTest : BugTestCase
    {
        [Test]
        public void should_not_throw_exception()
        {
            using (ISession session = this.OpenSession())
            {
                session.Load<Foo>(Guid.NewGuid()).ToString();
            }
        }

        [Test]
        public void should_throw_adoexception()
        {
            try
            {
                using (ISession session = this.OpenSession())
                {
                    session.CreateCriteria<Bar>()
                        .Add(Restrictions.Eq("FooReference", session.Load<Foo>(Guid.NewGuid())))
                        .SetProjection(Projections.SqlProjection("asdf", new[] { "asdf" }, new[] { NHibernateUtil.AnsiChar }))
                        .List();
                }
                Assert.Fail("should have thrown an exception");
            }
            catch (Exception e)
            {
                Assert.IsTrue(typeof(NHibernate.Exceptions.GenericADOException).IsAssignableFrom(e.GetType()));
            }
        }
    }
}
