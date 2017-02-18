
App.imports([

    //Structure
    'Structure',

    'Framework/Components/Form',
    'Framework/Components/PopupForm',
    'Framework/Components/EventEmitter',
    'Framework/Components/Controller',

    //View Controllers
    'App/Site/ViewControllers/GamePageController',
    'App/Site/ViewControllers/HomePageController',
    'App/Site/ViewControllers/NewGamePageController',

    //Api Controllers
    'App/Api/SiteController',
    'App/Api/GameController',

    //Models
    'App/Common/Models/Game',
    'App/Common/Models/Site',

    //Components
    'App/Site/Pages/Layout/Components/MainMenu',
    'App/Site/Pages/GamePage/Components/Toolbox',
    'App/Site/Pages/GamePage/Components/MenuBar',
    'App/Site/Pages/GamePage/Components/ContentPanel',
    'App/Site/Pages/GamePage/Components/ProposedElements',
    'App/Site/Pages/GamePage/Components/TokensPopup',
    'App/Site/Pages/GamePage/Components/GameBoard',
    'App/Site/Pages/GamePage/Components/GameSheet',
    'App/Site/Pages/GamePage/Components/ElementProperties',
    'App/Site/Pages/GamePage/Components/Tokens',
    

    //Pages
    'App/Site/Pages/Layout/Layout',
    'App/Site/Pages/NewGamePage/NewGamePage',
    'App/Site/Pages/GamePage/GamePage',

], 'SiteMain');