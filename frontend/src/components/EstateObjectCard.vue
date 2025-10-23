<script setup lang="ts">
import { type EstateObjectCard } from '@/helpers/EstateObject';
import { Host } from '@/helpers/Host';

const props = defineProps<{
    object: EstateObjectCard
}>();
</script>

<template>
    <div class="body">
        <img
            class="card-image"
            :src="`${Host.getHost()}/Image/GetImage/${props.object.imagePath}`"
            :alt="props.object.imagePath"
        />
        <div class="card-field">
            {{ props.object.name }}
        </div>
        <div class="card-field">
            {{ props.object.address }}
        </div>
    </div>
</template>

<style scoped>
@import '@/assets/card.css';
</style>
