/// <reference path="../../_references.js" />

ProjektGrupowy.Framework.classes.EventEmitter = (function (eventsCount) {

    var handlers = [];
    var global = [];

    for (var i = 0; i < eventsCount; i++) {
        handlers[i] = [];
    }

    this.print = function(){
        //console.log(handlers);
        //console.log(global);
    }


    this.fireEvent = function (eventId, eventObj)
    {
        this.print();
        //console.log('i fired ' + eventId);

        handlers[eventId].forEach(function(e) {
            e.handler(eventObj);
        });
        global.forEach(function(e) {
            e.handler(eventId, eventObj);
        });
    }

    this.fireEventExcept = function (subscription, eventId, eventObj)
    {
        if (subscription.eventId == eventId) {
            handlers[eventId].forEach(function(e) {
                return e.id != subscription.id ? e.handler(eventObj) : 0;
            });
        }
        else {
            handlers[eventId].forEach(function(e) {
                e.handler(eventObj);
            });
        }

        if (subscription.eventId == 'global') {
            global.forEach(function(e) {
                return e.id != subscription.id ? e.handler(eventId, eventObj) : 0;
            });
        }
        else {
            global.forEach(function(e) {
                e.handler(eventId, eventObj);
            });
        }
    }

    this.addGlobalEventListener = function (handler)
    {
        var maxId = Math.max.apply(null, global.map(function(e) {
            return e.id;
        }));

        maxId = maxId < 0 ? 0 : maxId;

        var subscription = {
            id: maxId + 1,
            eventId: 'global',
            handler: handler
        }
        global.push(subscription);

        return subscription;
    }

    this.removeEventListener = function (subscription)
    {
        if (subscription.eventId == 'global') {
            var index = -1;
            var obj = global.map(function(e, i) {
                if (e.id == subscription.id) index = i;
            });
            if (index >= 0) {
                global.splice(index, 1);
            }
        }
        else {
            var index = -1;
            var obj = handlers[subscription.eventId].map(function(e, i) {
                if (e.id == subscription.id) index = i;
            });
            if (index >= 0) {
                handlers[subscription.eventId].splice(index, 1);
            }
        }
    }

    this.addEventListener = function (eventId, handler)
    {
        if (!eventId || eventId >= eventsCount) {
            console.log('invalid event ' + eventId);
        }

        var maxId = Math.max.apply(null, handlers[eventId].map(function(e) {
            return e.id
        }));
        maxId = maxId < 0 ? 0 : maxId;

        var subscription = {
            id: maxId + 1,
            eventId: eventId,
            handler: handler
        }

        handlers[eventId].push(subscription);
        return subscription;
    }
});
