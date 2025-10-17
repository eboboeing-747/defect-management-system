<script setup lang="ts">
import { type Ref, ref, onMounted } from 'vue';
import { type IEstateObjectCard } from '@/helpers/EstateObject';
import { Host } from '@/helpers/Host';
import EstateObjectCard from './EstateObjectCard.vue';
import CreateCard from './CreateCard.vue';
import { useUserDataStore } from '@/stores/userdata';

const userdata = useUserDataStore();
const cards: Ref<IEstateObjectCard[]> = ref([]);

async function getCards(): Promise<IEstateObjectCard[] | null> {
    if (!userdata.isLogged)
        return null;

    const opts: RequestInit = {
        method: 'GET',
        mode: 'cors',
        credentials: 'include'
    };

    try {
        console.log('requesting');
        const res: Response = await fetch(`${Host.getHost()}/EstateObject/GetAll`, opts);
        
        switch (res.status) {
        case 200:
            const body = await res.json();
            console.log(body);
            return body;
        case 404:
            alert('failed to rich the server');
        default:
            return null;
        }
    } catch(error) {
        return null;
    }
}

onMounted(async () => {
    cards.value = await getCards() ?? [];
})

</script>

<template>
    <div
        class="estate-object-list"
        v-if="userdata.isLogged"
    >
        <div class="list-align">
            <CreateCard />
            <EstateObjectCard
                v-for="card in cards"
                v-bind:object="card"
            />
        </div>
    </div>
</template>

<style scoped>
.estate-object-list {
    display: flex;
    justify-content: center;
}

.list-align {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    justify-items: center;
    justify-content: space-between;
}
</style>
