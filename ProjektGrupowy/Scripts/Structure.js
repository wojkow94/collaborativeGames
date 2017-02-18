
var ProjektGrupowy = {
    App: {
        Api: {
            Game: {},
            Site: {},
            Content: {},
        },

        Site: {
            pages: {
                Layout: {},
                NewGamePage: {},
                GamePage: {}
            },

            components: {
                Layout: {
                    MainMenu: {}
                },

                GamePage: {
                    ElementProperties: {},
                    GameBoard: {},
                    GameSheet: {},
                    MenuBaar: {},
                    ProposedElements: {},
                    Tokens: {},
                    TokensPopup: {},
                    TokensConfig: {},
                    Toolbox: {},
                    Players: {}
                }
            },

            viewControllers: {
                GamePage: {},
                NewGamePage: {},
                HomePage: {}
            }
        },
        Mobile: {
            viewControllers: {
                GamePage: {},
                NewGamePage: {},
                HomePage: {}
            },

            pages: {
                Layout: {},  
                GamePage: {}
            },

            components: {
                Layout: {
                    MainMenu: {}
                },
                GamePage: {
                    MenuBar: {},
                    ProposedElements: {},
                    AcceptedElements: {},
                    Tokens: {},
                    TokensPopup: {}
                }
            }
        },
        Common: {
            Hubs: {
                Game: {}
            },
            Models: {
                Site: {},
                Game: {}
            }
        }
    },
    Framework: {
        classes: {
            Controller: {},
            EventEmitter: {},
            Form: {},
            PopupForm: {},
        }
    }
}
