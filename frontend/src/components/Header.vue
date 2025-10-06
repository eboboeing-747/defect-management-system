<script setup lang="ts">
import { useUserDataStore } from '@/stores/userdata';
import { type Ref, ref } from 'vue';
import { onMounted } from 'vue';
import { useRouter, type Router } from 'vue-router';
import { Logout } from '@/helpers/Logout';

const userdata = useUserDataStore();
const router: Router = useRouter();

const isVisibleAccount: Ref<boolean> = ref(false);

function handleAccountModal(event: MouseEvent): void {
    if (!isVisibleAccount.value)
        return;

    let accountModal: HTMLElement | null = document.getElementById('account-modal');

    if (accountModal === null) {
        console.log('[handleAccountModal] accountModal is null');
        return;
    }

    if (!accountModal.contains(event.target as Node))
        isVisibleAccount.value = false;
}

onMounted((): void => {
    window.addEventListener('click', handleAccountModal, true);
});
</script>

<template>
    <header>
        <div>

        </div>

        <div class="account-info">
            <div v-if="userdata.isLogged">{{userdata.firstName}} {{userdata.middleName}}</div>
            <a v-else>Вы не зарегестрированы</a>

            <div class="spacer">
            </div>

            <img
                v-on:click="isVisibleAccount = !isVisibleAccount"
                v-bind:src="userdata.pfpPath"
                class="pfp focusable"
                alt="изображение профиля"
            >
        </div>
    </header>



    <div
        id="account-modal"
        class="account-modal"
        v-if="isVisibleAccount"
    >
        <div
            v-if="userdata.isLogged"
            class="account-action-list"
        >
            <button class="account-action">
                Редактировать профиль
            </button>
            <button
                class="account-action"
                @click="Logout()"
            >
                Выйти
            </button>
        </div>

        <div
            v-else
            class="account-action-list"
        >
            <button
                class="account-action"
                @click="router.push('/login');"
            >
                Войти
            </button>
            <button
                class="account-action"
                @click="router.push('/register');"
            >
                Зарегистрироваться
            </button>
        </div>
    </div>
</template>

<style scoped>
header {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
}

.account-info {
    display: flex;
    flex-direction: row;
    align-items: center;
}

.spacer {
    width: 16px;
}

.pfp {
    width: 50px;
    height: 50px;
}

.account-modal {
    border: 1px solid red;
    position: absolute;
    top: 5vh;
    right: 5vw;
    padding: 10px;
}

.account-action-list {
    display: flex;
    flex-direction: column;
}

.account-action {
    border: 1px solid blue;
    padding: 10px;
    background-color: transparent;
}

.account-action:hover {
    background-color: red;
}
</style>
