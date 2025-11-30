<script setup lang="ts">
import { type IEstateObjectCard } from '@/helpers/EstateObject';
import { Host } from '@/helpers/Host';

const props = defineProps<{
    object: IEstateObjectCard
}>();
</script>

<template>
    <div class="body">
        <div class="card-image-wrap">
            <div
                class="no-image"
                v-if="props.object.imagePath === null"
            >
                this object has no images
            </div>

            <img
                v-else
                class="card-image"
                :src="`${Host.getHost()}/Image/GetImage/${props.object.imagePath}`"
                :alt="props.object.imagePath"
            />
        </div>

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

.no-image {
    width: 100%;
    text-align: center;
    font-size: 24px;
    padding: 5%;
    display: flex;
    align-items: center;
    justify-content: center;
}
</style>
