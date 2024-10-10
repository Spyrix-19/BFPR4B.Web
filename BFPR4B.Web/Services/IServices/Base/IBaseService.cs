using AutoMapper.Internal;
using BFPR4B.Web.Models.System;

namespace BFPR4B.Web.Services.IServices.Base
{
	public interface IBaseService
	{
		APIResponse responseModel { get; set; }
		Task<T> SendAsync<T>(APIRequest apiRequest);
	}
}
