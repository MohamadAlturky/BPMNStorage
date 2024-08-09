using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Application.AccessControl;
using ProjectsManagement.Application.Users;
using ProjectsManagement.SharedKernel.AccessControl;
using ProjectsManagement.Storage.Adapters.Context;

namespace ProjectsManagement.Storage.Adapters.CommandAccessControl;

public class CommandExecutionAccessController : IAccessController
{
    private readonly AppDbContext _dbContext;
    private readonly IUserIdentityPort _identityPort;

    public CommandExecutionAccessController(AppDbContext dbContext, IUserIdentityPort identityPort)
    {
        _dbContext=dbContext;
        _identityPort=identityPort;
    }

    public async Task<bool> HasAccess(AccessControlCriteria criteria)
    {
        //int contibutor = await _identityPort.GetUserIdAsync();

        //var query = _dbContext.ContributionMembers.AsQueryable();
        //query = query.Where(e => e.Contributor == contibutor);

        //if (criteria.ContributionMember.HasValue) 
        //{
        //    query = query.Where(e=>e.Id ==  criteria.ContributionMember.Value);
        //}

        //    if (criteria.Project.HasValue)
        //{
        //    query = query.Where(e => e.Project == criteria.Project.Value);
        //}
        //if (criteria.ProjectTask.HasValue)
        //{
        //    query = query
        //        .AsSplitQuery()
        //        .Include(e => e.ProjectNavigation)
        //        .ThenInclude(f => f.Tasks.Where(gg=>gg.Id == criteria.ProjectTask.Value));
        //}
        //if (criteria.Invitation.HasValue)
        //{
        //    query = query
        //        .AsSplitQuery()
        //        .Include(e => e.ProjectNavigation)
        //        .ThenInclude(f => f.Invitations.Where(gg => gg.Id == criteria.Invitation.Value));
        //}
        //if (criteria.ProjectTask.HasValue)
        //{
        //    query = query
        //        .AsSplitQuery()
        //        .Include(e => e.ProjectNavigation)
        //        .ThenInclude(f => f.Tasks.Where(gg => gg.Id == criteria.ProjectTask.Value));
        //}
        //if (criteria.Activity.HasValue)
        //{
        //    query = query
        //        .AsSplitQuery()
        //        .Include(e => e.ProjectNavigation)
        //        .ThenInclude(f => f.Activities.Where(gg => gg.Id == criteria.Activity.Value));
        //}

        return false;

    }
}
