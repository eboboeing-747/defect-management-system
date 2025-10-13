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

    const accountModal: HTMLElement | null = document.getElementById('account-modal');

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
            {{ userdata.role }}
        </div>

        <div class="account-info">
            <div v-if="userdata.isLogged">
                {{userdata.firstName}} {{userdata.middleName}}
            </div>
            <a v-else class="username">
                not logged in
            </a>

            <div class="spacer">
            </div>

            <img
                v-on:click="isVisibleAccount = !isVisibleAccount"
                v-bind:src="userdata.pfpPath"
                class="pfp focusable"
                alt="profile picture"
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
                edit profile
            </button>
            <button
                class="account-action"
                @click="Logout()"
            >
                log out
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
                log in
            </button>
            <button
                class="account-action"
                @click="router.push('/register');"
            >
                sign up
            </button>
        </div>
    </div>
</template>

<style scoped>
@import '@/assets/base.css';

header {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    padding: 10px;
    align-items: center;
}

.account-info {
    display: flex;
    flex-direction: row;
    align-items: center;
}

.username {
    color: var(--font);
}

.spacer {
    width: 16px;
}

.pfp {
    width: 50px;
    height: 50px;
}

.account-modal {
    border-radius: var(--border-radius);
    border: 1px solid var(--border-color);
    background: var(--background);
    position: absolute;
    top: 8vh;
    right: 2vw;
}

.account-action-list {
    display: flex;
    flex-direction: column;
}

.account-action {
    border: none;
    color: var(--font);
    font-size: var(--font-size);
    padding: 14px;
    background-color: transparent;
}

.account-action:hover {
    background-color: var(--border-color);
}
</style>
