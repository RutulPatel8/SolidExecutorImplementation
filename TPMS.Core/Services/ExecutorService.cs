using TPMS.Core.Factory;
using TPMS.Core.Interfaces;
using TPMS.Models;

namespace TPMS.Core.Services
{
    /// <summary>
    /// Central service for accessing executors
    /// </summary>
    public class ExecutorService
    {
        /// <summary>
        /// Gets the manager responsible for a task
        /// </summary>
        public static manager GetTaskResponsibility(tpms_task task, Stage stage, int resourceId, bool isActuator, string clientName = null)
        {
            IPrimaryResponsibilityExecutor executor = ExecutorFactory.CreateResponsibilityExecutor(clientName);
            return executor.GetTaskResponsibility(task, stage, resourceId, isActuator);
        }

        /// <summary>
        /// Gets the manager responsible for a subtask
        /// </summary>
        public static manager GetSubTaskResponsibility(sub_task subTask, int resourceId, string clientName = null)
        {
            IPrimaryResponsibilityExecutor executor = ExecutorFactory.CreateResponsibilityExecutor(clientName);
            return executor.GetSubTaskResponsibility(subTask, resourceId);
        }

        /// <summary>
        /// Gets the manager responsible for a project
        /// </summary>
        public static manager GetProjectResponsibility(string externalUniqueId, Resource resource, double? currentStageSequence = 0.0, string currentStageSuccessorStage = null, string clientName = null)
        {
            IPrimaryResponsibilityExecutor executor = ExecutorFactory.CreateResponsibilityExecutor(clientName);
            return executor.GetProjectResponsibility(externalUniqueId, resource, currentStageSequence, currentStageSuccessorStage);
        }

        /// <summary>
        /// Calculates the lead time for a given group and stage
        /// </summary>
        //public static double CalculateLeadTime(string groupName, string stageExternalUniqueId, double defaultDuration, string clientName = null)
        //{
        //    ILeadTimeExecutor executor = ExecutorFactory.CreateLeadTimeExecutor(clientName);
        //    return executor.CalculateLeadTime(groupName, stageExternalUniqueId, defaultDuration);
        //}
    }
}
