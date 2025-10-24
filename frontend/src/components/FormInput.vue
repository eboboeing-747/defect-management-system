<script setup lang="ts">
import { defineProps, ref, type Ref, computed } from 'vue';
defineOptions({
  inheritAttrs: false
})
const props = defineProps<{
    isRequired: boolean,
    minLength?: number,
    name: string,
}>();

const inputValue = defineModel<string>();

interface IError {
    present: boolean,
    message: string
}

const error = computed((): IError => {
    if (!props.minLength)
        return {
            present: false,
            message: ''
        };

    const errorMessage: string = `${props.name} should at least be ${props.minLength} symbols`;

    if (!inputValue.value)
        return { present: true, message: errorMessage};

    const len: number = inputValue.value.length;
    const hasError: boolean = len < props.minLength;
    return {
        present: hasError,
        message: hasError ? errorMessage : ''
    };
})

function getValue(): string | null {
    return inputValue.value ?? null;
}

defineExpose({ error, getValue });
</script>

<template>
    <div
        class="error-message"
    >
        {{ error.message }}
    </div>

    <input
        v-bind:="$attrs"
        v-model="inputValue"
        :required="props.isRequired"
        :class="{ error: error.present }"
        :name="name"
        class="action-field"
    >
</template>

<style scoped>
@import '@/assets/form.css';
</style>
