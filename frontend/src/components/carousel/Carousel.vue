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
            class="prev scroll-button"
            @click="current = prev(current, length)"
        >
            &lt;
        </button>

        <button
            class="next scroll-button"
            @click="current = next(current, length)"
        >
            &gt;
        </button>
    </div>
</template>

<style scoped>
@import '@/assets/base.css';

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
    font-size: 24px;
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
