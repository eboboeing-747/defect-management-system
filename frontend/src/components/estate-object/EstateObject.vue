<script setup lang="ts">
import { useRoute, useRouter, type Router } from 'vue-router';
import { Host } from '@/helpers/Host';
import type { IEstateObject } from '@/helpers/EstateObject';
import { onMounted, ref, type Ref } from 'vue';
import Carousel from '@/components/carousel/Carousel.vue';
import CarouselItem from '@/components/carousel/CarouselItem.vue';
import CreateCard from '../CreateCard.vue';

const router: Router = useRouter();
const isCollapsedDescription: Ref<boolean> = ref(true);

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
            <Carousel
                v-slot="props"
                v-if="estateObject.images.length > 0"
            >
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

            <div
                v-else
                class="no-images"
            >
                this estate object has no images
            </div>
        </div>

        <div v-else>
            loading content...
        </div>

        <div style="margin-top: 16px">
            <i class="hint">
                address
            </i>

            <h2 class="address">
                {{ estateObject?.address }}
            </h2>
        </div>

        <div class="description-wrap">
            <div class="description-collapse-bar">
                <i class="hint">description</i>

                <button
                    class="expand-collapse-chevron nf"
                    :class="[isCollapsedDescription ? 'nf-fa-chevron_down' : 'nf-fa-chevron_up' ]"
                    @click="isCollapsedDescription = !isCollapsedDescription"
                >
                </button>
            </div>

            <p
                class="description"
                :class="{ 'description-collapse': isCollapsedDescription }"
            >
                {{ estateObject?.description }}
            </p>

            <i
                class="hint expand-collapse-text"
                @click="isCollapsedDescription = !isCollapsedDescription"
            >
                {{ isCollapsedDescription ? 'expand' : 'collapse' }}
            </i>
        </div>

        <div class="defect-list">
            <CreateCard />
        </div>
    </div>
</template>

<style scoped>
@import '@/assets/base.css';

.image {
    object-position: center center;
    aspect-ratio: 4 / 3;
    object-fit: cover;

    border: 2px solid var(--border-color);
    border-radius: var(--border-radius);
}

.no-images {
    text-align: center;
    font-size: 32px;
    padding: 8%;
    border: 2px solid var(--border-color);
    border-radius: var(--border-radius);
}

.imagelist {
    display: flex;
    flex-direction: row;
}

.expand-collapse-chevron {
    padding: 10px;
    border-radius: var(--border-radius);
    color: var(--font);
    background-color: transparent;
    border: none;
}

.expand-collapse-chevron:hover {
    background-color: var(--font);
    color: var(--background);
}

.expand-collapse-chevron:active {
    background-color: var(--background);
    color: var(--font);
}

.expand-collapse-text:hover {
    color: var(--font);
    cursor: pointer;
}

.expand-collapse-text:active {
    color: var(--border-color);
}

.description {
    overflow: hidden;
    white-space: pre-wrap;
    margin: 0;
}

.description-wrap {
    display: flex;
    flex-direction: column;
    margin: 16px;
}

.description-collapse {
    line-clamp: 8;
    display: -webkit-box;
    -webkit-box-orient: vertical;
    -webkit-line-clamp: 8;
}

.description-collapse-bar {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
}

.align {
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 50%;
}

.hint {
    color: var(--hint);
    margin: 10px 0px;
}

.address {
    margin: 0px;
}

.defect-list {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    width: 100%;
}
</style>
