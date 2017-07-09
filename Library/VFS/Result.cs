// ------------------------------------------------------------------------
// Result.cs written by Code A Software (http://www.code-a-software.net)
// Created on:      30.06.2017
// Last update on:  01.07.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFS
{
    /// <summary>
    /// Defines a more specified result of a method in VFS.cs
    /// </summary>
    /// <typeparam name="T">The result type</typeparam>
    public class Result<T> 
    {
        /// <summary>
        /// The value of the result
        /// </summary>
        public readonly T Value = default(T);

        /// <summary>
        /// Determines if method succeeded
        /// </summary>
        public readonly bool Success = false;

        /// <summary>
        /// The exception if method wasn't sucessfull.
        /// </summary>
        public readonly Exception FailInfo;

        /// <summary>
        /// Returns true if this result has value
        /// </summary>
        public bool HasValue => (Value != null);

        /// <summary>
        /// Creates a new result
        /// </summary>
        /// <param name="value">The final result value</param>
        /// <param name="success">Sucessfull or not</param>
        /// <param name="failInfo">The exception if you get one</param>
        public Result(T value, bool success, Exception failInfo)
        {
            this.Value = value;
            this.Success = success;
            this.FailInfo = failInfo;
        }

        /// <summary>
        /// Creates a new result which is successfull
        /// </summary>
        /// <param name="value">The final result value</param>
        public Result(T value)
        {
            if (value is bool)
            {
                bool state = Convert.ToBoolean(value);
                this.Success = state;
                this.Value = value;
                this.FailInfo = null;
            }
            else
            {
                this.Success = true;
                this.Value = value;
                this.FailInfo = null;
            }
        }

        /// <summary>
        /// Creates a new result which isn't successfull
        /// </summary>
        /// <param name="failInfo">The exception you get</param>
        public Result(Exception failInfo) : this(default(T), false, failInfo)
        { }

    }
}
