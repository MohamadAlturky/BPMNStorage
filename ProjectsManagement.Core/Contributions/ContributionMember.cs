using ProjectsManagement.Core.Common;
using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace ProjectsManagement.Core.Contributions;

public class ContributionMember : ResourceEntityBase
{
    public int Project { get; set; }

    public int Contributor { get; set; }

    [NotMapped]
    public ContributorInfo ContributorInfo { get; set; } = null!;

    public int ContributionType { get; set; }

    public DateTime Date { get; set; }
    
    public virtual ContributionType ContributionTypeNavigation { get; set; } = null!;

    public virtual Project ProjectNavigation { get; set; } = null!;
}


public class ContributorInfo
{
    [JsonPropertyName("userName")]
    public string UserName { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("id")]
    public int Id { get; set; }
}