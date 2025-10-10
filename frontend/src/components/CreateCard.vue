<script setup lang="ts">
import { type Ref, ref } from 'vue';
import FileUpload from './FileUpload.vue';

const isVisibleCreate: Ref<boolean> = ref(false);

window.addEventListener('keydown', (event: KeyboardEvent): void => {
    if (event.key == 'Escape')
        isVisibleCreate.value = false;
});
</script>

<template>
    <div
        class="body create-card"
        @click="isVisibleCreate = !isVisibleCreate"
    >
        +
    </div>

    <div
        v-show="isVisibleCreate"
        class="create-modal-bg"
    >
        <div class="spacer"></div>

        <form id="auth-wrapper" class="auth-wrapper background resizable">
            <h1 class="title">create a new estate object</h1>

            <input class="action-field" name="name" type="text" placeholder="name" required>
            <input class="action-field" name="address" type="text" placeholder="address" required>

            <textarea name="description" placeholder="description" />

            <FileUpload />

            <button class="action-field">create</button>

            <p id="error-display" class="error-display"></p>
        </form>
    </div>
</template>

<style scoped>
@import '@/assets/card.css';
@import '@/assets/form.css';

.create-card {
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 100px;
}

.create-modal-bg {
    display: flex;
    justify-content: center;
    align-items: center;
    position: absolute;
    top: 0;
    right: 0;
    margin: 0px;
    border-width: 0px;
    width: 100vw;
    height: 100vh;
    background: rgba(0, 0, 0, 0.5);
    overflow: scroll;
    scrollbar-width: none;
}

.spacer {
    height: 20vh;
}

.background {
    background-color: var(--background);
}

.resizable {
    resize: both;
    overflow: scroll;
    scrollbar-width: none;
    margin: 500px 0px;
}

textarea {
    resize: vertical;
}

</style>
