using Mango.Web.Models;
using Mango.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Web.Services.IServices
{
    public interface IBaseService : IDisposable
    {
        public ResponseDto responseModel { get; set; }
        public Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
