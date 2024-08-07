using ProjectsManagement.Core.Activities;
using ProjectsManagement.Core.Contributions;
using ProjectsManagement.Core.Projects;
using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.Storage.Adapters.Model;

namespace ProjectsManagement.Core.Constants;

public static class ConstantsProvider
{
    public static ProjectTaskStatus ON_WORKING = new()
    {
        Id = 1,
        Name = "On Working"
    };
    public static ProjectTaskStatus FAILED = new()
    {
        Id = 2,
        Name = "Failed"
    };
    public static ProjectTaskStatus FINISHED = new()
    {
        Id = 3,
        Name = "Finished"
    };



    public static InvitationStatus ACCEPTED = new()
    {
        Id = 1,
        Name = "Accepted"
    };
    public static InvitationStatus REJECTED = new()
    {
        Id = 2,
        Name = "Rejected"
    };
    public static InvitationStatus PENDING = new()
    {
        Id = 3,
        Name = "Pending"
    };




    public static ActivityType MERGED = new()
    {
        Id = 1,
        Name = "Merged"
    };
    public static ActivityType INITIALIZED = new()
    {
        Id = 2,
        Name = "Initialized"
    };
    public static ActivityType UPDATED = new()
    {
        Id = 3,
        Name = "Updated"
    };
    public static ActivityType CLOSED = new()
    {
        Id = 4,
        Name = "Closed"
    };




    public static ActivityResourceType IMAGE= new ()
    {
        Id = 1,
        Name = "Image"
    };
    public static ActivityResourceType DIAGRAM = new()
    {
        Id = 2,
        Name = "Diagram"
    };
    public static ActivityResourceType PROCESS_DESCRIPTION = new()
    {
        Id = 3,
        Name = "Process Description"
    };

    public static ActivityResourceType PDF = new()
    {
        Id = 4,
        Name = "Pdf"
    };






    public static ContributionType OWNER = new()
    {
        Id = 1,
        Name = "Owner"
    };
    public static ContributionType CONTRIBUTOR = new()
    {
        Id = 2,
        Name = "Contributor"
    };




    public static ProjectType PUBLIC = new()
    {
        Id = 1,
        Name = "Public"
    };
    public static ProjectType PRIVATE = new()
    {
        Id = 2,
        Name = "Private"
    };
}
