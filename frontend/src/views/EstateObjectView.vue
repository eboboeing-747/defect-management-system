<script setup lang="ts">
import Header from '@/components/Header.vue';
import EstateObject from '@/components/estate-object/EstateObject.vue';
</script>

<template>
    <Header />
    <div
        class="estate-object"
    >
        <EstateObject />
    </div>
</template>

<style scoped>
.estate-object {
    display: flex;
    flex-direction: column;
    align-items: center;
}
</style>
