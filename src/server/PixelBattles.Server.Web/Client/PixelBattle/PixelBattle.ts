import { PixelGame } from "./PixelGame";
import { ApiClient } from "./ApiClient";
import { IApiClient } from "./IApiClient";

export class PixelBattle {
    private apiClient: IApiClient;

    private battleId: string;
    private battleContainer: HTMLDivElement;

    private game: PixelGame;
    private gameContainer: HTMLDivElement;
    
    constructor(battleContainer: HTMLDivElement, battleId: string) {
        this.battleContainer = battleContainer;
        this.battleId = battleId;
        this.apiClient = new ApiClient();

        this.apiClient.getBattleInfo(battleId).then(battle => {
            this.gameContainer = this.createGameContainer();
            this.battleContainer.appendChild(this.gameContainer);
            this.game = this.createGame(this.gameContainer, this.apiClient, this.battleId);
        }, error => {
            this.onError();
        });
    }

    private createGameContainer(): HTMLDivElement {
        let gameContainer: HTMLDivElement = <HTMLDivElement>document.createElement('div');
        gameContainer.className = "gameContainer";
        return gameContainer;
    }

    private createGame(gameContainer: HTMLDivElement, apiClient: IApiClient, battleId : string): PixelGame {
        let game: PixelGame = new PixelGame(gameContainer, apiClient, battleId);
        return game;
    }

    private onError() {
        let errorText = document.createTextNode('Error happened!');
        this.battleContainer.appendChild(errorText);
    }
}