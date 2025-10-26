<script setup lang="ts">
import { type Ref, ref, useTemplateRef } from 'vue';
import FileUpload from '@/components/file-upload/FileUpload.vue';
import { Host } from '@/helpers/Host';

const nameRef: Ref<HTMLInputElement | null> = useTemplateRef<HTMLInputElement>('name');
const addressRef: Ref<HTMLInputElement | null> = useTemplateRef<HTMLInputElement>('address');
const descriptionRef: Ref<HTMLInputElement | null> = useTemplateRef<HTMLInputElement>('description');
const fileUploadRef: Ref<typeof FileUpload | null> = useTemplateRef<typeof FileUpload>('fileUpload');

const result: Ref<{
    status: boolean,
    message: string
}> = ref({
    status: false,
    message: ''
});

async function submit(): Promise<void> {
    const filelist: File[] = fileUploadRef.value!.getFiles();

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

    try {
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
    } catch(error: unknown) {
        console.log(error);
    }
}

</script>

<template>
    <form
        @submit.prevent="submit"
    >
        <h1 class="title">create a new estate object</h1>

        <input
            ref="name"
            name="name"
            type="text"
            placeholder="name"
            required
        >

        <input
            ref="address"
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

        <FileUpload ref="fileUpload" />

        <button
            @submit.prevent="submit"
            type="submit"
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
</template>

<style scoped>
@import '@/assets/card.css';
@import '@/assets/form.css';

form {
    display: flex;
    flex-direction: column;
}
</style>
