<script setup lang="ts">
import { defineProps, getCurrentInstance, type ComponentInternalInstance, computed, onMounted } from 'vue';
import { type Position, next, prev } from './Carousel.vue';

const props = defineProps<{
    current: number,
    length: number
}>();

const instance: ComponentInternalInstance | null = getCurrentInstance();

const position = computed((): Position => {
    const index: PropertyKey = instance?.vnode.key!;

    if (index === props.current)
        return 'current';
    else if (index === prev(props.current, props.length))
        return 'prev';
    else if (index === next(props.current, props.length))
        return 'next';

    return 'hidden';
});
</script>

<template>
    <div
        class="carousel-item"
        :class="position"
    >
        <slot />
    </div>
</template>

<style scoped>
.carousel-item {
    position: absolute;
    display: flex;
    height: 50vh;
}

.prev {
    right: 64%;
    height: 30vh;
}

.current {
    position: static;
    z-index: 1;
}

.next {
    left: 64%;
    height: 30vh;
}

.hidden {
    border: 2px solid white;
    visibility: hidden;
}
</style>
