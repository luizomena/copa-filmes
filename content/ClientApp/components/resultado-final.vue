<template>
    <div>

        <br/>
        <div class="jumbotron text-center">
            <h6>CAMPEONATO DE FILMES</h6>
            <h1>Resultado Final</h1>
            <hr/>
            <p>Veja o resultado final do Campeonato de filmes de forma simples e rápida.</p>
        </div>
        
        <div class="row">
            <div class="col-sm-12 text-center">
                <div v-if="!campeao || !vicecampeao" class="text-center">
                    <p><em>Carregando...</em></p>
                    <h1><icon icon="spinner" pulse/></h1>            
                </div>
            </div>
        </div>

        <div class="row border-between">
            <div class="col-lg-1 text-right">                    
                <h1>1º</h1>
            </div>
            <div class="col-lg-11"><br/>
                <h5>{{campeao.titulo}}</h5>
            </div>
        </div>

        <div class="row border-between">
            <div class="col-lg-1 text-right">                    
                <h1>2º</h1>
            </div>
            <div class="col-lg-11"><br/>
                <h5>{{vicecampeao.titulo}}</h5>
            </div>
        </div>
    </div>
</template>

<script>


export default {

  props: ['filmesSelecionados'],

  data () {
    return {
      campeao: null,
      vicecampeao: null
    }
  },

  methods: {
    async loadPage () {
      try {
        var json = JSON.stringify(this.filmesSelecionados)
        let response = await this.$http.get(`/api/filme/gerarcampeonato?json=${json}`)
        this.campeao = response.data.campeao
        this.vicecampeao = response.data.vicecampeao
      } catch (err) {
        console.log(err)
      }
    }
  },

  async created () {
    this.loadPage()
  }
}
</script>

<style>
</style>
