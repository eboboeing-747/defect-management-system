<script setup lang="ts">
import { type Ref, ref, onMounted } from 'vue';
import { type IEstateObjectCard } from '@/helpers/EstateObject';
import { Host } from '@/helpers/Host';
import EstateObjectCard from './estate-object/EstateObjectCard.vue';
import CreateCard from './CreateCard.vue';
import { useUserDataStore } from '@/stores/userdata';
import { Role } from '@/helpers/User';
import CreateEstateObject from './estate-object/CreateEstateObject.vue';
import { useRouter, type Router } from 'vue-router';

const userdata = useUserDataStore();
const router: Router = useRouter();
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
        const res: Response = await fetch(`${Host.getHost()}/EstateObject/GetAll`, opts);
        
        switch (res.status) {
        case 200:
            const body = await res.json();
            console.log(body);
            return body;
        case 404:
            alert('failed to rich the server');
            return null;
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
            <CreateCard
                v-if="userdata.role == Role.MANAGER"
            >
                <CreateEstateObject />
            </CreateCard>

            <EstateObjectCard
                v-for="card in cards"
                v-bind:object="card"
                @click="router.push(`/estate-object/${card.id}`)"
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
