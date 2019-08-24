function login() {
    let username = $('#username-login').val();
    let password = $('#password-login').val();

    if (username === '' || password === '') {
        alert("Username and password, cannot be empty");
        return;
    }

    $('#username-login').val('');
    $('#password-login').val('');

    let requestBody = {
        username: username,
        password: password
    };

    $.post({
        url: APP_SERVICE_URL + 'users/login',
        headers:{
            "Content-Type":"application/json"
        },
        data: JSON.stringify(requestBody),
        success: function (data) {
            hideGuestNavbar();
            $('#caption').text('Welcome to Chat-Inc!');
            let token = data.rawHeader
            + '.'
            + data.rawPayload
            + '.'
            + data.rawSignature;

            saveToken(token);

            $("#username-logged-in").text(getUser());

            hideLoginAndRegisterAndShowLoggedInData();
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function register() {
    let username = $('#username-register').val();
    let password = $('#password-register').val();

    $('#username-register').val('');
    $('#password-register').val('');

    let requestBody = {
        username: username,
        password: password
    };

    $.post({
        url: APP_SERVICE_URL + 'users/register',
        data: JSON.stringify(requestBody),
        headers:{
          "Content-Type":"application/json"
        },
        success: function (data) {
            toggleLogin();
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function toggleLogin() {
    $('#login-data').show();
    $('#register-data').hide();
}

function toggleRegister() {
    $('#login-data').hide();
    $('#register-data').show();
}

function hideGuestNavbar() {
    $("#guest-navbar")
        .removeClass("d-block")
        .addClass("d-none");
}

function showGuestNavbar() {
    $("#guest-navbar")
        .removeClass("d-none")
        .addClass("d-block");
}

function hideLoginAndRegisterAndShowLoggedInData() {
    $('#login-data').hide();
    $('#register-data').hide();

    $('#logged-in-data').show();
}

function showLoginAndHideLoggedInData() {
    toggleLogin();

    $('#logged-in-data').hide();
}

function logout() {
    // TODO: Copy Functionality described in the Exercise
    $('#caption').text('Choose your username to begin chatting!');

    showGuestNavbar();
    showLoginAndHideLoggedInData();
}

function saveToken(token) {
    localStorage.setItem('auth_token', token);
}

function evictToken() {
    localStorage.removeItem('auth_token', token);
}

function getUser() {
    let token = localStorage.getItem('auth_token');

    let claims = token.split('.')[1];
    let decodedClaims = atob(claims);
    let parsedClaims = JSON.parse(decodedClaims);

    return parsedClaims.nameid;
}

function isLoggedIn() {
    return localStorage.getItem('auth_token') === null;
}


$('#logged-in-data').hide();
toggleLogin();