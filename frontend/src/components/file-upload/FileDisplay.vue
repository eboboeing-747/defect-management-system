<script setup lang="ts">
const emit = defineEmits<{
    remove: []
}>();

const props = defineProps<{
    name: string
}>();

</script>

<template>
    <div class="file-display">
        <div class="file-name">
            {{ props.name }}
        </div>

        <button
            type="button"
            class="remove"
            @click="emit('remove')"
        >
            <img
                src="/trash-bin-dark.png"
                alt="remove"
            >
        </button>
    </div>
</template>

<style scoped>
@import '@/assets/base.css';

.file-display {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;

    padding: 10px 16px;
    margin: 0px 8px;
    background: transparent;
    color: var(--font);
    font-size: 24px;
}

.file-name {
    overflow-wrap: anywhere;
}

.remove {
    display: flex;
    justify-content: space-between;
    align-items: center;
    background-color: transparent;

    border: 1px solid transparent;
}

.remove:hover {
    border: 1px solid red;
    background-color: lightcoral;
    border-radius: var(--border-radius);
}

img {
    height: 30px;
    width: 30px;
}
</style>
