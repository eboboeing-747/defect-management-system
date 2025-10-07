<script setup lang="ts">
import { type IEstateObjectCard } from '@/helpers/EstateObject';

const props = defineProps<{
    object: IEstateObjectCard
}>();
</script>

<template>
    <div class="body">
        <img
            class="card-image"
            v-bind:src="props.object.image"
        />
        <div>
            {{ props.object.name }}
        </div>
        <div>
            {{ props.object.address }}
        </div>
    </div>
</template>

<style scoped>
@import '@/assets/card.css';
</style>
