<script setup lang="ts">
import { useRouter } from 'vue-router';
import { useUserDataStore } from '@/stores/userdata';
import { Host } from '@/helpers/Host';
import { ref, type Ref } from 'vue';
import FormInput from '@/components/FormInput.vue';
import { type IUser, type IUserCredentials } from '@/helpers/User';
import { useTemplateRef } from 'vue';

const userstore = useUserDataStore();
const router = useRouter();
const login: Ref<typeof FormInput | null> = useTemplateRef<typeof FormInput>('login');
const password: Ref<typeof FormInput | null> = useTemplateRef<typeof FormInput>('password');
const errorMessage: Ref<string> = ref('');

async function logIn(): Promise<void> {
    if (login.value?.error.present)
        return;

    const userCreds: IUserCredentials = {
        login: login.value?.getValue(),
        password: password.value?.getValue()
    };

    const params: RequestInit = {
        method: 'POST',
        mode: 'cors',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userCreds)
    }

    try {
        const res: Response = await fetch(`${Host.getHost()}/User/Login`, params);

        switch (res.status) {
        case 200:
            const body: IUser = await res.json();
            userstore.LogIn(body);
            router.push('/');
            console.log('[logIn] logged', body);
            return;
        case 401:
            errorMessage.value = 'login or password is incorrect';
            return;
        }
    } catch(error) {
        console.log(error);
        errorMessage.value = 'failed to rich the server';
    }
}
</script>

<template>
    <div class="page">
        <div class="spacer"></div>

        <form v-on:submit.prevent="logIn" id="auth-wrapper" class="auth-wrapper">
            <h1 class="title">log in</h1>

            <FormInput
                ref="login"
                :is-required="true"
                :min-length="4"
                placeholder="login"
                name="login"
                type="text"
            />

            <FormInput
                ref="password"
                :is-required="true"
                :min-length="4"
                placeholder="password"
                name="password"
                type="password"
            />

            <button class="action-field">log in</button>

            <a class="title" href="/register">dont have an account? register</a>
            <p class="error-message">{{ errorMessage }}</p>
        </form>
    </div>
</template>


<style scoped>
.spacer {
    height: 20vh;
}
</style>

<style src="@/assets/form.css" scoped>
</style>
