﻿namespace JurJurMaker2D.WebApi.Services
{
    public interface IAuthenticationService
    {            /// <summary>
                 /// Returns the user name of the authenticated user
                 /// </summary>
                 /// <returns></returns>
        string? GetCurrentAuthenticatedUserId();
    }
}
