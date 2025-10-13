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

/*
const cards: Ref<IEstateObjectCard[]> = ref([
    {
        image: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTt2fAoCstnaitEc1r28oTgziMEIY9HJHr-I6cKWizXtBBZJVC0FxVsUbXwUmkWSwmejZY&usqp=CAU",
        name: "lmao jshk",
        address: "Groove st."
    },
    {
        image: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTt2fAoCstnaitEc1r28oTgziMEIY9HJHr-I6cKWizXtBBZJVC0FxVsUbXwUmkWSwmejZY&usqp=CAU",
        name: "jshk top 1",
        address: "fine square"
    },
    {
        image: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTt2fAoCstnaitEc1r28oTgziMEIY9HJHr-I6cKWizXtBBZJVC0FxVsUbXwUmkWSwmejZY&usqp=CAU",
        name: "lmao jshk",
        address: "Groove st."
    },
    {
        image: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTt2fAoCstnaitEc1r28oTgziMEIY9HJHr-I6cKWizXtBBZJVC0FxVsUbXwUmkWSwmejZY&usqp=CAU",
        name: "residential complex fine idk",
        address: "time square"
    },
    {
        image: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTt2fAoCstnaitEc1r28oTgziMEIY9HJHr-I6cKWizXtBBZJVC0FxVsUbXwUmkWSwmejZY&usqp=CAU",
        name: "apartment complex xxx",
        address: "#1 street in the city"
    },
    {
        image: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTt2fAoCstnaitEc1r28oTgziMEIY9HJHr-I6cKWizXtBBZJVC0FxVsUbXwUmkWSwmejZY&usqp=CAU",
        name: "lmao jshk",
        address: "Groove st."
    },
]);
*/

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
