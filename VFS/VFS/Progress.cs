// ------------------------------------------------------------------------
// Progress.cs written by Code A Software (http://www.code-a-software.net)
// Created on:      28.12.2016
// Last update on:  23.11.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFS.Helpers;

namespace VFS
{
    /// <summary>
    /// Describes the method of a method in ModifiedVFS
    /// </summary>
    public class Progress
    {
        private double value = 0.0, entireValue = 0.0;

        /// <summary>
        /// Code to identfy the method
        /// </summary>
        public Methods SpecialCode = 0x00;

        /// <summary>
        /// The value of of the current progress
        /// </summary>
        public double Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
                checkForFinal();
            }
        }

        /// <summary>
        /// The value of the main-progress
        /// </summary>
        public double EntireValue
        {
            get
            {
                return this.entireValue;
            }
            set
            {
                this.entireValue = value;
                checkForFinal();
            }
        }

        /// <summary>
        /// The VFS instance which can cause a progress change
        /// </summary>
        public VFS Handle { get; private set; }

        /// <summary>
        /// List which contains each progress to calculate the final value if there is more than one progress at the same time
        /// </summary>
        public static ExtendedList<Progress> LstProgress = new ExtendedList<Progress>();

        /// <summary>
        /// onValueChanged is called when the value or the main-progress is changed
        /// </summary>
        /// <param name="value">T</param>
        /// <param name="step"></param>
        /// <param name="handle"></param>
        public delegate void onValueChanged(double value, double step, VFS handle);

        /// <summary>
        /// OnValueChanged is called when the value or the main-progress is changed
        /// </summary>
        public static event onValueChanged OnValueChanged;

        static Progress()
        {
            LstProgress.OnItemChanged += LstProgress_OnItemChanged;
        }

        private static void LstProgress_OnItemChanged(Progress[] item)
        {
            // Calculate the main progress
            int count = LstProgress.Count;
            double sum = 0.0;
            double sumE = 0.0;

            // Currently there are just theses types of VFS-systems!
            Type[] vfsTypes = new Type[] { typeof(SplitVFS), typeof(ExtendedVFS.ExtendedVFS) }; //, typeof(CryptoVFS) };

            foreach (Type vfsType in vfsTypes)
            {
                List<Progress> filterdList = new List<Progress>();
                
                foreach (Progress prg in LstProgress)
                {
                    if (prg.Handle != null && prg.Handle.GetType() == vfsType)
                        filterdList.Add(prg);
                }

                sum = 0.0;
                sumE = 0.0;
                count = filterdList.Count;

                foreach (Progress prg in filterdList)
                {
                    sum += prg.Value;
                    sumE += prg.EntireValue;
                }

                double stepValue = (count == 0 ? 0 : sum / count);
                double entireValue = (count == 0 ? 0 : sumE / count);

                Progress.OnValueChanged?.Invoke(stepValue, entireValue, count == 0 ? null : filterdList[0].Handle);
            }
        }

        /// <summary>
        /// Instantiates a new progress and adds this progress to the list of all progresses
        /// </summary>
        /// <param name="Value">Value of the current progress</param>
        /// <param name="EntireValue">Value of the main progress</param>
        /// <param name="SpecialCode">Code to identify the method</param>
        /// <param name="handle">VFS handle to identify this progress</param>
        public Progress(double Value, double EntireValue, VFS handle, Methods SpecialCode)
        {
            this.Value = Value;
            this.EntireValue = EntireValue;
            this.Handle = handle;
            this.SpecialCode = SpecialCode;
            LstProgress.Add(this);
        }

        /// <summary>
        /// Instantiate a new progress and adds this progress to the list of all progresses
        /// </summary>
        public Progress()
        {
            LstProgress.Add(this);
        }

        private void checkForFinal()
        {
            if (value == 1 && this.EntireValue == 1)
            {
                // Delete this from LstProgress
                if (LstProgress.Contains(this))
                {
                    this.Handle.VStopWatch.Stop();
                    this.Handle.VStopWatch.Reset();
                    LstProgress.Remove(this);
                }
            }

            LstProgress_OnItemChanged(null);
        }

        /// <summary>
        /// Get the progress by the method identifier
        /// </summary>
        /// <param name="SpecialCode">Code to identify the code</param>
        /// <param name="handle">Code to identify handle</param>
        /// <returns></returns>
        public static Progress GetProgress(Methods SpecialCode, VFS handle)
        {
            foreach (Progress prg in LstProgress)
                if (prg.SpecialCode == SpecialCode && prg.Handle == handle)
                    return prg;
            return null;
        }

        /// <summary>
        /// Creates a new progress with the values or change the values of an existing progress
        /// </summary>
        /// <param name="value">The value of the current progress</param>
        /// <param name="entireValue">The value of the main progress</param>
        /// <param name="SpecialCode">The code to identify the method</param>
        /// <param name="handle">The vfs instance to identify the progress-item</param>
        public static void Register(double value, double entireValue, VFS handle, Methods SpecialCode)
        {
            Progress currentProgress = GetProgress(SpecialCode, handle);

            if (currentProgress != null)
            {
                currentProgress.EntireValue = entireValue;
                currentProgress.Value = value;
            }
            else
            {
                Progress prg = new Progress(value, entireValue, handle, SpecialCode);
                handle.VStopWatch.Stop();
                handle.VStopWatch.Reset();
                handle.VStopWatch.Start();
                LstProgress.Add(prg);
            }
        }

        /// <summary>
        /// Creates a new progress with the values or change the values of an existing progress
        /// </summary>
        /// <param name="value">The value of the current progress</param>
        /// /// <param name="handle">VFS handle to differ between different systems</param>
        /// <param name="SpecialCode">The code to identify the method</param>
        public static void Register(double value, VFS handle, Methods SpecialCode)
        {
            Progress currentProgress = GetProgress(SpecialCode, handle);

            if (currentProgress != null)
                currentProgress.Value = value;
            else
            {
                Progress prg = new Progress(value, 0.0, handle, SpecialCode);
                handle.VStopWatch.Stop();
                handle.VStopWatch.Reset();
                handle.VStopWatch.Start();
                LstProgress.Add(prg);
            }
        }

        /// <summary>
        /// Creates a new progress with the values or change the values of an existing progress
        /// </summary>
        /// <param name="entireState">The value of the main progress</param>
        /// <param name="SpecialCode">The code to identify the method</param>
        /// <param name="handle">VFS handle to differ between different systems</param>
        /// <param name="different">Just to differentiate between the other method (not used in the method)</param>        
        public static void Register(double entireState, VFS handle, Methods SpecialCode, bool different = false)
        {
            Progress currentProgress = GetProgress(SpecialCode, handle);

            if (currentProgress != null)
                currentProgress.EntireValue = entireState;
            else
            {
                Progress prg = new Progress(0.0, entireState, handle, SpecialCode);
                handle.VStopWatch.Stop();
                handle.VStopWatch.Reset();
                handle.VStopWatch.Start();
                LstProgress.Add(prg);
            }
        }
    }
}
