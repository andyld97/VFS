// ------------------------------------------------------------------------
// StringHelper.cs written by Code A Software (http://www.code-a-software.net)
// Created on:      11.04.2016
// Last update on:  23.11.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFS.Interfaces;

namespace VFS.Helpers
{
    public static class StringHelper
    {
        public static string ExtractExtension(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (!input.Contains("."))
                    return string.Empty;
                else
                {
                    string[] pathSegements = input.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                    if (pathSegements.Length > 0)
                        return pathSegements[pathSegements.Length - 1];
                }
            }

            return string.Empty;
        }

        public static string ExtractExtension(this IFile input)
        {
            return ExtractExtension(input.GetName());
        }
    }
}
