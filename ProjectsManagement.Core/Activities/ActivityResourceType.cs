using ProjectsManagement.SharedKernel.Contracts.Entities;

namespace ProjectsManagement.Core.Activities;

public partial class ActivityResourceType : EntityBase
{

    public string Name { get; set; } = null!;

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();
}
