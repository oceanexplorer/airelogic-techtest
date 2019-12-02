import '@babel/polyfill'
import 'mutationobserver-shim'
import Vue from 'vue'
import './plugins/bootstrap-vue'
import App from './App.vue'
import VueLodash from 'vue-lodash'

Vue.config.productionTip = false
Vue.use(VueLodash);

new Vue({
  render: h => h(App),
}).$mount('#app')


