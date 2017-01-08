// ------------------------------------------------------------------------
// Progress.cs written by Code A Software (http://www.code-a-software.net)
// SP: VHP-0001 (OpenSource-Software)
// Created on:      28.12.2016
// Last update on:  08.01.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFS.Extensions;

namespace VFS.ExtendedVFS.Wrapper
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

        private static double mainValue = 0.0, mainEntireValue = 0.0;
        /// <summary>
        /// List which contains each progress to calculate the final value if there is more than one progress at the same time
        /// </summary>
        public static Extensions.ExtendedList<Progress> LstProgress = new Extensions.ExtendedList<Progress>();

        /// <summary>
        /// onValueChanged is called when the value or the main-progress is changed
        /// </summary>
        /// <param name="value">T</param>
        /// <param name="step"></param>
        public delegate void onValueChanged(double value, double step);

        /// <summary>
        /// OnValueChanged is called when the value or the main-progress is changed
        /// </summary>
        public static event onValueChanged OnValueChanged;

        /// <summary>
        /// The calculated value of all progress-instances
        /// </summary>
        public static double MainValue
        {
            get
            {
                return mainValue;
            }
            private set
            {
                mainValue = value;
            }
        }

        /// <summary>
        /// The calculatet value of all progress-instances which describes the main-progress
        /// </summary>
        public static double MainEntireValue
        {
            get
            {
                return mainEntireValue;
            }
            private set
            {
                mainEntireValue = value;
            }
        }

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

            foreach (Progress prg in LstProgress)
            {
                sum += prg.Value;
                sumE += prg.EntireValue;
            }

            MainValue = (count == 0 ? 0 : sum / count);
            MainEntireValue = (count == 0 ? 0 : sumE / count);

            Progress.OnValueChanged?.Invoke(MainValue, MainEntireValue);
        }

        /// <summary>
        /// Instantiates a new progress and adds this progress to the list of all progresses
        /// </summary>
        /// <param name="Value">Value of the current progress</param>
        /// <param name="EntireValue">Value of the main progress</param>
        /// <param name="SpecialCode">Code to identify the method</param>
        public Progress(double Value, double EntireValue, Methods SpecialCode)
        {
            this.Value = Value;
            this.EntireValue = EntireValue;
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
                    LstProgress.Remove(this);
            }

            LstProgress_OnItemChanged(null);
        }

        /// <summary>
        /// Get the progress by the method identifier
        /// </summary>
        /// <param name="SpecialCode">Code to identify the code</param>
        /// <returns></returns>
        public static Progress GetProgress(Methods SpecialCode)
        {
            foreach (Progress prg in LstProgress)
                if (prg.SpecialCode == SpecialCode)
                    return prg;
            return null;
        }

        /// <summary>
        /// Creates a new progress with the values or change the values of an existing progress
        /// </summary>
        /// <param name="value">The value of the current progress</param>
        /// <param name="entireValue">The value of the main progress</param>
        /// <param name="SpecialCode">The code to identify the method</param>
        public static void Register(double value, double entireValue, Methods SpecialCode)
        {
            Progress currentProgress = GetProgress(SpecialCode);

            if (currentProgress != null)
            {
                currentProgress.EntireValue = entireValue;
                currentProgress.Value = value;
            }
            else
            {
                Progress prg = new Progress(value, entireValue, SpecialCode);
                LstProgress.Add(prg);
            }
        }

        /// <summary>
        /// Creates a new progress with the values or change the values of an existing progress
        /// </summary>
        /// <param name="value">The value of the current progress</param>
        /// <param name="SpecialCode">The code to identify the method</param>
        public static void Register(double value, Methods SpecialCode)
        {
            Progress currentProgress = GetProgress(SpecialCode);

            if (currentProgress != null)
                currentProgress.Value = value;
            else
            {
                Progress prg = new Progress(value, 0.0, SpecialCode);
                LstProgress.Add(prg);
            }
        }

        /// <summary>
        /// Creates a new progress with the values or change the values of an existing progress
        /// </summary>
        /// <param name="entireState">The value of the main progress</param>
        /// <param name="SpecialCode">The code to identify the method</param>
        /// <param name="different">Just to differentiate between the other method (not used in the method)</param>
        public static void Register(double entireState, Methods SpecialCode, bool different = false)
        {
            Progress currentProgress = GetProgress(SpecialCode);

            if (currentProgress != null)
                currentProgress.EntireValue = entireState;
            else
            {
                Progress prg = new Progress(0.0, entireState, SpecialCode);
                LstProgress.Add(prg);
            }
        }
    }
}
