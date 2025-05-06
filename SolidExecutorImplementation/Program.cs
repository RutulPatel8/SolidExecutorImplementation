// Register LTVL executors
using TPMS.Core.Factory;
using TPMS.Models;
using TPMS.Core.Services;

ExecutorFactory.RegisterResponsibilityExecutor("LTVL", typeof(TPMS.LTVL.PrimaryResponsibilityExecutor));
//ExecutorFactory.RegisterLeadTimeExecutor("LTVL", typeof(TPMS.LTVL.LeadTimeExecutor));

// Register ISSEC executors
ExecutorFactory.RegisterResponsibilityExecutor("ISSEC", typeof(TPMS.ISSEC.PrimaryResponsibilityExecutor));
//ExecutorFactory.RegisterLeadTimeExecutor("ISSEC", typeof(TPMS.ISSEC.LeadTimeExecutor));

// Set the default client
string clientName = "ISSEC";
ExecutorFactory.SetDefaultClient(clientName);


// Create sample managers
var manager1 = new manager { Id = 1, Name = "Domestic Sales Head", LoginName = "20003603", Department = "Sales" };
var manager3 = new manager { Id = 3, Name = "Plant Head", LoginName = "20004501", Department = "Manufacturing" };

tpms_task task = new tpms_task
{
    Id = 501,
    Name = "Sample Task",
    ManagedBy = manager3
};

Stage stage = new Stage
{
    Id = 10,
    Name = "Stage 20",
    ExternalUniqueId = "Stage20"
};

Resource resource = new Resource
{
    Id = 101,
    Name = "Test Resource",
    ManagedBy = manager1
};

bool isActuator = true;

manager taskManager = ExecutorService.GetTaskResponsibility(task, stage, resource.Id, isActuator);
