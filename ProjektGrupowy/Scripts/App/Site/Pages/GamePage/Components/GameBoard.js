/// <reference path="../../_references.js" />

ProjektGrupowy.App.Site.components.GamePage.GameBoard = (function () {

    //-------------------------------------------------

    var viewControllers = {
        game: ProjektGrupowy.App.Site.viewControllers.GamePage
    }

    var components = {
        TokensPopup: ProjektGrupowy.App.Site.components.GamePage.TokensPopup
    }

    var models = {
        Game: ProjektGrupowy.App.Common.Models.Game,
        GameEvents: ProjektGrupowy.App.Common.Models.Game.enums.events
    }

    //-------------------------------------------------

    var config = {
        elementLabel: ".elementLabel",
        tokenLabel: ".tokenLabel",
        rejectElement: ".rejectElementBtn",
        boardRegion: ".boardRegion",
        regionContainer: ".regionContainer",
        elementsContainer: ".elementsContainer"
    };

    var container = undefined;
    var modelSubscriptions = [];
    var loaded = false;
    var canAccept = {};
    var containerId = 0;
    var regionId = 0;

    //-------------------------------------------------

    function isLoaded() {
        return loaded;
    }

    function load(cont) {
        container = cont;

        components.TokensPopup.init();
        bindModelEvents();
        bindUIEvents();
        refresh();

        loaded = true;
    };

    function unload() {
        $(container).empty();
        unbindModelEvents();
        unbindUIEvents();
        loaded = false;
    }


    function bindModelEvents() {

        modelSubscriptions.push(

            models.Game.events.addEventListener(models.GameEvents.onMoveElement, function (event) {

                var label = $(container).find(config.elementLabel + '#' + event.elementId);
                var region = $(container).find(config.regionContainer + '#' + event.containerId).find(config.boardRegion + '#' + event.regionId + ' ' + config.elementsContainer);
                region.append(label);
            }),

            models.Game.events.addEventListener(models.GameEvents.onEditElement, function (event) {
                refreshElement(event.elementId);
            }),
            //IMPORTANT!! on accept
            models.Game.events.addEventListener(models.GameEvents.onAcceptElement, function (event) {

                var label = $(container).find(config.elementLabel + '#' + event.elementId);
                $.get(viewControllers.game.actions.boardElementLabel(event.elementId), function (data) {
                    var newLabel = $(data);
                    if (label.length > 0) {
                        label.replaceWith(newLabel);
                    }
                    else {
                        var region = $(container).find(config.regionContainer + '#' + event.containerId).find(config.boardRegion + '#' + event.regionId + ' ' + config.elementsContainer);
                        region.append(newLabel);
                    }

                    configureElementsDrag(newLabel);
                    enableTokenDrop(newLabel);
                    configurePopups($(config.elementLabel));
                });
            }),

            models.Game.events.addEventListener(models.GameEvents.onAddTokens, function (event) {
                refreshElement(event.elementId);
            }),
            models.Game.events.addEventListener(models.GameEvents.onSetTokens, function (event) {
                refreshElement(event.elementId);
            }),

            models.Game.events.addEventListener(models.GameEvents.onRejectElement, function (event) {
                $(config.boardRegion).find(config.elementLabel).filter('#' + event.elementId).remove();
            })
        );
    }

    function unbindModelEvents() {
        modelSubscriptions.map(function (subscription) {
            models.Game.events.removeEventListener(subscription);
        });
    }

    function configurePopups(label) {
        label.popup();
        label.find('.tokenLabel').popup();
    }

    function refreshElement(elementId) {
        var label = $(container).find(config.elementLabel + '#' + elementId);
        $.get(viewControllers.game.actions.boardElementLabel(elementId), function (data) {
            var newLabel = $(data);
            label.replaceWith(newLabel);

            configureElementsDrag(newLabel);
            enableTokenDrop(newLabel);

            configurePopups(newLabel);
        });
    }

    function refresh() {
        $(container).siblings('.dimmer').dimmer('show');

        $.get(viewControllers.game.actions.gameBoardView(), function (data) {
            $(container).html(data);
            configureElementDragNDrop();
            enableTokenDrop($(container).find(config.elementLabel));
            $(container).siblings('.dimmer').dimmer('hide');

            configurePopups($(config.elementLabel));
        });
    }

    function bindUIEvents() {

        $(container).on("click", config.rejectElement, function () {
            models.Game.rejectElement(this.dataset.elementdefid, this.id);
            $(config.elementLabel).popup('hide');
            event.stopPropagation();
        });

        $(container).on("click", config.tokenLabel, function (event) {
            selectToken(this.id, $(this).closest(config.elementLabel).get(0).id, event.pageX, event.pageY);
            event.stopPropagation();
        });

        $(container).on("click", config.elementLabel, function () {
            models.Game.selectElement(this.id);
        });
    }

    function unbindUIEvents() {
        $(container).off();
    }

    function enableTokenDrop(elements) {
        elements.droppable({
            hoverClass: "elementLabelOverToken",
            accept: '.tokenLabel',
            drop: function (event, ui) {
                dropToken(ui.draggable[0].id, this.id, event.clientX, event.clientY);
            }
        });
    }

    function enableElementDrag(element) {
        $(element).draggable({
            helper: 'clone',
            revert: 'invalid',
            scroll: false,
            start: function () {
                $(this).css("opacity", 0.2);
            },
            stop: function () {
                $(this).css("opacity", 1);
                dropElement(element.id, containerId, regionId, element.dataset.elementdefid);
            }
        });
    }

    function configureElementsDrag(elements) {
        console.log('configure drag');
        console.log(elements);

        var ids = [];
        elements.each(function (index, el) {
            ids.push(el.dataset.elementdefid);
        });
        var unique = ids.filter(function (elem, index, self) {
            return index == self.indexOf(elem);
        });

        unique.map(function (id) {
            if (canAccept[id] == undefined) {
                models.Game.canAccept(id, function (result) {
                    canAccept[id] = result.Data.canAccept;
                    elements.each(function (index, label) {
                        if (label.dataset.elementdefid == id && canAccept[id] === true) {
                            enableElementDrag(label);
                        }
                    });
                });
            }
            else {
                elements.each(function (index, label) {
                    if (label.dataset.elementdefid == id && canAccept[id] === true) {
                        enableElementDrag(label);
                    }
                });
            }
        });
    }

    //IMPORTANT!!
    function configureElementDragNDrop() {

        configureElementsDrag($(container).find(config.elementLabel));

        $(container).find(config.boardRegion).droppable({
            hoverClass: "hoverRegion",
            accept: function (element) {
                return $(element).attr("data-elementdefid") === $(this).attr("data-accepted");
            },
            drop: function (event, ui) {
                $(ui.draggable[0]).css("left", 0);
                $(ui.draggable[0]).css("top", 0);

                containerId = this.parentElement.id;
                regionId = this.id;

                models.Game.canAcceptElement(ui.draggable[0].id, function (result) {
                    if (result.Data.canAccept == true) {
                        dropElement(ui.draggable[0].id, containerId, regionId, ui.draggable[0].dataset.elementdefid);
                    }
                });


                $(this).find(config.elementsContainer).append($(ui.draggable[0]));
            }
        });
    }

    function dropElement(elementId, containerId, regionId, elDefId) {
        models.Game.acceptElement(elDefId, elementId, containerId, regionId);
    }

    function selectToken(tokenDefId, elementId, x, y) {
        models.Game.getPlayerTokensAmount(tokenDefId, elementId, function (amount) {
            components.TokensPopup.load(x, y, tokenDefId, elementId, components.TokensPopup.modes.set, amount);
        });
    }

    function dropToken(tokenDefId, elementId, x, y) {
        models.Game.getPlayerTokensAmount(tokenDefId, elementId, function (amount) {
            components.TokensPopup.load(x, y, tokenDefId, elementId, components.TokensPopup.modes.set, amount);
        });
    }

    //-------------------------------------------------

    return {
        load: load,
        unload: unload,
        refresh: refresh
    }
}());
