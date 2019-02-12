namespace OpenCaseManager.Managers
{
    public interface IActiveRepositoryService
    {
        string InitializeGraph(string graphId);

        string GetPendingOrEnabled(string graphId, string simulationId);

        string ExecuteEvent(string graphId, string simulationId, string eventId);

        string GetProcessRoles(string graphId);

        string SearchProcess(string title);

        string GetProcess(string graphId);

        string GetProcessPhases(string graphId);

        string AdvanceTime(string graphId, string simId, string time);
    }
}
