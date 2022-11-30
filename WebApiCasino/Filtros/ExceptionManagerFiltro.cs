using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCasino.Entidades;

namespace WebApiCasino.Filtros
{
    public class ExceptionManagerFiltro : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostingEviroment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public ExceptionManagerFiltro(IWebHostEnvironment hostingEviroment,
            IModelMetadataProvider modelMetadataProvider)
        {
            this._hostingEviroment = hostingEviroment;
            this._modelMetadataProvider = modelMetadataProvider;
        }
        public void OnException(ExceptionContext context)
        {
            //if (context.Exception is Participante)
            //{
            //    context.Result = new JsonResult(" Error " + _hostingEviroment.ApplicationName + " la excepcion de tipo:" + context.Exception.GetType());
            //}
        }
    }
}
