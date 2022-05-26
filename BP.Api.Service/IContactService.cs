using BP.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Api.Service
{
    public interface IContactService
    {
        public ContactDVO GetContactById(int Id);
    }
}
