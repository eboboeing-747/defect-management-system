<script setup lang="ts">
import { useRoute, useRouter, type Router } from 'vue-router';
import { Host } from '@/helpers/Host';
import type { EstateObject } from '@/helpers/EstateObject';
import { onMounted, ref, type Ref } from 'vue';

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

async function GetEstateObject(): Promise<EstateObject | null> {
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

const estateObject: Ref<EstateObject | null> = ref(null);

onMounted(async () => {
    estateObject.value = await GetEstateObject();
});
</script>

<template>
    <div>
        <h1>
            {{ estateObject?.name }}
        </h1>

        <div
            v-if="estateObject !== null"
            class="imagelist"
        >
            <img
                v-for="image in estateObject?.images"
                :key="image"
                :src="`${Host.getHost()}/Image/GetImage/${image}`"
                class="image"
            />
        </div>

        <div v-else>
            loading content...
        </div>

        <h2>{{ estateObject?.address }}</h2>

        <p class="description">{{ estateObject?.description }}</p>
    </div>
</template>

<style>

.image {
    max-height: 300px;
    max-width: 600px;
}

.imagelist {
    display: flex;
    flex-direction: row;
}

.description {
    white-space: pre-wrap;
}
</style>
