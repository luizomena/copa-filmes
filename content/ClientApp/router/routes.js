import EscolhaParticipantes from 'components/escolha-participantes'
import ResultadoFinal from 'components/resultado-final'

export const routes = [
  { name: 'escolha-participantes', path: '/', component: EscolhaParticipantes, props: true },
  { name: 'resultado-final', path: '/resultado-final', component: ResultadoFinal, props: true }
]
