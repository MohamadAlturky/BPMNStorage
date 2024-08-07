using ProjectsManagement.Core.ProjectTasks;

namespace ProjectsManagement.API.Endpoints.ProjectTasks;

public class GetProjectTaskByIdRequest
{
    public int Id { get; set; }
}

public class FilterProjectTaskRequest
{
    public ProjectTaskFilter Filter { get; set; }
}

public class UpdateProjectTaskRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Project { get; set; }
    public int TaskStatus { get; set; }
}

public class CreateProjectTaskRequest
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Project { get; set; }
    public int TaskStatus { get; set; }
}