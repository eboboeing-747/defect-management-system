<script setup lang="ts">
import { type Ref, ref } from 'vue';
import { useTemplateRef } from 'vue';

const isVisibleCreate: Ref<boolean> = ref(false);
const createModal: Ref<HTMLElement | null> = useTemplateRef<HTMLElement>('createModal');

function onBgClick(event: MouseEvent): void {
    if (!createModal.value?.contains(event.target as Node))
        isVisibleCreate.value = false;
}

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
        @click="onBgClick"
    >
        <div class="spacer"></div>

        <div
            ref="createModal"
            class="auth-wrapper background resizable"
        >
            <slot />
        </div>
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
</style>
