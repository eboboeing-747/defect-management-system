<script setup lang="ts">
import { useUserDataStore } from '@/stores/userdata';
import { type Ref, useTemplateRef, computed, ref } from 'vue';
import { useRouter } from 'vue-router';
import { Host } from '@/helpers/Host';
import FormInput from '@/components/FormInput.vue';
import type { UserRegister, User } from '@/helpers/User';
import { Role } from '@/helpers/User';

const roles: Role[] = Object.values(Role);
const conflict: Ref<boolean> = ref(false);

const userstore = useUserDataStore();
const router = useRouter();

const login: Ref<typeof FormInput | null> = useTemplateRef<typeof FormInput>('login');
const firstName: Ref<typeof FormInput | null> = useTemplateRef<typeof FormInput>('first-name');
const middleName: Ref<typeof FormInput | null> = useTemplateRef<typeof FormInput>('middle-name');
const lastName: Ref<typeof FormInput | null> = useTemplateRef<typeof FormInput>('last-name');
const password: Ref<typeof FormInput | null> = useTemplateRef<typeof FormInput>('password');
const verifyPassword: Ref<typeof FormInput | null> = useTemplateRef<typeof FormInput>('verify-password');
const sex: Ref<typeof HTMLInputElement | null> = useTemplateRef<typeof HTMLInputElement>('sex');
const role: Ref<typeof HTMLSelectElement | null> = useTemplateRef<typeof HTMLSelectElement>('role');

const errorMessage = computed((): string | null => {
    if (conflict.value)
        return 'user with such login already exists';

    if (password.value?.getValue() != verifyPassword.value?.getValue())
        return 'passwords does not match';

    return null;
})

async function register(): Promise<void> {
    if (errorMessage.value !== null)
        return;

    const userRegister: UserRegister = {
        login: login.value?.getValue(),
        firstName: firstName.value?.getValue(),
        middleName: middleName.value?.getValue(),
        lastName: lastName.value?.getValue(),
        password: password.value?.getValue(),
        sex: !sex.value?.checked,
        role: role.value?.value
    };

    const opts: RequestInit = {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userRegister)
    };

    try {
        const res: Response = await fetch(`${Host.getHost()}/User/Register`, opts);
        const user: User = await res.json();

        switch (res.status) {
            case 201:
                userstore.LogIn(user);
                router.push('/');
                break;
            case 409:
                conflict.value = true;
                break;
            default:
        }
    } catch(error: unknown) {
        console.log(error);
    }
}
</script>

<template>
    <div class="page">
        <div class="spacer"></div>

        <form v-on:submit.prevent="register" id="auth-wrapper" class="auth-wrapper">
            <h1 class="title">register</h1>

            <FormInput
                ref="login"
                :is-required="true"
                :min-length="4"
                placeholder="login"
                name="login"
                type="text"
            />

            <FormInput
                ref="first-name"
                :is-required="true"
                placeholder="first-name"
                name="first-name"
                type="text"
            />

            <FormInput
                ref="middle-name"
                :is-required="true"
                placeholder="middle-name"
                name="middle-name"
                type="text"
            />

            <FormInput
                ref="last-name"
                :is-required="true"
                placeholder="last-name"
                name="last-name"
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

            <FormInput
                ref="verify-password"
                :is-required="true"
                placeholder="verify-password"
                name="verify-password"
                type="password"
            />

            <div class="sex-picker">
                <input ref="sex" type="radio" name="sex" checked>
                <label for="male">male</label>
                <input type="radio" name="sex">
                <label for="female">female</label>
            </div>

            <div class="dropdown-container">
                <label for="role">role</label>
                <select name="role" ref="role" id="role">
                    <option
                        v-for="role in roles"
                    >
                        {{ role }}
                    </option>
                </select>
            </div>

            <button type="submit" class="action-field">register</button>

            <p
                v-if="errorMessage !== null"
                class="error-message"
            >{{ errorMessage }}</p>
        </form>
    </div>
</template>

<style src="@/assets/form.css" scoped>
</style>

<style scoped>
@import '@/assets/base.css';

.spacer {
    height: 14vh;
}

.sex-picker {
    display: flex;
    justify-content: center;
    align-items: center;
}

input[type="radio"] {
    accent-color: var(--background);
    width: 16px;
    height: 16px;
}

input[type="radio"]:checked {
    accent-color: var(--font);
}

label {
    font-size: 24px;
    color: white;
}
</style>
