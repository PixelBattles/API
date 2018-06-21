import { ApiClient } from "./Clients/ApiClient";
import { IApiClient } from "./Clients/IApiClient";
import { BattleHeader } from "./Controls/BattleHeader";

export class PixelBattle {
    private apiClient: IApiClient;

    private battleId: string;
    private widgetContainer: HTMLDivElement;

    private header: BattleHeader;
    private battleContainer: HTMLDivElement;
    
    constructor(widgetContainer: HTMLDivElement) {
        this.widgetContainer = widgetContainer;
        this.widgetContainer.className = "card";
        this.battleId = this.widgetContainer.getAttribute("battle-id");

        this.header = this.initializeHeader("test header text");

        this.apiClient = new ApiClient("/api/");
        
        //this.apiClient.getBattleInfo(this.battleId).then(battle => {
        //    this.battleHeader = this.createBattleHeader("Test");
        //    this.battleContainer.appendChild(this.battleHeader);
        //    this.gameContainer = this.createGameContainer();
        //    this.battleContainer.appendChild(this.gameContainer);
        //    this.game = this.createGame(this.gameContainer, this.apiClient, this.battleId);

        //window.addEventListener("resize", this.onResize.bind(this));
        //    this.onResize();
        //}, error => {
        //    this.onError();
        //});
    }

    //private createGameContainer(): HTMLDivElement {
    //    let gameContainer: HTMLDivElement = <HTMLDivElement>document.createElement('div');
    //    gameContainer.className = "gameContainer";
    //    gameContainer.setAttribute("style","overflow:hidden");
    //    return gameContainer;
    //}

    private initializeHeader(text: string): BattleHeader{
        let headerContainer: HTMLDivElement = <HTMLDivElement>document.createElement('div');
        this.widgetContainer.appendChild(headerContainer);
        return new BattleHeader(headerContainer, text);
    }

    //private createGame(gameContainer: HTMLDivElement, apiClient: IApiClient, battleId : string): PixelGame {
    //    let game: PixelGame = new PixelGame(gameContainer, apiClient, battleId);
    //    return game;
    //}

    //private onError() {
    //    let errorText = document.createTextNode('Error happened!');
    //    this.battleContainer.appendChild(errorText);
    //}

    //private onResize(): void {
    //    this.game.resize(this.battleContainer.clientWidth, this.battleContainer.clientHeight - this.battleHeader.clientHeight);
    //}
}