/*
 * ClamAV.Managed - Managed bindings for ClamAV
 * Copyright (C) 2011, 2013 Rupert Muchembled
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along
 * with this program; if not, write to the Free Software Foundation, Inc.,
 * 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClamAV.Managed
{
    /// <summary>
    /// Exception type corresponding to a ClamAV error.
    /// </summary>
    [Serializable]
    public class ClamException : Exception
    {
        private int _errorCode;

        /// <summary>
        /// Integer error code.
        /// </summary>
        public int ErrorCode { get { return _errorCode; } }

        /// <summary>
        /// ClamAV error status.
        /// </summary>
        public ClamError ClamError { get { return (ClamError)_errorCode; } }

        /// <summary>
        /// Initializes a new instance of the ClamException class.
        /// </summary>
        public ClamException() { }

        /// <summary>
        /// Initializes a new instance of the ClamException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        public ClamException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the ClamException class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified. </param>
        public ClamException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the ClamException class with a specified error code and message.
        /// </summary>
        /// <param name="errorCode">The ClamAV error code for this exception.</param>
        /// <param name="message">The ClamAV error message for this exception.</param>
        public ClamException(int errorCode, string message)
            : base(message)
        {
            _errorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the ClamException class with serialized data.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected ClamException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
