#define ProcessEngine
//#define ActiveRepository


namespace OpenCaseManager.Managers
{
    public class DCRService : IDCRService
    {
        private IActiveRepositoryService _activeRepositoryService;
        private IManager _manager;

        public DCRService(IActiveRepositoryService activeRepositoryService, IManager manager)
        {
            _activeRepositoryService = activeRepositoryService;
            _manager = manager;
        }

        #region Active Repository

        /// <summary>
        /// Initialize a graph
        /// </summary>
        /// <param name="graphId"></param>
        /// <returns></returns>
        public string InitializeGraph(string graphId)
        {
            return _activeRepositoryService.InitializeGraph(graphId);
        }

        /// <summary>
        /// Get Pending or Enabled Events
        /// </summary>
        /// <param name="graphId"></param>
        /// <param name="simulationId"></param>
        /// <returns></returns>
        public string GetPendingOrEnabled(string graphId, string simulationId)
        {
            return _activeRepositoryService.GetPendingOrEnabled(graphId, simulationId);
        }

        /// <summary>
        /// Execute an event
        /// </summary>
        /// <param name="graphId"></param>
        /// <param name="simulationId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public string ExecuteEvent(string graphId, string simulationId, string eventId)
        {
            return _activeRepositoryService.ExecuteEvent(graphId, simulationId, eventId);
        }

        /// <summary>
        /// Get Process Roles
        /// </summary>
        /// <param name="graphId"></param>
        /// <returns></returns>
        public string GetProcessRoles(string graphId)
        {
            return _activeRepositoryService.GetProcessRoles(graphId);
        }

        /// <summary>
        /// Search Processes
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public string SearchProcess(string title)
        {
            return _activeRepositoryService.SearchProcess(title);
        }

        /// <summary>
        /// Get Process from active repository using graph id
        /// </summary>
        /// <param name="graphId"></param>
        /// <returns></returns>
        public string GetProcess(string graphId)
        {
            return _activeRepositoryService.GetProcess(graphId);
        }

        /// <summary>
        /// Get process phases
        /// </summary>
        /// <param name="graphId"></param>
        /// <returns></returns>
        public string GetProcessPhases(string graphId)
        {
            return _activeRepositoryService.GetProcessPhases(graphId);
        }

        /// <summary>
        /// Advance time to current time/next deadline
        /// </summary>
        /// <param name="graphId"></param>
        /// <param name="simulationId"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public string AdvanceTime(string graphId, string simulationId, string time)
        {
            return _activeRepositoryService.AdvanceTime(graphId, simulationId, time);
        }
        #endregion
    }
}