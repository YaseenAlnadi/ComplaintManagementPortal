using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer;
namespace BusinessLayer
{
    public class BaseClass : IDisposable
    {
        private ComplaintManagementPortalEntities _innerDataModelContainer = null;
        public ComplaintManagementPortalEntities InnerDataContext
        {
            get
            {
                if (_innerDataModelContainer == null)
                    _innerDataModelContainer = new ComplaintManagementPortalEntities();

                return _innerDataModelContainer;
            }
        }

        private bool disposed;

        public void Dispose()
        {
            //Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~BaseClass()
        {
            //Dispose(false);
        }

        public class DALException : Exception
        {
            public DALException() { }
            public DALException(string message) : base(message) { }
            public DALException(string message, Exception inner) : base(message, inner) { }
        }
        public class Errorhandling
        {
            public void getErrorhandling()
            {

            }
        }
    }
}
