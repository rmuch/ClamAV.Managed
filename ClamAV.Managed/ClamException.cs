/*
 * ClamAV.Managed - Managed bindings for ClamAV
 * Copyright (C) 2011, 2013-2014, 2016 Rupert Muchembled
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

namespace ClamAV.Managed
{
    /// <summary>
    /// Exception type corresponding to a ClamAV error.
    /// </summary>
    [Serializable]
    public class ClamException : Exception
    {
        private readonly int _errorCode;
        private readonly string _message;

        /// <summary>
        /// ClamAV integer error code.
        /// </summary>
        protected int ErrorCode { get { return _errorCode; } }

        /// <summary>
        /// ClamAV error status.
        /// </summary>
        public ClamError ClamError { get { return (ClamError)_errorCode; } }

        /// <summary>
        /// ClamAV error message.
        /// </summary>
        public override string Message { get { return _message; } }

        /// <summary>
        /// Initializes a new instance of the ClamException class with a specified error code, using libclamav to
        /// obtain a string representation of the error message.
        /// </summary>
        /// <param name="errorCode">The ClamAV error code for this exception.</param>
        internal ClamException(int errorCode)
            : this(errorCode, ClamEngine.ErrorString(errorCode)) { }

        /// <summary>
        /// Initializes a new instance of the ClamException class with a specified error, using libclamav to obtain
        /// a string representation of the error message.
        /// </summary>
        /// <param name="error">The ClamAV error for this exception.</param>
        internal ClamException(ClamError error)
            : this((int)error) { }

        /// <summary>
        /// Initializes a new instance of the ClamException class with a specified error code and message.
        /// </summary>
        /// <param name="errorCode">The ClamAV error code for this exception.</param>
        /// <param name="message">The ClamAV error message for this exception.</param>
        internal ClamException(int errorCode, string message)
        {
            if (errorCode == 0)
                throw new ArgumentException("error code indicates success", nameof(errorCode));

            if (errorCode < 1 || errorCode >= (int)ClamError.LastError)
                throw new ArgumentException("error code is an unexpected value", nameof(errorCode));

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            _errorCode = errorCode;
            _message = message;
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
