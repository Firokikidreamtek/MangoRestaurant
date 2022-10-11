using Mango.Web.Models;
using Mango.Web.Models.Dto;
using Mango.Web.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Web.Services
{
    public class BaseService : IBaseService
    {
        ResponseDto IBaseService.responseModel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        Task<T> IBaseService.SendAsync<T>(ApiRequest apiRequest)
        {
            throw new NotImplementedException();
        }
    }
}
