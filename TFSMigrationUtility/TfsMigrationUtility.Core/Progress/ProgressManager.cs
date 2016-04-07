﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Configuration;

namespace TfsMigrationUtility.Core.Progress
{
    public interface IProgressManager
    {
        IEnumerable<IProgress> ProgressHandlers { get; }
        int CurrentStep { get; set; }
        int MaxStep { get; set; }
        void InvokeProgress(int currentstep, int maxstep, string description);
        void InvokeProgress(string description);
        void WriteDebug(string message);
        void WriteException(string message, Exception e);
    }
    public class ProgressManager : IProgressManager
    {
        public int CurrentStep{get; set;}

        public int MaxStep { get; set; }

        public IEnumerable<IProgress> ProgressHandlers
        {
            get
            {

                return ServiceLocator.Get<IEnumerable<IProgress>>();
            }
        }

        public void InvokeProgress(string description)
        {
            InvokeProgress(++CurrentStep, MaxStep, description);
        }

        public void InvokeProgress(int currentstep, int maxstep, string description)
        {
            this.CurrentStep = currentstep;
            this.MaxStep = maxstep;
            foreach (IProgress progress in ProgressHandlers)
            {
                progress.OnProgress(currentstep,maxstep,description);
            }
        }
        public void WriteDebug(string message)
        {
            if (ServiceLocator.Get<MigrationConfig>().DebugMode)
            {
                foreach (IProgress progress in ProgressHandlers)
                {
                    progress.WriteDebug(message);
                }
            }
        }
        public void WriteException(string message, Exception e)
        {
            foreach (IProgress progress in ProgressHandlers)
            {
                progress.WriteException(message,e);
            }
        }
    }
}