import Vue from 'vue'
import Group from './Group.vue'
import axios from 'axios'

new Vue({
    el: '#app',
    render: function (h) {
        var renderedElement = h(Group, {
            props: {
                url: EJ.Settings.GroupsUrl,
                data: "Andre"
            }
        });
        return renderedElement;
    },
    components: {
        'group': Group
    }
});