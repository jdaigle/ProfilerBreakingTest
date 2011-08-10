using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.Test.NHSpecificTest.Bug
{
    public class Bar
    {
        public virtual Guid Id { get; set; }
        public virtual Foo FooReference { get; set; }
        public virtual string BarData { get; set; }
    }
}
