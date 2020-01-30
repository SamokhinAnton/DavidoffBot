using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DavidoffBot.Interface
{
    public interface IBaseRepository
    {
        Task<string> Get(string keyword);
    }
}
