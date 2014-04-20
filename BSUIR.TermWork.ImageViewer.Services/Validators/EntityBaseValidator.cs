using System.Collections.Generic;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    internal abstract class EntityBaseValidator
    {
        private static readonly Dictionary<string, object> operationBuffer;

        static EntityBaseValidator()
        {
            operationBuffer = new Dictionary<string, object>();
        }

        protected Dictionary<string, object> OperationBuffer
        {
            get { return operationBuffer; }
        }
    }
}
