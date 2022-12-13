﻿using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.Models;
using InventoryManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InventoryManagement.API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;
        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }



        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null)
            {
                await next.Invoke();
                return;
            }
            var id = (Int64)idValue;

            //id kontrolü
            var anyEntity = await _service.AnyAsync(x => x.Id == id);
            if (anyEntity)
            {
                await next.Invoke();
                return;
            }
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContent>.Fail(404, $"{typeof(T).Name} ({id}) bulunamadı"));

            throw new NotImplementedException();
        }

    }
}
