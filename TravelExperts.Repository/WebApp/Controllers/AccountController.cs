using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelExperts.BLL;
using TravelExperts.Repository.Domain;
using TravelExpertsWebApp.Models;

namespace TravelExpertsWebApp.Controllers
{
    /*
     * Authentication and account management done by Eric
     */

    public class AccountController : Controller
    {
        // Route:   /Account/Login
        public IActionResult Login(string returnUrl = null)
        {
            // If redirected to the login page, returnUrl will hold the link to go to after logging in
            if (returnUrl != null)
                TempData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST overload of login that checks the database for a matching user, then Authenticates them
        [HttpPost]
        public async Task<IActionResult> LoginAsync(CredentialModel loginAttempt)
        {
            // Attempt to find a matching user -> if none, returns null
            var usr = AccountManager.Authenticate(loginAttempt.Login, loginAttempt.Password);

            // If no match, return to the Login page
            if (usr == null)
            {
                // Provide a message to display on the login page to alert the user
                ViewBag.BadLogin = "The username or password you provided is not correct.";
                return View();
            }

            // If we didn't return to the view above, we can begin adding the user's authentication
            // First, create list of claims needed to be stored
            List<Claim> claims = new List<Claim>()
            {
                // Only need ID (unique name, useable for queries) and first name (for display)
                new Claim(ClaimTypes.Name, usr.CustId.ToString()),
                new Claim("FirstName", usr.FirstName),
            };

            // Creat a claims identity in cookies using the claims list
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Create the claims principle and sign in
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            // Add name to TempData
            TempData["FName"] = usr.FirstName;

            // Check to see if the user was redirected from another page to the login. If so, go there
            if (TempData["ReturnUrl"] == null)
                return RedirectToAction("Index", "Home");
            else
            {
                // Remove returnUrl from tempdata so it's not reused
                string returnUrl = TempData["ReturnUrl"].ToString();
                TempData["ReturnUrl"] = null;

                // Then, go to that url
                return Redirect(returnUrl);
            }

                
        }

        // Logs user out
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["FName"] = null;
            return RedirectToAction("Index", "Home");
        }

    }
}
