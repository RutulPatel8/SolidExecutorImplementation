using TPMS.Models;

namespace TPMS.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for determining the primary responsibility for tasks, subtasks and projects
    /// </summary>
    public interface IPrimaryResponsibilityExecutor
    {
        /// <summary>
        /// Determines the manager responsible for a task at a specific stage
        /// </summary>
        /// <param name="task">The task being processed</param>
        /// <param name="stage">The current stage</param>
        /// <param name="resourceId">The resource ID</param>
        /// <param name="isActuator">Indicates if this is an actuator type</param>
        /// <returns>The manager responsible for the task</returns>
        manager GetTaskResponsibility(tpms_task task, Stage stage, int resourceId, bool isActuator);

        /// <summary>
        /// Determines the manager responsible for a subtask
        /// </summary>
        /// <param name="subTask">The subtask being processed</param>
        /// <param name="resourceId">The resource ID</param>
        /// <returns>The manager responsible for the subtask</returns>
        manager GetSubTaskResponsibility(sub_task subTask, int resourceId);

        /// <summary>
        /// Determines the manager responsible for a project
        /// </summary>
        /// <param name="externalUniqueId">External Unique Id of the Project</param>
        /// <param name="resource">Resource of the Project</param>
        /// <param name="currentStageSequence">Sequence of the Project's Current Stage</param>
        /// <param name="currentStageSuccessorStage">Successor Stage of the Project's Current Stage</param>
        /// <returns>The manager responsible for the project</returns>
        manager GetProjectResponsibility(string externalUniqueId, Resource resource, double? currentStageSequence = 0.0, string currentStageSuccessorStage = null);
    }
}
