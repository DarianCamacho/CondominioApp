using CondominioApp.Models;
using Microsoft.AspNetCore.Mvc;
using Firebase.Auth.Providers;
using Firebase.Auth;
using Microsoft.AspNetCore.Http;

using CondominioApp.FirebaseAuth;

namespace CondominioApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        public async Task<IActionResult> Login(string loginUsername, string loginPassword)
        {
            try
            {
                await FirebaseAuthHelper.setFirebaseAuthClient().SignInWithEmailAndPasswordAsync(loginUsername, loginPassword);

                return RedirectToAction("Index", "Home");
            }
            catch (FirebaseAuthHttpException ex)
            {
                return FirebaseAuthHttpExceptionHandler(ex);
            }
        }

        //SIGN UP
        public async Task<IActionResult> SignUp(string signupUsername, string signupPassword, string signupDisplayName)
        {
            try
            {
                await FirebaseAuthHelper.setFirebaseAuthClient().
                    CreateUserWithEmailAndPasswordAsync(signupUsername, signupPassword, signupDisplayName);

                return View("Index");
            }
            catch (FirebaseAuthHttpException ex)
            {
                return FirebaseAuthHttpExceptionHandler(ex);
            }
        }

        public async Task<IActionResult> ForgotPwd(string signupUsername)
        {
            try
            {
                await FirebaseAuthHelper.setFirebaseAuthClient().
                    ResetEmailPasswordAsync(signupUsername);

                return View("Index");
            }
            catch (FirebaseAuthHttpException ex)
            {
                return FirebaseAuthHttpExceptionHandler(ex);
            }
        }

        public IActionResult FirebaseAuthHttpExceptionHandler(FirebaseAuthHttpException ex)
        {
            ViewBag.Error = new ErrorHandler()
            {
                Title = ex.Reason.ToString(),
                ErrorMessage = ex.InnerException?.Message,
                ActionMessage = "Go to login",
                Path = "Index"
            };

            return View("ErrorHandler");
        }
    }
}