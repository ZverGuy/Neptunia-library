using System;
using System.Threading;
using System.Threading.Tasks;
using Neptunia_library.DTOs;

namespace Neptunia_library
{
    internal static class NeptuniaTaskFactory
    {


        internal static Task<TResult> CreateTaskWithExceptionChain<TResult>( Func<TResult> mainfunc,Func<TResult>[] funcsAfterExcepion, object continualtion)
        {
            
            TaskScheduler syncContextScheduler;
            if (SynchronizationContext.Current != null)
            {
                syncContextScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            }
            else
            {
                // If there is no SyncContext for this thread (e.g. we are in a unit test
                // or console scenario instead of running in an app), then just use the
                // default scheduler because there is no UI thread to sync with.
                syncContextScheduler = TaskScheduler.Current;
            }

            var task = Task<TResult>.Factory.StartNew(mainfunc);
            foreach (Func<TResult> exceptionFunc in funcsAfterExcepion)
            {
                task = task.ContinueWith<TResult>(
                    (task, o) => exceptionFunc.Invoke(),
                    continualtion,
                    TaskContinuationOptions.OnlyOnFaulted);
            }

            return task;
        }
    }
}