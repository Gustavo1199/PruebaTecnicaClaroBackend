using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PruebasTecnicaClaroDomBackend.Helper
{
    public class ResultResponses
    {
        
            private HttpStatusCode _status = HttpStatusCode.OK;
            public HttpStatusCode status
            {
                get
                {
                    return _status;
                }
                set
                {
                    _status = value;
                }
            }


            private IEnumerable<HttpStatusCode> Success = new List<HttpStatusCode>
        {
            HttpStatusCode.OK,
            HttpStatusCode.Created,
            HttpStatusCode.Accepted,
            HttpStatusCode.NonAuthoritativeInformation,
            HttpStatusCode.NoContent,
            HttpStatusCode.ResetContent,
            HttpStatusCode.PartialContent,
            HttpStatusCode.Continue,
        };

            public bool isSuccess
            {
                get
                {
                    return (Success.Contains(status));
                }
            }

            private Object _result;
            public Object result
            {
                get
                {
                    if (isSuccess)
                        return _result;
                    else
                        return null;
                }
                set { _result = value; }
            }

            private Object _error;
            public Object error
            {
                get
                {
                    if (!isSuccess || status == HttpStatusCode.Continue)
                        return _error;
                    else
                        return null;
                }
                set { _error = value; }
            }

            public void GetSuccessOperation(object result)
            {
                if (result == null)
                {
                    this.status = HttpStatusCode.NoContent;
                }
                else
                {
                    this.status = HttpStatusCode.OK;
                    this.result = result;
                }
            }

            public void GetErrorOperation(Exception message, HttpStatusCode status = HttpStatusCode.InternalServerError, bool IsWarning = false)
            {
                message = message.GetBaseException();
                var handled_error = message.GetBaseException().Message;
                if (message.GetType() == typeof(DbEntityValidationException))
                {

                    var errorMessages = ((DbEntityValidationException)message).EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                    var fullErrorMessage = string.Join("; ", errorMessages);
                    handled_error = string.Concat("Se encontraron los siguientes errores en la validación del modelo : ", fullErrorMessage);
                }

                //if (handled_error.Contains("ORA"))
                //{
                //    handled_error = OraResponseExceptions.GetOraCustomMessage(message,
                //     ConfigurationManager.ConnectionStrings["GAECONTEXT"].ConnectionString, "ORA_ERRORS", "oracle");
                //}

                this.status = OperationError(message, status, IsWarning);
                this.error = handled_error;
            }

            public static HttpStatusCode OperationError(Exception ex, HttpStatusCode status, bool IsWarning = false)
            {
                if (ex.Message.Contains("Timeout"))
                {
                    status = System.Net.HttpStatusCode.RequestTimeout;
                }
                else if (ex.Message.Contains("No HTTP resource was found"))
                {
                    status = HttpStatusCode.NotFound;
                }
                else if (ex.Message.Contains("Msg") && IsWarning == false)
                {
                    status = HttpStatusCode.InternalServerError;
                }
                else if (ex.Message.Contains("Msg") && IsWarning)
                {
                    status = HttpStatusCode.Continue;
                }
                return status;
            }

           
        
    }
}
