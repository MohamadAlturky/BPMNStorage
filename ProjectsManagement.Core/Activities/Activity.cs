using ProjectsManagement.Core.Common;
using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.Contracts.Entities;

namespace ProjectsManagement.Core.Activities;


public partial class Activity : ResourceEntityBase
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }


    public int Project { get; set; }
    public int ActivityType { get; set; }
    public int ActivityResourceType { get; set; }


    public virtual ICollection<ActivityPrecedent> ActivityPrecedentActivityNavigations { get; set; } = new List<ActivityPrecedent>();

    public virtual ICollection<ActivityPrecedent> ActivityPrecedentPrecedentNavigations { get; set; } = new List<ActivityPrecedent>();

    public virtual ActivityResourceType ActivityResourceTypeNavigation { get; set; } = null!;

    public virtual ActivityType ActivityTypeNavigation { get; set; } = null!;

    public virtual Project ProjectNavigation { get; set; } = null!;
}
