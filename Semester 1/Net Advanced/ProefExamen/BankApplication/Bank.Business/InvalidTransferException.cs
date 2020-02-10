using System;

namespace Bank.Business
{
    public class InvalidTransferException : ApplicationException
    {
        public InvalidTransferException(string message) : base(message)
        {

        }
    }
}