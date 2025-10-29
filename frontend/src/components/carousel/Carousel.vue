<script setup lang="ts">
import { computed, ref, type ComponentInternalInstance, getCurrentInstance, type Ref, onMounted } from 'vue';

export type Position = 'current' | 'next' | 'prev' | 'hidden';

const current: Ref<number> = ref(0);
const length: Ref<number> = ref(0);

onMounted((): void => {
    length.value = document.querySelectorAll('.carousel-item').length;
})
</script>

<script lang="ts">
export function prev(current: number, length: number): number {
    if (current <= 0)
        return length - 1;

    return current - 1;
}

export function next(current: number, length: number): number {
    if (current >= length - 1)
        return 0;

    return current + 1;
}

</script>

<template>
    <div class="carousel">
        <slot
            :current="current"
            :length="length"
        />



        <button
            v-if="length >= 2"
            class="prev scroll-button nf nf-fa-chevron_left"
            @click="current = prev(current, length)"
        >
        </button>

        <button
            v-if="length >= 2"
            class="next scroll-button nf nf-fa-chevron_right"
            @click="current = next(current, length)"
        >
        </button>
    </div>
</template>

<style scoped>
@import '@/assets/base.css';
@import 'https://www.nerdfonts.com/assets/css/webfont.css';

.carousel {
    position: relative;
    display: flex;
    align-items: center;
}

.scroll-button {
    position: absolute;
    height: 50px;
    width: 50px;
    border-radius: var(--border-radius);
    border: 2px solid var(--font);
    color: var(--font);
    background-color: var(--background);
    font-size: 16px;
    text-align: center;
    z-index: 1;
}

.prev {
    left: 10%;
}

.next {
    right: 10%;
}
</style>
