using ProjectsManagement.SharedKernel.Contracts.Entities;

namespace ProjectsManagement.Core.Activities;


public partial class ActivityPrecedent : EntityBase
{
    public int Activity { get; set; }

    public int Precedent { get; set; }

    public virtual Activity ActivityNavigation { get; set; } = null!;

    public virtual Activity PrecedentNavigation { get; set; } = null!;
}
