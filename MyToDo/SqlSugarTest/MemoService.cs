using MyToDo.Api.Context;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarTest
{
    internal class MemoService : Repository<Memo>
    {
        public List<Memo> GetAll()
        {
            return base.GetList();
        }
    }
}