<template>
    <div v-bind:style="computedStyle">Hello {{ name }}</div>
</template>
<script>
export default {
    name: 'Greeter',
    props: [ 'backgroundColor', 'name'],
    computed: {
        computedStyle() {
            return `background-color: ${this.backgroundColor}`;
        }
    }
}
</script>

