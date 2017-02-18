using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ProjektGrupowy.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/libs").Include(
                "~/Scripts/Libraries/jquery-2.1.4.min.js",
                "~/Scripts/Libraries/jquery-ui.js",
                "~/Scripts/Libraries/semantic.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                "~/Scripts/Structure.js",
                "~/Scripts/Framework/Components/Form.js",
                "~/Scripts/Framework/Components/PopupForm.js",
                "~/Scripts/Framework/Components/EventEmitter.js",
                "~/Scripts/Framework/Components/Controller.js",
                "~/Scripts/App/Api/SiteController.js",
                "~/Scripts/App/Api/GameController.js",
                "~/Scripts/App/Common/Models/Game.js",
                "~/Scripts/App/Common/Models/Site.js",
                "~/Scripts/App/Common/Hubs/GamePageHub.js"));

            bundles.Add(new ScriptBundle("~/bundles/mobile").Include(
                "~/Scripts/App/Mobile/ViewControllers/GamePageController.js",
                "~/Scripts/App/Mobile/ViewControllers/HomePageController.js",
                "~/Scripts/App/Mobile/ViewControllers/NewGamePageController.js",
                "~/Scripts/App/Mobile/Pages/GamePage/Components/Tokens.js",
                "~/Scripts/App/Mobile/Pages/GamePage/Components/TokensPopup.js",
                "~/Scripts/App/Mobile/Pages/Layout/Components/MainMenu.js",
                "~/Scripts/App/Mobile/Pages/Layout/Layout.js",
                "~/Scripts/App/Mobile/Pages/GamePage/GamePage.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                
                "~/Scripts/App/Site/ViewControllers/GamePageController.js",
                "~/Scripts/App/Site/ViewControllers/HomePageController.js",
                "~/Scripts/App/Site/ViewControllers/NewGamePageController.js",
                
                "~/Scripts/App/Site/Pages/Layout/Components/MainMenu.js",
                "~/Scripts/App/Site/Pages/GamePage/Components/Toolbox.js",
                "~/Scripts/App/Site/Pages/GamePage/Components/MenuBar.js",
                "~/Scripts/App/Site/Pages/GamePage/Components/ContentPanel.js",
                "~/Scripts/App/Site/Pages/GamePage/Components/ProposedElements.js",
                "~/Scripts/App/Site/Pages/GamePage/Components/TokensPopup.js",
                "~/Scripts/App/Site/Pages/GamePage/Components/GameBoard.js",
                "~/Scripts/App/Site/Pages/GamePage/Components/GameSheet.js",
                "~/Scripts/App/Site/Pages/GamePage/Components/ElementProperties.js",
                "~/Scripts/App/Site/Pages/GamePage/Components/TokensConfig.js",
                "~/Scripts/App/Site/Pages/GamePage/Components/Tokens.js",
                "~/Scripts/App/Site/Pages/GamePage/Components/Players.js",
                "~/Scripts/App/Site/Pages/Layout/Layout.js",
                "~/Scripts/App/Site/Pages/NewGamePage/NewGamePage.js",
                "~/Scripts/App/Site/Pages/GamePage/GamePage.js"));
        }
    }
}