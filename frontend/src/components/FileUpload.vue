<script setup lang="ts">
import { type Ref, ref } from 'vue';
import FileDisplay from './FileDisplay.vue';

const filelist: Ref<File[]> = ref([]);
const extentions: string[] = ['.png', '.jpg', '.jpeg'];

function getFiles(): File[] {
    return filelist.value;
}

function getExtention(filename: string): string {
    return filename.substring(filename.lastIndexOf('.'), filename.length) || filename;
}

function onDrop(event: DropEvent): void {
    for (const file of event.dataTransfer.files) {
        const filename = file.name;
        const extention: string = getExtention(filename);

        if (!extentions.includes(extention)) {
            alert(`${extention} is not allowed`);
            return;
        }

        const existsFile: boolean = filelist.value.findIndex((file: File): boolean => {
            return file.name == filename;
        }) > -1;

        if (existsFile)
            return;

        filelist.value.push(file);
    }
}
</script>

<template>
    <label
        @drop.prevent="onDrop"
        @dragenter.prevent
        @dragover.prevent
    >
        <input multiple type="file" accept=".png, .jpg, .jpeg">

        <p class="extentions">
            {{ extentions.join(' ') }}
        </p>
        <p>drop your files here</p>
        <p>or</p>
        <p>click to browse</p>
    </label>

    <FileDisplay
        v-for="(file, index) in filelist"
        v-bind:name="file.name"
        @remove="filelist.splice(index, 1)"
    />
</template>

<style scoped>
@import '@/assets/base.css';

label {
    display: flex;
    align-items: center;
    flex-direction: column;

    border-radius: var(--border-radius);
    border: 2px solid var(--border-color);
    margin: 16px 8px;
    padding: 4px;
    background: transparent;
    color: var(--font);
    font-size: 24px;
}

input {
    visibility: hidden;
    position: absolute;
}

.extentions {
    margin: 16px 0px;
}

p {
    margin: 4px 0px;
    padding: 0px;
}
</style>
