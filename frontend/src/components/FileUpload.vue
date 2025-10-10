<script setup lang="ts">
import { type Ref, ref } from 'vue';
import FileDisplay from './FileDisplay.vue';

const filelist: Ref<File[]> = ref([]);

function getFiles(): File[] {
    return filelist.value;
}

function onDrop(event: DropEvent): void {
    for (const file of event.dataTransfer.files) {
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
        <p>drop your files here</p>
        <p>or</p>
        <button>browse</button>
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
    padding: 10px 16px;
    margin: 16px 8px;
    background: transparent;
    color: var(--font);
    font-size: 24px;
}

input {
    visibility: hidden;
}
</style>
