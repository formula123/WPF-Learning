using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMDemo.ViewModel;
using Xunit;

namespace MVVMDemo_Test
{
    public class Class1
    {
        [Fact]
        void test()
        {
            StudentViewModel model = new StudentViewModel();
            model.LoadStudents();
            Assert.True(model.Students.Count == 4);
        }
    }
}
