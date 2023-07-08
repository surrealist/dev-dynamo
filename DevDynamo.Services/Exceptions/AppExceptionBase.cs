using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DevDynamo.Services.Exceptions
{
    public abstract class AppExceptionBase : Exception
    {
        protected AppExceptionBase() { }
        public AppExceptionBase(int httpStatus)
        {
            HttpStatus = httpStatus;
        }
        public AppExceptionBase(string message): base(message) { }
        public int HttpStatus { get; set; }
    }

    public class NotFoundException : AppExceptionBase {

        public NotFoundException(string objectName, 
            object? keyThatNotFound = null,
            string? extraMessage = null)
        {
            ObjectName = objectName;
            KeyThatNotFound = keyThatNotFound;
            ExtraMessage = extraMessage;

            HttpStatus = 404;
        }

        public string ObjectName { get; }
        public object? KeyThatNotFound { get; }
        public string? ExtraMessage { get; }

        public override string Message
        {
            get
            {
                var s = $"{ObjectName} was not found.";

                if (KeyThatNotFound != null)
                    s += $" [{KeyThatNotFound}]";
                if (ExtraMessage != null)
                    s += $" {ExtraMessage}.";

                return s;
            }
        }
    }
}
