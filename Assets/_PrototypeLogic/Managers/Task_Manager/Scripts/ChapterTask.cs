using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeLogic.Task_Manager
{
    public abstract class ChapterTask : TaskBase
    {
        //[SerializeField] private Sprite ChapterSprite;
        //[SerializeField] private string ChapterShortName;
        //[SerializeField] private string ChapterFullName;
        [SerializeField] private SingleTask[] SingleTasks;
        [SerializeField] private SingleTask[] AdditionTasks;

        private Dictionary<TaskTitle, SingleTask> additionTasksInitialized = new Dictionary<TaskTitle, SingleTask>();
        private int SingleTaskIndex { get; set; }
        private int AdditionTaskIndex { get; set; }
        private int AdditionTaskCompletedAmount { get; set; }
        private int SingleTaskCompletedAmount { get; set; }

        public Action<TaskTitle> OnAdditionTaskStarted { get; set; }
        public Action<TaskTitle> OnAdditionTaskCompleted { get; set; }
        public Action OnAllAdditionTasksCompleted { get; set; }

        //public Sprite GetChapterSprite => ChapterSprite;
        //public string GetChapterShortName => ChapterShortName;
        //public string GetFullName => ChapterFullName;

        public virtual void CompleteAllAdditionTasks()
        {
            Debug.Log("ChapterTask Parent Complete Addition Tasks");
            OnAllAdditionTasksCompleted?.Invoke();
        }

        public override void CancelTask()
        {
            Debug.Log("ChapterTask Parent Cancel");
            foreach (var singleTask in SingleTasks)
            {
                singleTask.OnTaskCompleted -= SingleTaskComplete;
                singleTask.CancelTask();
            }

            foreach (var additionTask in AdditionTasks)
            {
                additionTask.OnTaskCompleted -= AdditionTaskComplete;
                additionTask.CancelTask();
                additionTasksInitialized.Add(additionTask.GetTitle, additionTask);
            }
            OnTaskCanceled?.Invoke();
        }

        public override void CompleteTask()
        {
            Debug.Log("ChapterTask Parent Complete");
            OnTaskCompleted?.Invoke(GetTitle);
        }

        public override void ResetValues()
        {
            OnAllAdditionTasksCompleted = null;
            SingleTaskIndex = 0;
            AdditionTaskIndex = 0;
            AdditionTaskCompletedAmount = 0;
            SingleTaskCompletedAmount = 0;
        }

        public override void InitializeTask()
        {
            Debug.Log("ChapterTask Parent Initialize");

            ResetValues();
            foreach (var singleTask in SingleTasks)
            {
                singleTask.InitializeTask();
                singleTask.OnTaskCompleted += SingleTaskComplete;
            }
            
            foreach (var additionTask in AdditionTasks)
            {
                additionTask.InitializeTask();
                additionTask.OnTaskCompleted += AdditionTaskComplete;
            }
            OnTaskInitialized?.Invoke();
        }

        public override void ReloadTask()
        {
            OnTaskReloaded?.Invoke();
            ResetValues();
            Debug.Log("ChapterTask Parent Reload");
            foreach (var singleTask in SingleTasks)
            {
                singleTask.ReloadTask();
            }

            foreach (var additionTask in AdditionTasks)
            {
                additionTask.ReloadTask();
            }
        }

        public override void StartTask()
        {
            Debug.Log("ChapterTask Parent Start");
            SingleTaskStart();
            OnTaskStarted?.Invoke();
        }

        protected void SingleTaskStart()
        {
            if (SingleTaskIndex >= SingleTasks.Length) return;
            SingleTasks[SingleTaskIndex].StartTask();
            SingleTaskIndex++;
        }
        protected void AdditionTaskStart()
        {
            Debug.Log("ADDDINDEX+ "+ AdditionTaskIndex);
            if (AdditionTaskIndex >= AdditionTasks.Length) return;
            AdditionTasks[AdditionTaskIndex].StartTask();
            OnAdditionTaskStarted?.Invoke(AdditionTasks[AdditionTaskIndex].GetTitle);
            AdditionTaskIndex++;
        }

        private void SingleTaskComplete(TaskTitle title)
        {
            SingleTaskCompletedAmount++;
            if (SingleTaskCompletedAmount == SingleTasks.Length)
            {
                Debug.Log("ChapterTask Parent Completed");
                CompleteTask();
            }
            SingleTaskStart();
        }
        private void AdditionTaskComplete(TaskTitle title)
        {
            OnAdditionTaskCompleted?.Invoke(title);
            AdditionTaskCompletedAmount++;
            if (AdditionTaskCompletedAmount == AdditionTasks.Length)
            {
                Debug.Log("ChapterTask Parent AdditionTask Complete and Reward");
                CompleteAllAdditionTasks();
            }


        }

    }
}