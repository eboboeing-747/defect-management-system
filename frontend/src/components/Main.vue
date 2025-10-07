<script setup lang="ts">
import { type Ref, ref } from 'vue';
import { type IEstateObjectCard } from '@/helpers/EstateObject';
import EstateObjectCard from './EstateObjectCard.vue';
import CreateCard from './CreateCard.vue';

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

</script>

<template>
    <div class="estate-object-list">
        <EstateObjectCard
            v-for="card in cards"
            v-bind:object="card"
        />
        <CreateCard />
    </div>
</template>

<style scoped>
.estate-object-list {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    justify-items: center;
}
</style>
