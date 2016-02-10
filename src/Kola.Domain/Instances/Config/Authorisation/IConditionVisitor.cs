namespace Kola.Domain.Instances.Config.Authorisation
{
    public interface IConditionVisitor<out T>
    {
        T Visit(HasClaimCondition condition);

        T Visit(IsAuthenticatedCondition condition);

        T Visit(HasAllClaimsCondition condition);

        T Visit(HasAnyClaimsCondition condition);
    }
}