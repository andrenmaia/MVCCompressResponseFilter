using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO.Compression;

namespace MVCCompressResponseFilter.ActionFilters
{
    /// <summary>
    /// Filtro para compressão do Response.
    /// </summary>
    public class CompressAttribute : ActionFilterAttribute
    {
        /// <summary>
        ///  Chamado pelo ASP.NET MVC framework antes da executação do método de ação terminar.
        /// </summary>
        /// <param name="filterContext">
        /// O filterContext
        /// </param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Obtém o request do cliente e o Accept-Encoding, para verifica se 
            // o cliente aceita compressão.
            HttpRequestBase request = filterContext.HttpContext.Request;
            string acceptEncoding = request.Headers["Accept-Encoding"];

            /// Caso o cliente aceite compressão:
            // - verifica qual tipo de compressão;
            // - comprime o response.
            if (!String.IsNullOrEmpty(acceptEncoding))
            {

                acceptEncoding = acceptEncoding.ToUpperInvariant();
                HttpResponseBase response = filterContext.HttpContext.Response;

                // Comprime o response utilizando os algoritmos de compressão
                // comuns do .NET (caso o cliente aceite esses tipos de compressão).
                if (acceptEncoding.Contains("GZIP"))
                {
                    response.AppendHeader("Content-encoding", "gzip");
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                }
                else if (acceptEncoding.Contains("DEFLATE"))
                {
                    response.AppendHeader("Content-encoding", "deflate");
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                }
            }
        }

    }
}