EJ.Vue = EJ.Vue || {};
EJ.Vue.Settings = EJ.Vue.Settings || {};

EJ.Vue.Settings = (function () {
    var eventStack = new Map();

    function pushToEventStack(key, instance) {
        eventStack.set(key, instance);
    }

    function popFromEventStack(key) {
        eventStack.delete(key);
    }

    function getEventFromStack(key) {
        return eventStack.get(key);
    }

    function currentStack() {
        console.log(eventStack);
    }
    return {
        pushToEventStack: pushToEventStack,
        popFromEventStack: popFromEventStack,
        currentStack: currentStack,
        getEventFromStack: getEventFromStack
    };
})();
