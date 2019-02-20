<template>
    <div>
        <br/>
        <div class="jumbotron text-center">
            <h6>CAMPEONATO DE FILMES</h6>
            <h1>Fase de Seleção</h1>
            <hr/>
            <p>Selecione 8 filmes que você deseja que entrem na competição e depois pressione o botão Gerar Meu Campeonato para prosseguir.</p>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <div v-if="!filmes" class="text-center">
                    <p><em>Carregando...</em></p>
                    <h1><icon icon="spinner" pulse/></h1>            
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-6 text-left">
                Selecionados<br/>
                {{filmesSelecionados.length}} de 8 filmes
            </div>
            <div class="col-sm-6 text-right">
                <button type="button" class="btn btn-primary" :disabled="filmesSelecionados.length < 8" @click="gerarCampeonato()">Gerar meu campeonato</button>
            </div>
        </div>

        <template v-if="filmes">
            
            <div class="row border-between">
                <div class="col-lg-3" v-for="(filme, index) in filmes" :key="index">                    
                    <input type="checkbox" :id="filme.id" :value="filme" :disabled="filmesSelecionados.length > 7 && filmesSelecionados.indexOf(filme) === -1" v-model="filmesSelecionados">                      
                    <label :for="filme.id"><strong>{{filme.titulo}}</strong><br/>{{ filme.ano }}</label>
                </div>
            </div>

        </template>
    </div>
</template>

<script>

import router from '../router'

export default {

  data () {
    return {
      filmes: null,
      filmesSelecionados: []
    }
  },

  watch: { 
      filmesSelecionados: function(value){
          console.log(value);
      }
  },

  methods: {
    async loadPage () {
      try {
        let response = await this.$http.get(`https://copadosfilmes.azurewebsites.net/api/filmes`)
        this.filmes = response.data
      } catch (err) {
        console.log(err)
      }      
    },
    gerarCampeonato: function () {
      var data = this.filmesSelecionados
      router.push({
        name: 'resultado-final',
        params: {
          filmesSelecionados: data
        }
      })
    }    
  },

  async created () {
    this.loadPage()
  }
}
</script>

<style>
</style>
