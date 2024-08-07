using ProjectsManagement.Core.Projects;

namespace ProjectsManagement.API.Endpoints.Projects;

public class CreateProjectRequest
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ProjectType { get; set; }
}


public class UpdateProjectRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ProjectType { get; set; }
}

public class FilterProjectRequest
{
    public ProjectFilter Filter { get; set; }
}

public class GetProjectByIdRequest
{
    public int Id { get; set; }
}