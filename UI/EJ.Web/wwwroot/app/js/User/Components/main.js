import Vue from 'vue'
import User from './User.vue'

new Vue({
    el: '#app',
    render: function (h) {
        var renderedElement = h(User, {
            props: {
                url: EJ.Settings.UserUrl
            }
        });
        return renderedElement;
    },
    components: {
        'user': User
    }
});