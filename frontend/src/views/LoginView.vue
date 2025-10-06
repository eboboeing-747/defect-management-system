<script setup>
import { useRouter } from 'vue-router';
import { ErrorHandler } from '@/helpers/ErrorHandler';
import { useUserDataStore } from '@/stores/userdata';
import { onMounted } from 'vue';
import { Host } from '@/helpers/Host';

const userstore = useUserDataStore();
const router = useRouter();

let errorHandler = null;
let authWrapper = null;

onMounted(() => {
    authWrapper = document.getElementById('auth-wrapper');
    const errorDisplay = document.getElementById('error-display');
    errorHandler = new ErrorHandler(errorDisplay);
});

async function login() {
    errorHandler.hideErrors();

    let usernameInput = document.getElementById('username');
    let username = usernameInput.value;
    let passwordInput = document.getElementById('password');
    let password = passwordInput.value;

    console.log(`[login] ${username} ${password}`);

    if (username.length < 4) {
        errorHandler.displayError('username can not subceed 4 characters', [usernameInput]);
        return;
    }

    if (password.length < 4) {
        errorHandler.displayError('password can not subceed 4 characters', [passwordInput]);
        return;
    }

    let params = {
        method: 'POST',
        mode: 'cors',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            login: username,
            password: password
        })
    }

    try {
        const res = await fetch(`${Host.getHost()}/User/Login`, params);

        switch (res.status) {
            case 200:
                const body = await res.json();
                userstore.init(body);
                userstore.isLogged = true;
                router.push('/');
                console.log('[login] logged', body);
                return;
            case 401:
                errorHandler.displayError('login or password is incorrect', [usernameInput, passwordInput]);
                return;
        }
    } catch(error) {
        console.log(error);
        errorHandler.displayError('failed to rich the server', [authWrapper]);
    }
}
</script>

<template>
    <div class="page">
        <div class="spacer"></div>

        <form v-on:submit.prevent="login" id="auth-wrapper" class="auth-wrapper">
            <h1 class="title">login</h1>

            <input id="username" class="action-field" name="username" type="text" placeholder="username" required>
            <input id="password" class="action-field" name="password" type="password" placeholder="password" required>

            <button class="action-field">login</button>

            <a class="title" href="/register">dont have an account? register</a>
            <p id="error-display" class="error-display"></p>
        </form>
    </div>
</template>

<style src="../assets/form.css" scoped>
</style>
