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
        <!--
        <div>{{ position + ' ' + props.current  + ' ' + instance?.vnode.key?.toString() }}</div>
        -->

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
    right: 450px;
    border: 2px solid red;
    height: 30vh;

    /* visibility: hidden; */
}

.current {
    position: static;
    border: 2px solid yellow;
    z-index: 1;
}

.next {
    left: 450px;
    border: 2px solid green;
    height: 30vh;

    /* visibility: hidden; */
}

.hidden {
    border: 2px solid white;
    visibility: hidden;
}
</style>
