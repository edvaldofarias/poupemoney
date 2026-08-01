using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace PoupeMoney.Core.WebApi.Controllers;

[ExcludeFromCodeCoverage]
public static class ApiConventions
{
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static void Delete([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id) => throw new NotImplementedException();

    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static void Get([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id) => throw new NotImplementedException();

    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public static void Get() => throw new NotImplementedException();

    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static void DepGet() => throw new NotImplementedException();

    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public static void Post([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model) => throw new NotImplementedException();

    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static void DepPost([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model) => throw new NotImplementedException();

    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public static void Post([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model, [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.AssignableFrom)] ApiVersion apiVersion) => throw new NotImplementedException();

    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static void DepPost([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model, [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] ApiVersion apiVersion) => throw new NotImplementedException();

    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static void Put([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id, [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model) => throw new NotImplementedException();

    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static void Put([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id, [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model, [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Exact), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] ApiVersion apiVersion) => throw new NotImplementedException();
}