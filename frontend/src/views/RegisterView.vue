<script setup>
import { ErrorHandler } from '@/helpers/ErrorHandler';
import { useUserDataStore } from '@/stores/userdata';
import { onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { Host } from '@/helpers/Host';
import { EMPTY_PFP } from '@/helpers/User';

const userstore = useUserDataStore();
const router = useRouter();

let errorHandler = null;
let authWrapper = null;

onMounted(() => {
    authWrapper = document.getElementById('auth-wrapper');
    const errorDisplay = document.getElementById('error-display');
    errorHandler = new ErrorHandler(errorDisplay);
});

async function register() {
    errorHandler.hideErrors();

    const loginInput = document.getElementById('login');
    const login = loginInput.value;
    const passwordInput = document.getElementById('password');
    const password = passwordInput.value;
    const verifyPasswordInput = document.getElementById('verify-password');
    const verifyPassword = verifyPasswordInput.value;
    const firstName = document.getElementById('first-name').value;
    const middleName = document.getElementById('middle-name').value;
    const lastName = document.getElementById('last-name').value;
    const sex = document.getElementById('male').checked;
    const role = document.getElementById('role').value;

    if (login.length < 4) {
        errorHandler.displayError('login can not subceed 4 characters', [loginInput]);
        return;
    }

    if (password.length < 4) {
        errorHandler.displayError('password can not subceed 4 characters', [passwordInput]);
        return;
    }

    if (password !== verifyPassword) {
        errorHandler.displayError('passwords does not match', [passwordInput, verifyPasswordInput]);
        return;
    }

    const user = {
        login: login,
        password: password,
        firstName: firstName,
        middleName: middleName,
        lastName: lastName,
        pfpPath: EMPTY_PFP,
        sex: sex,
        role: role
    };

    let params = {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    };

    try {
        let res = await fetch(`${Host.getHost()}/User/register`, params);

        console.log(res.status, res.ok);

        switch (res.status) {
            case 201:
                router.push('/');
                userstore.init(user);
                userstore.isLogged = true;
                break;
            case 409:
                errorHandler.displayError('user with such login already exists', [authWrapper]);
                break;
            default:
                let body = res.json();
                errorHandler.displayError(body.error, [authWrapper]);
        }
    } catch (error) {
        errorHandler.displayError('unexpected error occured', [authWrapper]);
    }
}
</script>

<template>
    <div class="page">
        <div class="spacer"></div>

        <form v-on:submit.prevent="register" id="auth-wrapper" class="auth-wrapper">
            <h1 class="title">register</h1>

            <input id="login" class="action-field" name="login" type="text" placeholder="login" required>
            <input id="first-name" class="action-field" name="first-name" type="text" placeholder="first name" required>
            <input id="middle-name" class="action-field" name="middle-name" type="text" placeholder="middle name" required>
            <input id="last-name" class="action-field" name="last-name" type="text" placeholder="last name" required>
            <input id="password" class="action-field" name="password" type="password" placeholder="password" required>
            <input id="verify-password" class="action-field" name="verify-password" type="password" placeholder="verify password" required>

            <div class="sex-picker">
                <input id="male" type="radio" name="sex" checked>
                <label for="male">male</label>
                <input id="female" type="radio" name="sex">
                <label for="female">female</label>
            </div>

            <div class="dropdown-container">
                <label for="role">role</label>
                <select name="role" id="role">
                    <option value="engineer">engineer</option>
                    <option value="manager">manager</option>
                    <option value="supervisor">supervisor</option>
                </select>
            </div>

            <button type="submit" class="action-field">register</button>

            <p id="error-display" class="error-display"></p>
        </form>
    </div>
</template>

<style src="../assets/form.css" scoped>
</style>

<style scoped>
.spacer {
    height: 14vh;
}

.sex-picker {
    display: flex;
    justify-content: center;
    align-items: center;
}

input[type="radio"] {
    accent-color: white;
    background: white;
    width: 15px;
    height: 15px;
}

label {
    font-size: 24px;
    color: white;
}
</style>
