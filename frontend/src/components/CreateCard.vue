<script setup lang="ts">
import { type Ref, ref } from 'vue';
import FileUpload from './FileUpload.vue';
import { useTemplateRef } from 'vue';
import { Host } from '@/helpers/Host';

const isVisibleCreate: Ref<boolean> = ref(false);
const createModal: Ref<HTMLElement | null> = useTemplateRef<HTMLElement>('createModal');

const result: Ref<{
    status: boolean,
    message: string
}> = ref({ status: false, message: '' });

const nameRef: Ref<HTMLInputElement | null> = useTemplateRef<HTMLInputElement>('name');
const addressRef: Ref<HTMLInputElement | null> = useTemplateRef<HTMLInputElement>('address');
const descriptionRef: Ref<HTMLInputElement | null> = useTemplateRef<HTMLInputElement>('description');
const fileUploadRef: Ref<typeof FileUpload | null> = useTemplateRef<typeof FileUpload>('fileUpload');

function onBgClick(event: MouseEvent): void {
    if (!createModal.value?.contains(event.target as Node))
        isVisibleCreate.value = false;
}

async function submit(): Promise<void> {
    const filelist: File[] = fileUploadRef.value.getFiles();

    const name: string = nameRef.value?.value!;
    const address: string = addressRef.value?.value!;
    const description: string = descriptionRef.value?.value!;

    const formData: FormData = new FormData();

    for (const file of filelist)
        formData.append('Files', file);

    formData.append('Name', name);
    formData.append('Address', address);
    formData.append('Description', description);

    const opts: RequestInit = {
        method: 'POST',
        mode: 'cors',
        credentials: 'include',
        body: formData
    };

console.log(formData);

    const res = await fetch(`${Host.getHost()}/EstateObject/Create`, opts);

    switch (res.status)
    {
    case 201:
        result.value.message = 'success';
        result.value.status = true;
        return;
    case 413:
        result.value.message = 'one of your files is too big';
        result.value.status = false;
        return;
    case 415:
        result.value.message = 'one of your filetypes is not supported';
        result.value.status = false;
        return;
    default:
        result.value.message = 'unexpected error occured';
        result.value.status = false;
    }
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

        <form
            ref="createModal"
            class="auth-wrapper background resizable"
            @submit.prevent="submit"
        >
            <h1 class="title">create a new estate object</h1>

            <input
                ref="name"
                class="action-field"
                name="name"
                type="text"
                placeholder="name"
                required
            >
            <input
                ref="address"
                class="action-field"
                name="address"
                type="text"
                placeholder="address"
                required
            >

            <textarea
                ref="description"
                name="description"
                placeholder="description"
            />

            <FileUpload ref="fileUpload"/>

            <button
                @submit.prevent="submit"
                type="submit"
                class="action-field"
            >
                create
            </button>

            <a
                class="status-display"
                :class="result.status ? 'success-message' : 'error-message'"
            >
                {{ result.message }}
            </a>
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

.status-display {
    text-align: center;
}
</style>
