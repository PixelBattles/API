import { PixelGame } from "./PixelGame";
import { ApiClient } from "./ApiClient";
import { IApiClient } from "./IApiClient";

export class PixelBattle {
    private apiClient: IApiClient;

    private battleId: string;
    private battleContainer: HTMLDivElement;
    private battleHeader: HTMLDivElement;


    private game: PixelGame;
    private gameContainer: HTMLDivElement;
    
    constructor(battleContainer: HTMLDivElement, battleId: string) {
        this.battleContainer = battleContainer;
        this.battleContainer.className = "card";
        this.battleId = battleId;
        this.apiClient = new ApiClient();
        
        this.apiClient.getBattleInfo(battleId).then(battle => {
            this.battleHeader = this.createBattleHeader("Test");
            this.battleContainer.appendChild(this.battleHeader);
            this.gameContainer = this.createGameContainer();
            this.battleContainer.appendChild(this.gameContainer);
            this.game = this.createGame(this.gameContainer, this.apiClient, this.battleId);

            window.addEventListener("resize", this.onResize.bind(this));
            this.onResize();
        }, error => {
            this.onError();
        });
    }

    private createGameContainer(): HTMLDivElement {
        let gameContainer: HTMLDivElement = <HTMLDivElement>document.createElement('div');
        gameContainer.className = "gameContainer";
        gameContainer.setAttribute("style","overflow:hidden");
        return gameContainer;
    }

    private createBattleHeader(headerText: string): HTMLDivElement {
        let headerContainer: HTMLDivElement = <HTMLDivElement>document.createElement('div');
        headerContainer.className = "card-header";
        let header: HTMLHeadingElement = <HTMLHeadingElement>document.createElement('h4');
        header.className = "my-0 font-weight-normal";
        header.textContent = headerText;
        headerContainer.appendChild(header);
        return headerContainer;
    }

    private createGame(gameContainer: HTMLDivElement, apiClient: IApiClient, battleId : string): PixelGame {
        let game: PixelGame = new PixelGame(gameContainer, apiClient, battleId);
        return game;
    }

    private onError() {
        let errorText = document.createTextNode('Error happened!');
        this.battleContainer.appendChild(errorText);
    }

    private onResize(): void {
        this.game.resize(this.battleContainer.clientWidth, this.battleContainer.clientHeight - this.battleHeader.clientHeight);
    }
}