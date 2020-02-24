<template>
    <div id="app">
        <Greeter name="Tim" background-color="red" />
    </div>
</template>

<script>
import Greeter from './components/Greeter.vue'

export default {
  name: 'app',
  components: {
    Greeter
  }
}
</script>
