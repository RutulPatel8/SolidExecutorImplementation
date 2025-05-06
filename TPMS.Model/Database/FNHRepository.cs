// File: TPMS.LTVL/ResponsibilityExecutor.cs
using System.Linq.Expressions;
using TPMS.Models;

namespace TPMS.Model
{
    /// <summary>
    /// Static repository class that simulates database operations with in-memory data
    /// </summary>
    public static class FNHRepository
    {
        // In-memory static data storage for various entity types
        private static Dictionary<Type, object> _dataStore = new Dictionary<Type, object>
        {
            { typeof(StageWisePGWiseResponsibility), new List<StageWisePGWiseResponsibility>() },
            { typeof(SubTaskWisePGWiseResponsibility), new List<SubTaskWisePGWiseResponsibility>() },
            { typeof(StageNPGWiseLeadTimeMaster), new List<StageNPGWiseLeadTimeMaster>() },
            { typeof(manager), new List<manager>() },
            { typeof(organization_structure), new List<organization_structure>() },
            { typeof(Resource), new List<Resource>() }
        };

        // Session simulation for direct queries
        public static class Session
        {
            public static IQueryable<T> Query<T>()
            {
                if (_dataStore.TryGetValue(typeof(T), out object data))
                {
                    return (data as List<T>).AsQueryable();
                }
                return new List<T>().AsQueryable();
            }
        }

        /// <summary>
        /// Initializes the repository with sample data
        /// </summary>
        static FNHRepository()
        {
            InitializeSampleData();
        }

        /// <summary>
        /// Finds entities by specified criteria
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="where">Filter expression</param>
        /// <returns>Collection of matching entities</returns>
        public static IQueryable<T> FindBy<T>(Expression<Func<T, bool>> where)
        {
            if (_dataStore.TryGetValue(typeof(T), out object data))
            {
                return (data as List<T>).AsQueryable().Where(where);
            }
            return new List<T>().AsQueryable();
        }

        /// <summary>
        /// Finds all entities of a specified type
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>All entities of the specified type</returns>
        public static IQueryable<T> FindAll<T>()
        {
            if (_dataStore.TryGetValue(typeof(T), out object data))
            {
                return (data as List<T>).AsQueryable();
            }
            return new List<T>().AsQueryable();
        }

        /// <summary>
        /// Adds a new entity to the repository
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity to add</param>
        public static void Add<T>(T entity)
        {
            if (_dataStore.TryGetValue(typeof(T), out object data))
            {
                (data as List<T>).Add(entity);
            }
            else
            {
                var list = new List<T> { entity };
                _dataStore[typeof(T)] = list;
            }
        }

        /// <summary>
        /// Populates the repository with sample data for testing
        /// </summary>
        private static void InitializeSampleData()
        {
            // Create sample managers
            var manager1 = new manager { Id = 1, Name = "Domestic Sales Head", LoginName = "20003603", Department = "Sales" };
            var manager2 = new manager { Id = 2, Name = "Global Sales Head", LoginName = "20330606", Department = "Sales" };
            var manager3 = new manager { Id = 3, Name = "Plant Head", LoginName = "20004501", Department = "Manufacturing" };
            var manager4 = new manager { Id = 4, Name = "Safety Supervisor", LoginName = "20008754", Department = "Safety", Role = "SafetySupervisor" };
            var manager5 = new manager { Id = 5, Name = "Priority Project Manager", LoginName = "20009123", Department = "Projects", Role = "PriorityProjectManager" };
            var manager6 = new manager { Id = 6, Name = "Domestic Projects Manager", LoginName = "20010234", Department = "DomesticProjects" };
            var manager7 = new manager { Id = 7, Name = "International Projects Manager", LoginName = "20011345", Department = "InternationalProjects" };

            // Add managers to repository
            AddMultiple(new[] { manager1, manager2, manager3, manager4, manager5, manager6, manager7 });

            // Create sample organization structures
            var org1 = new organization_structure { Id = 1, Name = "Manufacturing", ExternalUniqueId = "Manufacturing", ManagedBy = manager3 };
            var org2 = new organization_structure { Id = 2, Name = "Production Department", ExternalUniqueId = "ProductionDept", ManagedBy = manager3 };

            // Add organization structures to repository
            AddMultiple(new[] { org1, org2 });

            // Create sample resources
            var resource1 = new Resource { Id = 101, Name = "Resource 1", ManagedBy = manager1 };
            var resource2 = new Resource { Id = 102, Name = "Resource 2", ManagedBy = manager2, Priority = "High" };

            // Add resources to repository
            AddMultiple(new[] { resource1, resource2 });

            // Create sample StageWisePGWiseResponsibility entries
            var stageResp1 = new StageWisePGWiseResponsibility { Id = 1, TaskExternalUniqueId = "Stage10", Resource = resource1, ManagedBy = manager1 };
            var stageResp2 = new StageWisePGWiseResponsibility { Id = 2, TaskExternalUniqueId = "Stage20", Resource = resource1, ManagedBy = manager3 };
            var stageResp3 = new StageWisePGWiseResponsibility { Id = 3, TaskExternalUniqueId = "Stage30", Resource = resource2, ManagedBy = manager2 };

            // Add StageWisePGWiseResponsibility entries to repository
            AddMultiple(new[] { stageResp1, stageResp2, stageResp3 });

            // Create sample SubTaskWisePGWiseResponsibility entries
            var subTaskResp1 = new SubTaskWisePGWiseResponsibility { Id = 1, SubTaskExternalUniqueId = "SubTask1", Resource = resource1, ManagedBy = manager1 };
            var subTaskResp2 = new SubTaskWisePGWiseResponsibility { Id = 2, SubTaskExternalUniqueId = "SubTask2", Resource = resource2, ManagedBy = manager2 };

            // Add SubTaskWisePGWiseResponsibility entries to repository
            AddMultiple(new[] { subTaskResp1, subTaskResp2 });

            // Create sample StageNPGWiseLeadTimeMaster entries
            var leadTime1 = new StageNPGWiseLeadTimeMaster { Id = 1, GroupName = "Group1", stage_ExternalUniqueId = "Stage10", duration = 5 };
            var leadTime2 = new StageNPGWiseLeadTimeMaster { Id = 2, GroupName = "Group2", stage_ExternalUniqueId = "Stage20", duration = 10 };
            var leadTime3 = new StageNPGWiseLeadTimeMaster { Id = 3, GroupName = "HPGroup", stage_ExternalUniqueId = "Stage10", duration = 15 };
            var leadTime4 = new StageNPGWiseLeadTimeMaster { Id = 4, GroupName = "Group1URGENT", stage_ExternalUniqueId = "Stage20", duration = 8 };

            // Add StageNPGWiseLeadTimeMaster entries to repository
            AddMultiple(new[] { leadTime1, leadTime2, leadTime3, leadTime4 });
        }

        /// <summary>
        /// Adds multiple entities to the repository
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entities">Entities to add</param>
        private static void AddMultiple<T>(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }
    }
}