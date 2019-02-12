namespace OpenCaseManager.Managers
{
    public interface IDCRService
    {
        #region Active Repository
        string InitializeGraph(string graphId);

        string GetPendingOrEnabled(string graphId, string simulationId);

        string ExecuteEvent(string graphId, string simulationId, string eventId);

        string GetProcessRoles(string graphId);

        string SearchProcess(string title);

        string GetProcess(string graphId);

        string GetProcessPhases(string graphId);

        string AdvanceTime(string graphId, string simulationId, string time);
        #endregion
    }
}
