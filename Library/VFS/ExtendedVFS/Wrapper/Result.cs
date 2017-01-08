// ------------------------------------------------------------------------
// Result.cs written by Code A Software (http://www.code-a-software.net)
// SP: VHP-0001 (OpenSource-Software)
// Created on:      28.12.2016
// Last update on:  08.01.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VFS.ExtendedVFS.Wrapper
{
    /// <summary>
    /// Describes a result which contains information about the running of a method in ModifiedVFS
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Determines whether the methods fails or not
        /// </summary>
        public readonly bool Success;
        
        /// <summary>
        /// The return-value of the method
        /// </summary>
        public readonly object Value;

        /// <summary>
        /// The type of return-value of the method
        /// </summary>
        public readonly Type Typ;

        /// <summary>
        /// An exception, if there is one
        /// </summary>
        public readonly Exception CurrentException;

        /// <summary>
        /// Code to identify the method
        /// </summary>
        public Methods SpecialCode = 0x00;

        /// <summary>
        /// Instantiates a new result
        /// </summary>
        /// <param name="Success">Determines whether the methods fails or not</param>
        /// <param name="Value">The return-value of the method</param>
        /// <param name="Typ">The type of return-value of the method</param>
        /// <param name="CurrentException"> An exception, if there is one</param>
        /// <param name="SpecialCode">Code to identify the method</param>
        public Result(bool Success, object Value, Type Typ, Exception CurrentException, Methods SpecialCode)
        {
            this.Success = Success;
            this.Value = Value;
            this.Typ = Typ;
            this.CurrentException = CurrentException;
            this.SpecialCode = SpecialCode;
        }

        /// <summary>
        /// Instantiate a new result
        /// </summary>
        /// <param name="Success"></param>
        /// <param name="Value"></param>
        /// <param name="Typ"></param>
        /// <param name="SpecialCode"></param>
        public Result(bool Success, object Value, Type Typ, Methods SpecialCode = Methods.DEFAULT) : this(Success, Value, Typ, null, SpecialCode)
        { }

        /// <summary>
        /// Instantiate a new result
        /// </summary>
        /// <param name="Success"></param>
        /// <param name="Value"></param>
        /// <param name="CurrentException"></param>
        /// <param name="SpecialCode"></param>
        public Result(bool Success, string Value, Exception CurrentException, Methods SpecialCode = Methods.DEFAULT) : this(Success, Value, typeof(string), CurrentException, SpecialCode)
        { }

        /// <summary>
        /// Instantiate a new result
        /// </summary>
        /// <param name="Success"></param>
        /// <param name="Value"></param>
        /// <param name="CurrentException"></param>
        /// <param name="SpecialCode"></param>
        public Result(bool Success, byte[] Value, Exception CurrentException, Methods SpecialCode = Methods.DEFAULT) : this(Success, Value, typeof(byte[]), CurrentException, SpecialCode)
        { }

        /// <summary>
        /// Instantiate a new result
        /// </summary>
        /// <param name="Success"></param>
        /// <param name="Value"></param>
        /// <param name="CurrentException"></param>
        /// <param name="SpecialCode"></param>
        public Result(bool Success, bool Value, Exception CurrentException, Methods SpecialCode = Methods.DEFAULT) : this(Success, Value, typeof(bool), CurrentException, SpecialCode)
        { }

        /// <summary>
        /// Instantiate a new result
        /// </summary>
        /// <param name="Success"></param>
        /// <param name="Value"></param>
        /// <param name="SpecialCode"></param>
        public Result(bool Success, string Value, Methods SpecialCode = Methods.DEFAULT) : this(Success, Value, typeof(string), null, SpecialCode)
        { }

        /// <summary>
        /// Instantiate a new result
        /// </summary>
        /// <param name="Success"></param>
        /// <param name="Value"></param>
        /// <param name="SpecialCode"></param>
        public Result(bool Success, byte[] Value, Methods SpecialCode = Methods.DEFAULT) : this(Success, Value, typeof(byte[]), null, SpecialCode)
        { }

        /// <summary>
        /// Instantiate a new result
        /// </summary>
        /// <param name="Success"></param>
        /// <param name="Value"></param>
        /// <param name="SpecialCode"></param>
        public Result(bool Success, bool Value, Methods SpecialCode = Methods.DEFAULT) : this(Success, Value, typeof(bool), null, SpecialCode)
        { }

        /// <summary>
        /// Instantiate a new result
        /// </summary>
        /// <param name="Success"></param>
        /// <param name="SpecialCode"></param>
        public Result(bool Success, Methods SpecialCode = Methods.DEFAULT) : this(Success, Success, SpecialCode)
        { }

        /// <summary>
        /// Instantiate a new result
        /// </summary>
        /// <param name="Success"></param>
        /// <param name="CurrentException"></param>
        /// <param name="SpecialCode"></param>
        public Result(bool Success, Exception CurrentException, Methods SpecialCode = Methods.DEFAULT) : this(Success, Success, CurrentException, SpecialCode)
        { }

    }
}
