<template>
  <div>
    <h1>Adicionar Cavaleiro</h1>
    <form @submit.prevent="addKnight">
      <label for="name">Nome:</label>
      <input type="text" id="name" v-model="newKnight.name" required>
      
      <label for="nickname">Apelido:</label>
      <input type="text" id="nickname" v-model="newKnight.nickname" required>
      
      <label for="birthday">Aniversário:</label>
      <input type="date" id="birthday" v-model="newKnight.birthday" required>
      
      <label for="weaponName">Nome da Arma:</label>
      <input type="text" id="weaponName" v-model="newKnight.weapons[0].name">
      
      <label for="weaponMod">Modificador da Arma:</label>
      <input type="number" id="weaponMod" v-model="newKnight.weapons[0].mod">
      
      <!-- Adicione campos adicionais conforme necessário -->

      <button type="submit">Adicionar Cavaleiro</button>
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      newKnight: {
        name: '',
        nickname: '',
        birthday: '',
        weapons: [
          {
            name: '',
            mod: 0
          }
        ],
        attributes: {
          strength: 0,
          dexterity: 0,
          constitution: 0,
          intelligence: 0,
          wisdom: 0,
          charisma: 0
        },
        keyAttribute: '',
        isHero: true
      }
    };
  },
  methods: {
    async addKnight() {
      try {
        const response = await fetch('http://localhost:7101/api/knights', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(this.newKnight)
        });
        
        if (response.ok) {
          alert('Cavaleiro adicionado com sucesso!');
          // Limpar o formulário ou fazer outras ações conforme necessário
        } else {
          throw new Error('Falha ao adicionar cavaleiro');
        }
      } catch (error) {
        console.error('Erro ao adicionar cavaleiro:', error);
        alert('Erro ao adicionar cavaleiro. Consulte o console para mais detalhes.');
      }
    }
  }
};
</script>
