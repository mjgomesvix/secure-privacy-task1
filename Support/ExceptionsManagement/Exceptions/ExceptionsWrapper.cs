using System;
using System.Collections.Generic;

namespace Support.ExceptionsManagement.Exceptions
{
    public class ExceptionsWrapper : Exception
    {
        #region Members

        private readonly List<string> _messages;

        #endregion

        #region Constructors

        public ExceptionsWrapper() : base("exceptions wrapper")
        {
            _messages = new List<string>();
        }

        #endregion

        #region Attributes

        public IEnumerable<string> Messages
        {
            get { return _messages; }
        }

        #endregion

        #region Public Methods

        public void AddExceptionMessage(string message)
        {
            _messages.Add(message);
        }

        public void AddExceptionsMessages(IEnumerable<string> messages)
        {
            _messages.AddRange(messages);
        }

        #endregion
    }
}
