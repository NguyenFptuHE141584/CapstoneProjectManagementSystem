var googleUser = {};
var startApp = function () {
    gapi.load('auth2', function () {
        // Retrieve the singleton for the GoogleAuth library and set up the client.
        auth2 = gapi.auth2.init({
            client_id: '423028066984-496qol9ghqhb9dfphlio2p8oo7ak146k.apps.googleusercontent.com',
            cookiepolicy: '/google-response',
            // Request scopes in addition to 'profile' and 'email'
            scope: 'profile email openid'
        });
        attachSignin(document.getElementById('signinGg'));
    });
};
function attachSignin(element) {

    auth2.attachClickHandler(element, {},
        function (googleUser) {

            var id_token = googleUser.getAuthResponse().id_token;
            window.location.href = "/ExternalSignIn/SignInGoogle?returnUrl=@ViewBag.ReturnUrl&token=" + id_token;
        }, function (error) {
            console.log(JSON.stringify(error, undefined, 2));
        });
}