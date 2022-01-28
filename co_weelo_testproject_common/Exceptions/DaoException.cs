using System;

namespace co_weelo_testproject_common.Exceptions
{
    public class DaoException : Exception
    {
        private readonly string message;
        private readonly Exception exception;

        public DaoException() : base() { }

        public DaoException(string _message) : base(_message)
        {
            message = _message;
        }

        public DaoException(string _message, Exception _exception) : base(_message, _exception)
        {
            message = _message;
            exception = _exception;
        }


    }
}
