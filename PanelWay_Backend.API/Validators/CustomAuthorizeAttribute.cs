using Microsoft.AspNetCore.Authorization;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Utils;

namespace PanelWay_Backend.API.Validators;

public class CustomAuthorizeAttribute: AuthorizeAttribute
{
    public CustomAuthorizeAttribute(params RoleEnum[] roleEnums)
    {
        var allowedRolesAsString = roleEnums.Select(x => x.GetDescriptionFromEnum());
        Roles = string.Join(",", allowedRolesAsString);
    }
}