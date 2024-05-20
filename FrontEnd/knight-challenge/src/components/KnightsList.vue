<template>
  <div>
    <h1>Lista de Cavaleiros</h1>
    <ul v-if="knights.length > 0">
      <li v-for="knight in knights" :key="knight.id">{{ knight.name }}</li>
    </ul>
    <p v-else>Nenhum cavaleiro encontrado.</p>
    <p v-if="error">Erro ao buscar a lista de cavaleiros: {{ error }}</p>
  </div>
</template>

<script>
export default {
  data() {
    return {
      knights: [],
      error: null
    };
  },
  mounted() {
    this.getKnights();
  },
  methods: {
    async getKnights() {
      try {
        const response = await fetch('http://localhost:7101/api/knights');
        if (!response.ok) {
          throw new Error('Erro ao buscar a lista de cavaleiros');
        }
        const data = await response.json();
        this.knights = data;
      } catch (error) {
        console.error('Erro ao buscar a lista de cavaleiros:', error);
        this.error = error.message;
      }
    }
  }
};
</script>

