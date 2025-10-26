<script setup lang="ts">
import { useRoute, useRouter, type Router } from 'vue-router';
import { Host } from '@/helpers/Host';
import type { IEstateObject } from '@/helpers/EstateObject';
import { onMounted, ref, type Ref } from 'vue';
import Carousel from '@/components/carousel/Carousel.vue';
import CarouselItem from '@/components/carousel/CarouselItem.vue';

const router: Router = useRouter();

function getRouteParams(): string | void {
    const route = useRoute();
    const router: Router = useRouter();

    const id: string | string[] | undefined = route.params.id;

    if (typeof id === 'string')
        return id;
    
    router.push('/');
}

const id: string = getRouteParams()!;

async function GetEstateObject(): Promise<IEstateObject | null> {
    const opts: RequestInit = {
        method: 'GET',
        mode: 'cors',
        credentials: 'include'
    };

    try {
        const res: Response = await fetch(`${Host.getHost()}/EstateObject/Get/${id}`, opts);

        switch (res.status) {
        case 200:
            const body = await res.json();
            return body;
        case 404:
            router.push('/');
            return null;
        }
    } catch(error: unknown) {
        console.log(error);
    }

    return null;
}

const estateObject: Ref<IEstateObject | null> = ref(null);

onMounted(async () => {
    estateObject.value = await GetEstateObject();
});
</script>

<template>
    <div class="align">
        <h1>
            {{ estateObject?.name }}
        </h1>

        <div
            v-if="estateObject !== null"
        >
            <Carousel v-slot="props">
                <CarouselItem
                    :current="props.current"
                    :length="props.length"
                    v-for="(image, index) in estateObject?.images"
                    :key="index"
                >
                    <img
                        :src="`${Host.getHost()}/Image/GetImage/${image}`"
                        class="image"
                    />
                </CarouselItem>
            </Carousel>
        </div>

        <div v-else>
            loading content...
        </div>

        <h2>{{ estateObject?.address }}</h2>

        <p class="description">{{ estateObject?.description }}</p>
    </div>
</template>

<style scoped>
@import '@/assets/base.css';

.image {
    object-position: center center;
    aspect-ratio: 4 / 3;
    object-fit: cover;

    margin: 20px;
    border: 2px solid var(--border-color);
    border-radius: var(--border-radius);
}

.imagelist {
    display: flex;
    flex-direction: row;
}

.description {
    white-space: pre-wrap;
}

.align {
    display: flex;
    flex-direction: column;
    align-items: center;
}
</style>
