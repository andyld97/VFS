using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;

namespace Setup
{
    /// <summary>
    /// This class conatins methods for registring/unregistring a file extension
    /// </summary>
    public static class FileAssociation
    {
        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        /// <summary>
        /// Associates a file extension with your program
        /// </summary>
        /// <param name="name">Simply the name of the program, e.g. Test</param>
        /// <param name="extension">The extension, e.g. ".def"</param>
        /// <param name="icon">The path of the icon</param>
        /// <param name="path">The path of the programm</param>
        /// <returns>Whether it has worked or not</returns>
        public static bool SetFileAssociation(string name, string extension, string icon, string path)
        {
            try
            {
                Registry.ClassesRoot.CreateSubKey(extension).SetValue(string.Empty, name);
                RegistryKey key = Registry.ClassesRoot.CreateSubKey(name, RegistryKeyPermissionCheck.ReadWriteSubTree);
                key.SetValue(string.Empty, name, RegistryValueKind.String);
                key.CreateSubKey("DefaultIcon").SetValue(string.Empty, icon, RegistryValueKind.String);
                key.CreateSubKey(@"Shell\Open\Command").SetValue("", "\"" + path + "\" \"%1\"", RegistryValueKind.String);
                key = key.CreateSubKey("OpenWithList", RegistryKeyPermissionCheck.ReadWriteSubTree);
                key.CreateSubKey(name);

                key.Flush();
                key.Close();
                FileAssociation.SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Remove the association which you have created
        /// </summary>
        /// <param name="name">Name of your program, e.g. Test</param>
        /// <param name="extension">The extension of your file, e.g. ".def"</param>
        /// <returns>Whether it has worked or not</returns>
        public static bool RemoveFileAssociation(string name, string extension)
        {
            try
            {
                if (Registry.ClassesRoot.OpenSubKey(extension, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl) != null)
                {
                    Registry.ClassesRoot.DeleteSubKeyTree(Registry.ClassesRoot.OpenSubKey(extension, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl).GetValue("").ToString());
                    Registry.ClassesRoot.DeleteSubKeyTree(extension);
                    FileAssociation.SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
