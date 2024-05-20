<template>
  <div id="app">
    <KnightsList />
    <KnightForm />
  </div>
</template>

<script>
import KnightsList from './components/KnightsList.vue';
import KnightForm from './components/KnightForm.vue';

export default {
  name: 'App',
  components: {
    KnightsList,
    KnightForm
  }
}
</script>

<style>
/* Adicione estilos conforme necessário */
</style>
