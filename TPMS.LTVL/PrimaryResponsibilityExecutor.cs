// File: TPMS.LTVL/ResponsibilityExecutor.cs
using TPMS.Core.Interfaces;
using TPMS.Model;
using TPMS.Models;

namespace TPMS.LTVL
{
    /// <summary>
    /// LTVL-specific implementation of responsibility executor
    /// </summary>
    public class PrimaryResponsibilityExecutor : IPrimaryResponsibilityExecutor
    {
        public manager GetTaskResponsibility(tpms_task task, Stage stage, int resourceId, bool isActuator)
        {
            Console.WriteLine("LTVL Primary GetTaskResponsibility implementation");
            StageWisePGWiseResponsibility stageWisePGWiseResponsibility;

            // LTVL-specific logic for actuator type handling
            if (isActuator && stage.ExternalUniqueId.ToLower() == "stage20")
            {
                // For Stage20 Managed By will be same as Stage10 Managed By if Valve is Actuated Type
                stageWisePGWiseResponsibility = GetStageWisePGWisePrimaryResponsibility("Stage10", resourceId);
            }
            else
            {
                stageWisePGWiseResponsibility = GetStageWisePGWisePrimaryResponsibility(stage.ExternalUniqueId, resourceId);
            }

            // Determine task responsibility
            if (TaskValidator(task, stageWisePGWiseResponsibility))
            {
                return stageWisePGWiseResponsibility.ManagedBy ?? task.ManagedBy;
            }
            else
            {
                return task.ManagedBy;
            }
        }

        public manager GetSubTaskResponsibility(sub_task subTask, int resourceId)
        {
            Console.WriteLine("LTVL Primary GetSubTaskResponsibility implementation");
            SubTaskWisePGWiseResponsibility subTaskWisePGWiseResponsibility = FNHRepository
                .FindBy<SubTaskWisePGWiseResponsibility>(x =>
                    x.SubTaskExternalUniqueId == subTask.ExternalUniqueId &&
                    x.Resource.Id == resourceId)
                .FirstOrDefault();

            if (SubTaskValidator(subTask, subTaskWisePGWiseResponsibility))
            {
                return subTaskWisePGWiseResponsibility.ManagedBy ?? subTask.ManagedBy;
            }
            else
            {
                return subTask.ManagedBy;
            }
        }

        public manager GetProjectResponsibility(string externalUniqueId, Resource resource, double? currentStageSequence = 0.0, string currentStageSuccessorStage = null)
        {
            // LTVL-specific logic for project responsibility
            Console.WriteLine("LTVL Primary GetProjectResponsibility implementation");

            // If Manufacturing Clearance has been given and Assembly is not completed then responsibility will be with Plant Head
            if ((currentStageSequence > 20 && currentStageSequence < 80) || !string.IsNullOrEmpty(currentStageSuccessorStage))
            {
                var organization = FNHRepository.FindBy<organization_structure>(x => x.ExternalUniqueId == "Manufacturing").FirstOrDefault();
                return organization?.ManagedBy;
            }
            // LTVL-specific region handling
            else
            {
                // Domestic Region - responsibility is with Domestic Sales Head
                if (externalUniqueId.StartsWith("11"))
                {
                    return FNHRepository.FindBy<manager>(x => x.LoginName == "20003603").FirstOrDefault();
                }
                // International Region - responsibility is with Global Sales Head
                else if (externalUniqueId.StartsWith("12"))
                {
                    return FNHRepository.FindBy<manager>(x => x.LoginName == "20330606").FirstOrDefault();
                }
                // Default responsibility is to the resource's manager
                else
                {
                    return resource.ManagedBy;
                }
            }
        }

        #region Helper Methods
        private StageWisePGWiseResponsibility GetStageWisePGWisePrimaryResponsibility(string stageExternalUniqueId, int resourceId)
        {
            return FNHRepository.FindBy<StageWisePGWiseResponsibility>(x =>
                x.TaskExternalUniqueId == stageExternalUniqueId &&
                x.Resource.Id == resourceId)
                .FirstOrDefault();
        }

        private bool TaskValidator(tpms_task task, StageWisePGWiseResponsibility stageWisePGWiseResponsibility)
        {
            return task != null && stageWisePGWiseResponsibility != null;
        }

        private bool SubTaskValidator(sub_task subTask, SubTaskWisePGWiseResponsibility subTaskWisePGWiseResponsibility)
        {
            return subTask != null && subTaskWisePGWiseResponsibility != null;
        }
        #endregion
    }
}