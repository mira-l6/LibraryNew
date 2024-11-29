
const loginSection = document.querySelector('.login-section');
const registerSection = document.querySelector('.register-section');

const loginButton = document.querySelector('#loginBtn');
const registerButton = document.querySelector('#registerBtn');

loginButton.addEventListener('click', signHandler);
registerButton.addEventListener('click', signHandler);

function signHandler(e) {

    console.log('hjehrehreh')

    if (e.currentTarget.getAttribute('id') == 'loginBtn') {

        showHideSection(loginSection, registerSection);
        loginButton.style.backgroundColor = '#d9d9d9';
        registerButton.style.backgroundColor = '#f0f0f0';

    } else if (e.currentTarget.getAttribute('id') == 'registerBtn') {

        showHideSection(registerSection, loginSection);
        registerButton.style.backgroundColor = '#d9d9d9';
        loginButton.style.backgroundColor = '#f0f0f0';
    }

}

function showHideSection(correct, hidden) {
    correct.style.display = 'block';
    hidden.style.display = 'none';
}