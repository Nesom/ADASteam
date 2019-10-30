using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Models
{
    public class TestModel
    {
        public TestModel(ApplicationContext context)
        {
            TryToAdd = (int id, int count) => context.HasQuantity(id, count);
        }

        public Func<int, int, bool> TryToAdd { get; }
    }
}
